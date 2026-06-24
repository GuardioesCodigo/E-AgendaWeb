using System;
using E_Agenda.WebApp.Modulos.ModuloCategoria.Dominio;
using FluentResults;

namespace E_Agenda.WebApp.Modulos.ModuloCategoria.Aplicacao;

public class ServicoCategoria
{
    private readonly IRepositorioCategoria repositorioCategorias;
 
    public ServicoCategoria(IRepositorioCategoria repositorioCategorias)
    {
        this.repositorioCategorias = repositorioCategorias;
    }

    public Result Cadastrar(CadastrarCategoriasDto dtos)
    { 
        Categoria novaCategoria = new Categoria(dtos.Titulo);
 
        repositorioCategorias.Cadastrar(novaCategoria);
 
        return Result.Ok();
    }

    public List<ListarCategoriasDto> SelecionarTodos()
    {
        List<Categoria> categorias = repositorioCategorias.SelecionarTodos();
 
        return categorias
            .Select(c => new ListarCategoriasDto(c.Id, c.Titulo))
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
