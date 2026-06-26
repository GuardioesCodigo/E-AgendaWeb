using System;
using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloCategoria.Aplicacao;
using E_Agenda.WebApp.Modulos.ModuloCategoria.Apresentacao;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Aplicacao;

namespace E_Agenda.WebApp.Modulos.ModuloDespesas.Apresentacao;

public class DespesaProfile : Profile
{
    public DespesaProfile()
    {
        CreateMap<ListarDespesaDto, ListarDespesasViewModel>();
        CreateMap<CadastrarDespesaViewModel, CadastrarDespesaDto>();
        CreateMap<EditarDespesaViewModel, EditarDespesaDto>();

        CreateMap<OpcaoCategoriaDto, OpcaoCategoriaViewModel>();
 
        CreateMap<DetalhesDespesaDto, EditarDespesaViewModel>()
            .ForCtorParam(nameof(EditarDespesaViewModel.Categorias),
                opt => opt.MapFrom(src => new List<OpcaoCategoriaViewModel>()));
        CreateMap<DetalhesDespesaDto, ExcluirDespesaViewModel>();

        CreateMap<ListarDespesaDto, ListarDespesasViewModel>();
    }
}
