namespace E_Agenda.WebApp.Modulos.ModuloItensTarefa.DTOs;

public class ItensTarefaDTO
{
    public Guid Id { get; set; }
    public Guid ItensTarefaId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public bool StatusConclusao { get; set; }
}