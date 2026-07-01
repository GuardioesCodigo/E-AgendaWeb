using System;
using System.ComponentModel.DataAnnotations;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Dominio;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using E_Agenda.WebApp.Modulos.ModuloItensTarefa.Apresentacao;


namespace E_Agenda.WebApp.Modulos.ModuloTarefa.Apresentacao;

public record ListarTarefaViewModel(
    Guid Id,
    string Titulo,
    PrioridadeTarefa PrioridadeTarefa,
    DateTime DataConclusao,
    bool StatusConclusao,
    int PercentualConcluido
);

public record CadastrarTarefaViewModel(
    [Required(ErrorMessage = "O campo \"Título\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Título\" deve conter entre 2 e 100 caracteres.")]
    string Titulo,

    [Required(ErrorMessage = "Selecione uma Prioridade.")]
    PrioridadeTarefa PrioridadeTarefa,

    [Required(ErrorMessage = "O campo \"Data de Conclusão\" deve ser preenchido.")]
    DateTime DataConclusao,

    [ValidateNever]
    List<ItensDeTarefasViewModel> Itens
);

public record EditarTarefaViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Título\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Título\" deve conter entre 2 e 100 caracteres.")]
    string Titulo,

    [Required(ErrorMessage = "Selecione uma Prioridade.")]
    PrioridadeTarefa PrioridadeTarefa,

    [Required(ErrorMessage = "O campo \"Data de Conclusão\" deve ser preenchido.")]
    DateTime DataConclusao,

    bool StatusConclusao,

    [Range(0, 100, ErrorMessage = "O campo \"Percentual Concluído\" deve estar entre 0 e 100.")]
    int PercentualConcluido,

    [ValidateNever]
    List<ItensDeTarefasViewModel> Itens
);

public record ExcluirTarefaViewModel(
    Guid Id,
    string Titulo,
    PrioridadeTarefa PrioridadeTarefa,
    DateTime DataConclusao,
    bool StatusConclusao,
    int PercentualConcluido
);

public record DetalhesTarefaViewModel(
    Guid Id,
    string Titulo,
    PrioridadeTarefa PrioridadeTarefa,
    DateTime DataCriacao,
    DateTime DataConclusao,
    bool StatusConclusao,
    int PercentualConcluido,
    List<ItensDeTarefasViewModel> Itens
);