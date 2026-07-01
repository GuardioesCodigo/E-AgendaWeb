using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloItensTarefa.Dominio;
using E_Agenda.WebApp.Modulos.ModuloItensTarefa.Apresentacao;

namespace E_Agenda.WebApp.Modulos.ModuloItensTarefa.Apresentacao.Mapping;

public class ItensTarefaProfile : Profile
{
   public ItensTarefaProfile()
    {
        // Mapeia do Domínio PARA a ViewModel
        CreateMap<ItensDeTarefas, ItensDeTarefasViewModel>()
            .ForMember(dest => dest.ItensDeTarefaId, opt => opt.MapFrom(src => src.ItensDeTarefaId)) // Use src.TarefaId (o ID da tarefa no seu domínio)
            .ForMember(dest => dest.StatusConclusao, opt => opt.MapFrom(src => src.StatusConclusao));

        // Mapeia da ViewModel PARA o Domínio
        CreateMap<ItensDeTarefasViewModel, ItensDeTarefas>()
            .ForMember(dest => dest.ItensDeTarefaId, opt => opt.MapFrom(src => src.ItensDeTarefaId)) // Destino é dest.ItensDeTarefaId
            .ForMember(dest => dest.StatusConclusao, opt => opt.MapFrom(src => src.StatusConclusao));
    }
}