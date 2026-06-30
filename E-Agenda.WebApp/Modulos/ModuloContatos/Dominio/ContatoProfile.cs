using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloContatos.Apresentacao;
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

        // Se você tiver uma classe separada para Listar, mantenha assim:
        // CreateMap<Funcionario, ListarFuncionarioViewModel>();
    }
}