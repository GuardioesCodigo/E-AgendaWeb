using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloCompromissos.Dominio;
using E_Agenda.WebApp.Modulos.ModuloCompromissos.Apresentacao;

namespace E_Agenda.WebApp.Modulos.ModuloCompromissos.Dominio;

public class CompromissoProfile : Profile
{
    public CompromissoProfile()
    {
        // Se você não tiver uma ViewModel de Listagem específica, 
        // use a FuncionarioViewModel para tudo
        CreateMap<Compromisso, CadastrarCompromissoViewModel>().ReverseMap();
        CreateMap<Compromisso, ExcluirCompromissoViewModel>();
        CreateMap<Compromisso, EditarCompromissoViewModel>().ReverseMap();

        
        // Se você tiver uma classe separada para Listar, mantenha assim:
        // CreateMap<Funcionario, ListarFuncionarioViewModel>();
    }
}