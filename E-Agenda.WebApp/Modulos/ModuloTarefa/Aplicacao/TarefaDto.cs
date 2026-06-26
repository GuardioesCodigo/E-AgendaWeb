using E_Agenda.WebApp.Modulos.ModuloTarefa.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloTarefa.Aplicacao;

public record ListarTarefaDto(
    Guid Id,
    string Titulo,
    PrioridadeTarefa PrioridadeTarefa,
    DateTime DataConclusao,
    bool StatusConclusao,
    int PercentualConcluido
);

public record CadastrarItemTarefaDto(
    string Titulo
);

public record CadastrarTarefaDto(
    string Titulo,
    PrioridadeTarefa PrioridadeTarefa,
    DateTime DataConclusao,
    List<CadastrarItemTarefaDto> Itens
);

public record ItemTarefaDto(
    Guid Id,
    string Titulo,
    bool StatusConclusao
);

public record DetalhesTarefaDto(
    Guid Id,
    string Titulo,
    PrioridadeTarefa PrioridadeTarefa,
    DateTime DataCriacao,
    DateTime DataConclusao,
    bool StatusConclusao,
    int PercentualConcluido,
    List<ItemTarefaDto> Itens
);