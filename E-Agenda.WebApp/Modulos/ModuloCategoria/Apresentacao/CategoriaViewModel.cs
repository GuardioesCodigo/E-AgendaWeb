using System.ComponentModel.DataAnnotations;

namespace E_Agenda.WebApp.Modulos.ModuloCategoria.Apresentacao;

public record ListarCategoriasViewModel(
    Guid Id,
    string Titulo
);
 
 public record CadastrarCategoriasViewModel(
    [Required(ErrorMessage = "O campo \"Título\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Título\" deve conter entre 2 e 100 caracteres.")]
    string Titulo
);