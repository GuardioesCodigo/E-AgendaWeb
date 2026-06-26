using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Aplicacao;

namespace E_Agenda.WebApp.Modulos.ModuloTarefa.Apresentacao;

public class MapeadorTarefa : Profile
{
    public MapeadorTarefa()
    {
        CreateMap<ListarTarefaDto, ListarTarefaViewModel>();

        CreateMap<ItemTarefaDto, ItemTarefaViewModel>();

        CreateMap<DetalhesTarefaDto, EditarTarefaViewModel>();
        CreateMap<DetalhesTarefaDto, ExcluirTarefaViewModel>();
        CreateMap<DetalhesTarefaDto, DetalhesTarefaViewModel>();

        CreateMap<ItemTarefaViewModel, CadastrarItemTarefaDto>();
        CreateMap<CadastrarTarefaViewModel, CadastrarTarefaDto>();

        CreateMap<ItemTarefaViewModel, EditarItemTarefaDto>();
        CreateMap<EditarTarefaViewModel, EditarTarefaDto>();
    }
}
