namespace E_Agenda.WebApp.Modulos.ModuloContatos.DTOs;

public class ContatosDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public string Empresa { get; set; } = string.Empty;
    
    // Opcional: Se precisar passar a lista de compromissos para a View
    // public List<CompromissoDTO> Compromissos { get; set; } = new();
}