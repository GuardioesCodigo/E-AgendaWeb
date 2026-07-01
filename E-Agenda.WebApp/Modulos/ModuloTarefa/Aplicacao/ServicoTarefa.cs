using System;
using E_Agenda.WebApp.Modulos.ModuloItensTarefa.Dominio;
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

    public Result Cadastrar(CadastrarTarefaDto dto)
    {
        if (ExisteTarefaComTitulo(dto.Titulo))
            return Falha(nameof(dto.Titulo), "Já existe uma Tarefa com este Título.");

        List<CadastrarItemTarefaDto> itensDto = dto.Itens ?? new List<CadastrarItemTarefaDto>();

        Tarefa novaTarefa = new Tarefa(dto.Titulo, dto.PrioridadeTarefa, dto.DataConclusao);

        List<ItensDeTarefas> itens = itensDto
            .Select(i => new ItensDeTarefas())
            .ToList();

        novaTarefa.ItemTarefa = itens;

        repositorioTarefa.Cadastrar(novaTarefa);

        return Result.Ok();
    }

    public Result Editar(EditarTarefaDto dto)
    {
        Tarefa? tarefa = repositorioTarefa.SelecionarPorId(dto.Id);

        if (tarefa == null)
            return Result.Fail("Tarefa não encontrada.");

        if (ExisteTarefaComTitulo(dto.Titulo, dto.Id))
            return Falha(nameof(dto.Titulo), "Já existe uma Tarefa com este Título.");

        List<EditarItemTarefaDto> itensDto = dto.Itens ?? new List<EditarItemTarefaDto>();

        Tarefa tarefaAtualizada = new Tarefa(dto.Titulo, dto.PrioridadeTarefa, dto.DataConclusao)
        {
            StatusConclusao = dto.StatusConclusao,
            PercentualConcluido = dto.PercentualConcluido
        };

        List<ItensDeTarefas> itens = itensDto
            .Select(i => new ItensDeTarefas(i.Titulo))
            .ToList();

        tarefaAtualizada.ItemTarefa = itens;

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

    private bool ExisteTarefaComTitulo(string titulo, Guid? idIgnorado = null)
    {
        return repositorioTarefa.SelecionarTodos()
            .Any(t =>
                t.Titulo.Trim().Equals(titulo.Trim(), StringComparison.OrdinalIgnoreCase) &&
                t.Id != idIgnorado
            );
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