using System.ComponentModel.DataAnnotations;
using E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionarios.Apresentacao;

public class CadastrarContatosViewModel
{
    [Required(ErrorMessage = "O assunto é obrigatório.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O assunto deve ter entre 2 e 100 caracteres.")]
    public string Assunto { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data é obrigatória.")]
    public DateTime Data { get; set; }

    [Required(ErrorMessage = "A hora de início é obrigatória.")]
    public TimeSpan HoraInicio { get; set; }

    [Required(ErrorMessage = "A hora de término é obrigatória.")]
    public TimeSpan HoraTermino { get; set; }

    [Required(ErrorMessage = "O tipo de compromisso é obrigatório.")]
    public TipoCompromisso Tipo { get; set; }

    public string? Local { get; set; }
}

public class ListarCompromissoViewModel
{
    public Guid Id { get; set; }
    public string Assunto { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TipoCompromisso Tipo { get; set; }
}

public class ExcluirCompromissoViewModel
{
    public Guid Id { get; set; }
    public string Assunto { get; set; } = string.Empty;
    public DateTime Data { get; set; }
}

public class EditarContatosViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O assunto é obrigatório.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O assunto deve ter entre 2 e 100 caracteres.")]
    public string Assunto { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data é obrigatória.")]
    public DateTime Data { get; set; }

    [Required(ErrorMessage = "A hora de início é obrigatória.")]
    public TimeSpan HoraInicio { get; set; }

    [Required(ErrorMessage = "A hora de término é obrigatória.")]
    public TimeSpan HoraTermino { get; set; }

    [Required(ErrorMessage = "O tipo de compromisso é obrigatório.")]
    public TipoCompromisso Tipo { get; set; }

    public string? Local { get; set; }
}