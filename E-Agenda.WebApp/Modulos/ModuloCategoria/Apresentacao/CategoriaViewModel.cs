using System.ComponentModel.DataAnnotations;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Apresentacao;

namespace E_Agenda.WebApp.Modulos.ModuloCategoria.Apresentacao;

public record ListarCategoriasViewModel(
    Guid Id,
    string Titulo,
    int QuantidadeDespesasVinculadas
);
 
 public record CadastrarCategoriasViewModel(
    [Required(ErrorMessage = "O campo \"Título\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Título\" deve conter entre 2 e 100 caracteres.")]
    string Titulo
);

public record EditarCategoriasViewModel(
    Guid Id,
 
    [Required(ErrorMessage = "O campo \"Título\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Título\" deve conter entre 2 e 100 caracteres.")]
    string Titulo
);

public record ExcluirCategoriasViewModel(
    Guid Id,
    string Titulo,
    int QuantidadeDespesasVinculadas
);

public record DetalhesCategoriaViewModel(
    Guid Id,
    string Titulo,
    List<ListarDespesasViewModel> Despesas
);