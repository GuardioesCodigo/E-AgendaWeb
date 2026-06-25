using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace E_Agenda.WebApp.Modulos.ModuloDespesas.Apresentacao;

public class DespesaController(ServicoDespesa servicoDespesa, IMapper mapeador) : Controller
{
        [HttpGet]
    public ActionResult Listar()
    {
        List<ListarDespesaDto> dtos = servicoDespesa.SelecionarTodos();
 
        List<ListarDespesasViewModel> listarVms = mapeador.Map<List<ListarDespesaDto>>(dtos)
            .Select(d => new ListarDespesasViewModel(
                d.Id, d.Descricao, d.DataOcorrencia, d.Valor, d.FormaPagamento, d.CategoriaId, d.CategoriaTitulo))
            .ToList();
 
        return View(listarVms);
    }

}

