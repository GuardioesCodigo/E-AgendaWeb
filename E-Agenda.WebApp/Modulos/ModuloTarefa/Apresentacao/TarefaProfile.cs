using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Aplicacao;
using E_Agenda.WebApp.Modulos.ModuloItensTarefa.Apresentacao;

namespace E_Agenda.WebApp.Modulos.ModuloTarefa.Apresentacao;

public class MapeadorTarefa : Profile
{
    public MapeadorTarefa()
    {
        CreateMap<ListarTarefaDto, ListarTarefaViewModel>();

        CreateMap<ItemTarefaDto, ItensDeTarefasViewModel>();

        CreateMap<DetalhesTarefaDto, EditarTarefaViewModel>();
        CreateMap<DetalhesTarefaDto, ExcluirTarefaViewModel>();
        CreateMap<DetalhesTarefaDto, DetalhesTarefaViewModel>();

        CreateMap<ItensDeTarefasViewModel, CadastrarItemTarefaDto>();
        CreateMap<CadastrarTarefaViewModel, CadastrarTarefaDto>();

        CreateMap<ItensDeTarefasViewModel, EditarItemTarefaDto>();
        CreateMap<EditarTarefaViewModel, EditarTarefaDto>();
    }
}
