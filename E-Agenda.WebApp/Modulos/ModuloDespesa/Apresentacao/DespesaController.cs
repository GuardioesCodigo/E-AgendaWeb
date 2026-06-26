using AutoMapper;
using E_Agenda.WebApp.Compartilhado.Apresentacao.Extensions;
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
            Guid.Empty,
            ObterCategoriasDisponiveis()
        );

        ViewBag.Categorias = ObterCategoriasDisponiveis();
 
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

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesDespesaDto> resultado = servicoDespesa.SelecionarPorId(id);
 
        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
 
            return RedirectToAction(nameof(Listar));
        }
 
        EditarDespesaViewModel editarVm = mapeador.Map<EditarDespesaViewModel>(resultado.Value);
 
        return View(editarVm);
    }
 
    [HttpPost]
    public ActionResult Editar(EditarDespesaViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);
 
        EditarDespesaDto dto = mapeador.Map<EditarDespesaDto>(editarVm);

        Result resultado = servicoDespesa.Editar(dto);
 
        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            ModelState.Remove(nameof(CadastrarDespesaViewModel.Categorias));
 
            return View(editarVm);
        }
 
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesDespesaDto> resultado = servicoDespesa.SelecionarPorId(id);
 
        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
 
            return RedirectToAction(nameof(Listar));
        }
 
        ExcluirDespesaViewModel excluirVm = mapeador.Map<ExcluirDespesaViewModel>(resultado.Value);
 
        return View(excluirVm);
    }
 
    [HttpPost]
    public ActionResult Excluir(ExcluirDespesaViewModel excluirVm)
    {
        Result resultado = servicoDespesa.Excluir(excluirVm.Id);
 
        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);
 
        return RedirectToAction(nameof(Listar));
    }

    private List<OpcaoCategoriaViewModel> ObterCategoriasDisponiveis()
    {
        List<OpcaoCategoriaDto> dtos = servicoDespesa.SelecionarCategorias();

        return mapeador.Map<List<OpcaoCategoriaViewModel>>(dtos);
    }
    
    [HttpGet]
    public ActionResult Visualizar(Guid id)
    {
        Result<DetalhesDespesaDto> resultado = servicoDespesa.SelecionarPorId(id);

        if (resultado.IsFailed)
            return RedirectToAction(nameof(Listar));

        DetalhesDespesaViewModel vm = mapeador.Map<DetalhesDespesaViewModel>(resultado.Value);

        return View(vm);
    }
}

