using E_Agenda.WebApp.Modulos.ModuloCategoria.Dominio;
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
 
    private static Result ValidarEntidade(Categoria categorias)
    {
        List<string> erros = categorias.Validar();
 
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
