using System.ComponentModel.DataAnnotations;

namespace E_Agenda.WebApp.Modulos.ModuloContatos.Apresentacao;

public class CadastrarContatosViewModel
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Formato de telefone inválido.")]
    [Required(ErrorMessage = "O telefone é obrigatório.")]
    [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$", ErrorMessage = "Use o formato (99) 99999-9999")]
    public string Telefone { get; set; } = string.Empty;
    public string? Cargo { get; set; } = string.Empty;
    public string? Empresa { get; set; } = string.Empty;

}

public class ListarContatosViewModel
{
    public Guid Id { get; set; } // Necessário para gerar o link de Editar/Excluir

    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public string Empresa { get; set; } = string.Empty;
}

public class VisualizarContatoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cargo { get; set; }
        public string Empresa { get; set; }
    }
    
public class ExcluirContatosViewModel
{
    public string Email { get; set; } = string.Empty;

    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public string Empresa { get; set; } = string.Empty;
}


public class EditarContatosViewModel
{
    public Guid Id { get; set; } // Obrigatório para o Editar

    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Formato de telefone inválido.")]
    [Required(ErrorMessage = "O telefone é obrigatório.")]
    [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$", ErrorMessage = "Use o formato (99) 99999-9999")]
    public string Telefone { get; set; } = string.Empty;
    public string? Cargo { get; set; } = string.Empty;
    public string? Empresa { get; set; } = string.Empty;

}