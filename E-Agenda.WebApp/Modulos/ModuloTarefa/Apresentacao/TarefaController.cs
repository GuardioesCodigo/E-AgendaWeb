using Microsoft.AspNetCore.Mvc;
using E_Agenda.WebApp.Compartilhado.Apresentacao.Extensions;
using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Aplicacao;
using FluentResults;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloTarefa.Apresentacao;

public class TarefaController(IMapper mapeador, ServicoTarefa servicoTarefa) : Controller
{
    
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarTarefaDto> dtos = servicoTarefa.SelecionarTodos();

        List<ListarTarefaViewModel> listarVms = mapeador.Map<List<ListarTarefaViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarTarefaViewModel cadastrarVm = new CadastrarTarefaViewModel(
            string.Empty,
            PrioridadeTarefa.Normal,
            DateTime.Today,
            new List<ItemTarefaViewModel>()
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarTarefaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarTarefaDto dto = mapeador.Map<CadastrarTarefaDto>(cadastrarVm);
        Result resultado = servicoTarefa.Cadastrar(dto);

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
        Result<DetalhesTarefaDto> resultado = servicoTarefa.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        EditarTarefaViewModel editarVm = mapeador.Map<EditarTarefaViewModel>(resultado.Value);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarTarefaViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarTarefaDto dto = mapeador.Map<EditarTarefaDto>(editarVm);
        Result resultado = servicoTarefa.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesTarefaDto> resultado = servicoTarefa.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ExcluirTarefaViewModel excluirVm = mapeador.Map<ExcluirTarefaViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirTarefaViewModel excluirVm)
    {
        Result resultado = servicoTarefa.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Visualizar(Guid id)
    {
        Result<DetalhesTarefaDto> resultado = servicoTarefa.SelecionarPorId(id);

        if (resultado.IsFailed)
            return RedirectToAction(nameof(Listar));

        DetalhesTarefaViewModel vm = mapeador.Map<DetalhesTarefaViewModel>(resultado.Value);

        return View(vm);
    }
}

