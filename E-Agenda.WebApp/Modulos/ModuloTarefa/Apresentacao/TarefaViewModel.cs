using System;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloTarefa.Apresentacao;

public record ListarTarefaViewModel(
    Guid Id,
    string Titulo,
    PrioridadeTarefa PrioridadeTarefa,
    DateTime DataConclusao,
    bool StatusConclusao,
    int PercentualConcluido
);
