using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloCategoria.Aplicacao;

namespace E_Agenda.WebApp.Modulos.ModuloCategoria.Apresentacao;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<ListarCategoriasDto, ListarCategoriasViewModel>();
        CreateMap<CadastrarCategoriasViewModel, CadastrarCategoriasDto>();
        CreateMap<EditarCategoriasViewModel, EditarCategoriasDto>();

        CreateMap<DetalhesCategoriasDto, EditarCategoriasViewModel>();
        CreateMap<DetalhesCategoriasDto, ExcluirCategoriasViewModel>()
            .ForCtorParam(nameof(ExcluirCategoriasViewModel.QuantidadeDespesasVinculadas),
                opt => opt.MapFrom(src => src.Despesas.Count));

        CreateMap<DetalhesCategoriasDto, DetalhesCategoriaViewModel>();
    }
}
