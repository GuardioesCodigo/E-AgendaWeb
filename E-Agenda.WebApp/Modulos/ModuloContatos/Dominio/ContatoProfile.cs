using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloContatos.Apresentacao;
using E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;

public class ContatosProfile : Profile
{
    public ContatosProfile()
    {
        // Mapeamentos para Cadastrar e Editar
        // Usamos .ForMember(..., opt => opt.Ignore()) para o AutoMapper não tentar
        // converter a lista de Checkboxes para a lista de Entidades de Compromisso.
        
        CreateMap<CadastrarContatosViewModel, Contatos>()
            .ForMember(dest => dest.Compromissos, opt => opt.Ignore());
            
        CreateMap<Contatos, CadastrarContatosViewModel>();

        CreateMap<EditarContatosViewModel, Contatos>()
            .ForMember(dest => dest.Compromissos, opt => opt.Ignore());
            
        CreateMap<Contatos, EditarContatosViewModel>();

        // Demais mapeamentos
        CreateMap<Contatos, ExcluirContatosViewModel>();
        CreateMap<Contatos, ListarContatosViewModel>();
        CreateMap<Contatos, VisualizarContatoViewModel>();
    }
}