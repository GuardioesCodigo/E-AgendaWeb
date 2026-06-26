using System;
using E_Agenda.WebApp.Modulos.ModuloCategoria.Dominio;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Dominio;
using FluentResults;

namespace E_Agenda.WebApp.Modulos.ModuloCategoria.Aplicacao;

public class ServicoCategoria
{
    private readonly IRepositorioCategoria repositorioCategorias;
    private readonly IRepositorioDepesa repositorioDepesa;

    public ServicoCategoria(IRepositorioCategoria repositorioCategorias, IRepositorioDepesa repositorioDepesa)
    {
        this.repositorioCategorias = repositorioCategorias;
        this.repositorioDepesa = repositorioDepesa;
    }

    public Result Cadastrar(CadastrarCategoriasDto dtos)
    { 
        if (ExisteCategoriaComTitulo(dtos.Titulo))
            return Falha("Titulo", "Já existe uma Categoria com este Título.");

        Categoria novaCategoria = new Categoria(dtos.Titulo);
 
        repositorioCategorias.Cadastrar(novaCategoria);
 
        return Result.Ok();
    }

    public Result Editar(EditarCategoriasDto dto)
    {
        if (ExisteCategoriaComTitulo(dto.Titulo, dto.Id))
            return Falha(nameof(dto.Titulo), "Já existe uma Categoria com este Título.");
 
        Categoria categoriaAtualizada = new Categoria(dto.Titulo);
 
        Result resultadoValidacao = ValidarEntidade(categoriaAtualizada);
 
        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;
 
        bool conseguiuEditar = repositorioCategorias.Editar(dto.Id, categoriaAtualizada);
 
        if (!conseguiuEditar)
            return Result.Fail("Categoria não encontrada.");
 
        return Result.Ok();
    }

       public Result Excluir(Guid id)
    {
        Categoria? categorias = repositorioCategorias.SelecionarPorId(id);
 
        if (categorias == null)
            return Result.Fail("Categoria não encontrada.");

        bool possuiDespesas = repositorioDepesa
            .SelecionarTodos()
            .Any(d => d.Categoria.Id == id);

        if (possuiDespesas)
            return Result.Fail("Esta Categoria não pode ser excluída pois está vinculada a uma Despesa.");

        repositorioCategorias.Excluir(id);
 
        return Result.Ok();
    }

    private bool ExisteCategoriaComTitulo(string titulo, Guid? idIgnorado = null)
    {
        List<Categoria> categorias = repositorioCategorias.SelecionarTodos();
 
        foreach(Categoria c in categorias)
        {
            if (c.Id != idIgnorado && string.Equals(c.Titulo, titulo, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
 
        return false;
    }

    public List<ListarCategoriasDto> SelecionarTodos()
    {
        List<Categoria> categorias = repositorioCategorias.SelecionarTodos();
 
        return categorias
            .Select(c => new ListarCategoriasDto(c.Id, c.Titulo, c.Despesas.Count))
            .ToList();
    }
 
    public Result<DetalhesCategoriasDto> SelecionarPorId(Guid id)
    {
        Categoria? categorias = repositorioCategorias.SelecionarPorId(id);
 
        if (categorias == null)
            return Result.Fail("Categoria não encontrada");
 
        return Result.Ok(
            new DetalhesCategoriasDto(categorias.Id, categorias.Titulo)
        );
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
