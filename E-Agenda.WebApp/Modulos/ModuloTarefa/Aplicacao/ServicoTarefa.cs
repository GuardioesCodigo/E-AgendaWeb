using System;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Dominio;
using FluentResults;

namespace E_Agenda.WebApp.Modulos.ModuloTarefa.Aplicacao;

public class ServicoTarefa
{
    private readonly IRepositorioTarefa repositorioTarefa;

    public ServicoTarefa(IRepositorioTarefa repositorioTarefa)
    {
        this.repositorioTarefa = repositorioTarefa;
    }

    public List<ListarTarefaDto> SelecionarTodos()
    { 
        List<Tarefa> tarefas = repositorioTarefa.SelecionarTodos();

        return tarefas
            .Select(t => new ListarTarefaDto(
                t.Id,
                t.Titulo,
                t.PrioridadeTarefa,
                t.DataConclusao,
                t.StatusConclusao,
                t.PercentualConcluido))
            .ToList();
    }

    public Result<DetalhesTarefaDto> SelecionarPorId(Guid id)
    {
        Tarefa? tarefa = repositorioTarefa.SelecionarPorId(id);

        if (tarefa == null)
            return Result.Fail("Tarefa não encontrada");

        List<ItemTarefaDto> itens = tarefa.ItemTarefa
            .Select(i => new ItemTarefaDto(i.Id, i.Titulo, i.StatusConclusao))
            .ToList();

        return Result.Ok(
            new DetalhesTarefaDto(
                tarefa.Id,
                tarefa.Titulo,
                tarefa.PrioridadeTarefa,
                tarefa.DataCriacao,
                tarefa.DataConclusao,
                tarefa.StatusConclusao,
                tarefa.PercentualConcluido,
                itens
            ));
    }

    private static Result ValidarEntidade(Tarefa tarefa)
    {
        List<string> erros = tarefa.Validar();

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
