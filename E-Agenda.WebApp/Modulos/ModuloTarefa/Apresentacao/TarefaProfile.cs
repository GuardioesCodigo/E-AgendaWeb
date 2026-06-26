using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Aplicacao;

namespace E_Agenda.WebApp.Modulos.ModuloTarefa.Apresentacao;

public class MapeadorTarefa : Profile
{
    public MapeadorTarefa()
    {
        CreateMap<ListarTarefaDto, ListarTarefaViewModel>();
    }
}
