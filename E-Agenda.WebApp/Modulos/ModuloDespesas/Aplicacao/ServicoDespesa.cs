using E_Agenda.WebApp.Modulos.ModuloCategoria.Dominio;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Apresentacao;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Dominio;
using FluentResults;
using Microsoft.Extensions.DependencyModel;

namespace E_Agenda.WebApp.Modulos.ModuloDespesas.Aplicacao;

public class ServicoDespesa
{
    private readonly IRepositorioDepesa repositorioDespesa;
    private readonly IRepositorioCategoria repositorioCategoria;

    public ServicoDespesa(IRepositorioDepesa repositorioDespesa, IRepositorioCategoria repositorioCategoria)
    {
        this.repositorioDespesa = repositorioDespesa;
        this.repositorioCategoria = repositorioCategoria;
    }

    public Result Cadastrar(CadastrarDespesaDto dto)
    {
        Categoria? categoriaSelecionada = repositorioCategoria.SelecionarPorId(dto.CategoriaId);
 
        if (categoriaSelecionada == null)
            return Falha(nameof(dto.CategoriaId), "Selecione uma Categoria válida!");
 
        Despesa novaDespesa = new Despesa(
            dto.Descricao,
            dto.Valor,
            dto.FormaPagamento,
            categoriaSelecionada,
            dto.DataOcorrencia
        );
 
        Result resultadoValidacao = ValidarEntidade(novaDespesa);
 
        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;
 
        repositorioDespesa.Cadastrar(novaDespesa);
 
        return Result.Ok();
    }

    public List<ListarDespesaDto> SelecionarTodos()
    {
        List<Despesa> despesas = repositorioDespesa.SelecionarTodos();

        return despesas
            .Select(d => new ListarDespesaDto(
                d.Id, 
                d.Descricao,
                d.DataOcorrencia, 
                d.Valor, 
                d.FormaPagamento,
                d.Categoria.Id,
                d.Categoria.Titulo))
            .ToList();
    }
 
    public Result<DetalhesDespesaDto> SelecionarPorId(Guid id)
    {
        Despesa? despesas = repositorioDespesa.SelecionarPorId(id);
 
        if (despesas == null)
            return Result.Fail("Despesa não encontrada");
 
        return Result.Ok(
            new DetalhesDespesaDto(despesas.Id, 
            despesas.Descricao,
            despesas.DataOcorrencia,
            despesas.Valor,
            despesas.FormaPagamento,
            despesas.Categoria.Id,
            despesas.Categoria.Titulo
        ));
    }

    public List<OpcaoCategoriaDto> SelecionarCategorias()
    {
        return repositorioCategoria
            .SelecionarTodos()
            .Select(c => new OpcaoCategoriaDto(c.Id, c.Titulo))
            .ToList();
    }
 
     private static Result ValidarEntidade(Despesa despesa)
    {
        List<string> erros = despesa.Validar();
 
        if (erros.Count == 0)
            return Result.Ok();
 
        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }
 
    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);
 
        return Result.Fail(erro);
    }
}
