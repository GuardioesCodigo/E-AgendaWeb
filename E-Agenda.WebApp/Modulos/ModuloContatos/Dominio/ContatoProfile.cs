using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloContatos.Apresentacao;
using E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;

public class ContatosProfile : Profile
{
    public ContatosProfile()
    {
        // Se você não tiver uma ViewModel de Listagem específica, 
        // use a FuncionarioViewModel para tudo
        CreateMap<Contatos, CadastrarContatosViewModel>().ReverseMap();
        CreateMap<CadastrarContatosViewModel, Contatos>();
        CreateMap<Contatos, ExcluirContatosViewModel>();
        CreateMap<Contatos, EditarContatosViewModel>().ReverseMap();
        CreateMap<Contatos, ListarContatosViewModel>();
        CreateMap<Contatos, VisualizarContatoViewModel>();

        CreateMap<EditarContatosViewModel, Contatos>()
            .ForMember(dest => dest.Compromissos, opt => opt.Ignore());
            
        CreateMap<Contatos, EditarContatosViewModel>();

    }
}