using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloCategoria.Aplicacao;
using E_Agenda.WebApp.Modulos.ModuloCategoria.Dominio;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Aplicacao;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Dominio;
using FluentResults;
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

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarDespesaViewModel cadastrarVm = new CadastrarDespesaViewModel(
            string.Empty,
            null,
            0,
            FormaPagamento.AVista,
            ObterCategoriasDisponiveis()
        );
 
        return View(cadastrarVm);
    }
 
    [HttpPost]
    public ActionResult Cadastrar(CadastrarDespesaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);
 
        CadastrarDespesaDto dto = mapeador.Map<CadastrarDespesaDto>(cadastrarVm);
        Result resultado = servicoDespesa.Cadastrar(dto);
 
        if (resultado.IsFailed)
        {
            foreach (IError erro in resultado.Errors)
            {
                string campo =
                    erro.Metadata["Campo"] is string
                        ? erro.Metadata["Campo"].ToString()!
                        : string.Empty;
 
                ModelState.AddModelError(campo, erro.Message);
            }
 
            return View(cadastrarVm);
        }
 
        return RedirectToAction(nameof(Listar));
    }

    private List<OpcaoCategoriaViewModel> ObterCategoriasDisponiveis()
    {
        List<OpcaoCategoriaDto> dtos = servicoDespesa.SelecionarCategorias();

        return mapeador.Map<List<OpcaoCategoriaViewModel>>(dtos);
    }
}

