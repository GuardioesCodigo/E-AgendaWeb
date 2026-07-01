using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Dominio;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Apresentacao;

namespace E_Agenda.WebApp.Modulos.ModuloCompromisso.Dominio;

public class CompromissoProfile : Profile
{
    public CompromissoProfile()
    {
      
       CreateMap<Compromisso, ListarCompromissoViewModel>();

        // Necessário para o Cadastrar
        CreateMap<CadastrarCompromissoViewModel, Compromisso>();

        CreateMap<EditarCompromissoViewModel, Compromisso>().ReverseMap();

        CreateMap<Compromisso, ExcluirCompromissoViewModel>();
        
      
    }
}