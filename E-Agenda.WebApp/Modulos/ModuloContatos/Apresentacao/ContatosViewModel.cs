using System.ComponentModel.DataAnnotations;

namespace E_Agenda.WebApp.Modulos.ModuloContatos.Apresentacao;

public class CadastrarContatosViewModel
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Formato de telefone inválido.")]
    [Required(ErrorMessage = "O telefone é obrigatório.")]
    public string Telefone { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public string Empresa { get; set; } = string.Empty;

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
    public string Telefone { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public string Empresa { get; set; } = string.Empty;

}