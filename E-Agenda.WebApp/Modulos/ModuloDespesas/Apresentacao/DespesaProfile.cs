using System;
using AutoMapper;
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
 
        CreateMap<DetalhesDespesaDto, EditarDespesaViewModel>();
        CreateMap<DetalhesDespesaDto, ExcluirDespesaViewModel>();
    }
}
