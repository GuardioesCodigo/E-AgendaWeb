using E_Agenda.WebApp.Modulos.ModuloDespesas.Apresentacao;

namespace E_Agenda.WebApp.Modulos.ModuloCategoria.Aplicacao;

public record DetalhesCategoriasDto(
    Guid Id,
    string Titulo,
    List<ListarDespesasViewModel> Despesas
);

public record ListarCategoriasDto(
    Guid Id,
    string Titulo,
    int QuantidadeDespesasVinculadas
);

public record CadastrarCategoriasDto(
    string Titulo
);

public record EditarCategoriasDto(
    Guid Id,
    string Titulo
);
