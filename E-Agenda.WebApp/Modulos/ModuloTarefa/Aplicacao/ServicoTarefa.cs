using System;
using System.Collections.Generic;
using System.Linq;
using E_Agenda.WebApp.Modulos.ModuloItensTarefa.Dominio;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Dominio;
using FluentResults;
using E_Agenda.WebApp.Modulos.ModuloItensTarefa.Apresentacao;

namespace E_Agenda.WebApp.Modulos.ModuloTarefa.Aplicacao;

public class ServicoTarefa
{
    private readonly IRepositorioTarefa repositorioTarefa;

    public ServicoTarefa(IRepositorioTarefa repositorioTarefa)
    {
        this.repositorioTarefa = repositorioTarefa;
    }

    public Result Cadastrar(CadastrarTarefaDto dto)
    {
        List<CadastrarItemTarefaDto> itensDto = dto.Itens ?? new List<CadastrarItemTarefaDto>();

        Tarefa novaTarefa = new Tarefa(dto.Titulo, dto.PrioridadeTarefa, dto.DataConclusao);

        List<ItensDeTarefas> itens = itensDto
            .Select(i => new ItensDeTarefas(i.Titulo) { Tarefa = novaTarefa })
            .ToList();

        // Alterado de 'ItemTarefa' para 'Itens'
        novaTarefa.Itens = itens;

        repositorioTarefa.Cadastrar(novaTarefa);

        return Result.Ok();
    }

    public Result Editar(EditarTarefaDto dto)
    {
        Tarefa? tarefa = repositorioTarefa.SelecionarPorId(dto.Id);

        if (tarefa == null)
            return Result.Fail("Tarefa não encontrada.");

        List<EditarItemTarefaDto> itensDto = dto.Itens ?? new List<EditarItemTarefaDto>();

        Tarefa tarefaAtualizada = new Tarefa(dto.Titulo, dto.PrioridadeTarefa, dto.DataConclusao)
        {
            StatusConclusao = dto.StatusConclusao,
            PercentualConcluido = dto.PercentualConcluido
        };

        List<ItensDeTarefas> itens = itensDto
            .Select(i => new ItensDeTarefas(i.Titulo) { StatusConclusao = i.StatusConclusao, Tarefa = tarefaAtualizada })
            .ToList();

        // Alterado de 'ItemTarefa' para 'Itens'
        tarefaAtualizada.Itens = itens;

        tarefa.Atualizar(tarefaAtualizada);

        Result resultadoValidacao = ValidarEntidade(tarefa);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioTarefa.Editar(dto.Id, tarefa);

        return Result.Ok();
    } 

    public Result Excluir(Guid id)
    {
        Tarefa? tarefa = repositorioTarefa.SelecionarPorId(id);

        if (tarefa == null)
            return Result.Fail("Tarefa não encontrada.");

        repositorioTarefa.Excluir(id);

        return Result.Ok();
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
                t.PercentualConcluido)) // Agora sem o (int), pois o DTO aceita decimal
            .ToList();
    }
   public Result<DetalhesTarefaDto> SelecionarPorId(Guid id)
    {
        Tarefa? tarefa = repositorioTarefa.SelecionarPorId(id);

        if (tarefa == null)
            return Result.Fail("Tarefa não encontrada");

        // Alterado de 'ItemTarefa' para 'Itens'
        List<ItemTarefaDto> itens = tarefa.Itens
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
                tarefa.PercentualConcluido, // Agora sem o (int), pois o DTO aceita decimal
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
}