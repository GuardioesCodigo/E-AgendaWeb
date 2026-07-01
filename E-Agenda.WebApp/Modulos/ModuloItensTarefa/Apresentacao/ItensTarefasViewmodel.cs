using System.ComponentModel.DataAnnotations;

namespace E_Agenda.WebApp.Modulos.ModuloItensTarefa.Apresentacao
{
    public record ItensDeTarefasViewModel(
    Guid? Id,
    string Titulo,
    bool StatusConclusao,
    Guid ItensDeTarefaId = default
);
}