using Microsoft.AspNetCore.Mvc;
using E_Agenda.WebApp.Compartilhado.Apresentacao.Extensions;
using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Aplicacao;
using FluentResults;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Dominio;
using E_Agenda.WebApp.Modulos.ModuloItensTarefa.Apresentacao;

namespace E_Agenda.WebApp.Modulos.ModuloTarefa.Apresentacao;

public class TarefaController(IMapper mapeador, ServicoTarefa servicoTarefa) : Controller
{
    
    [HttpGet]
    public ActionResult Listar(string filtro = "todas")
    {
        List<ListarTarefaDto> dtos = servicoTarefa.SelecionarTodos();

        List<ListarTarefaViewModel> listarVms = mapeador.Map<List<ListarTarefaViewModel>>(dtos);

        listarVms = filtro switch
        {
            "pendentes" => listarVms.Where(t => !t.StatusConclusao).ToList(),
            "concluidas" => listarVms.Where(t => t.StatusConclusao).ToList(),
            _ => listarVms
        };

        ViewBag.FiltroAtual = filtro;

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarTarefaViewModel cadastrarVm = new CadastrarTarefaViewModel(
            string.Empty,
            PrioridadeTarefa.Normal,
            DateTime.Today,
            new List<ItensDeTarefasViewModel>()
        );

        return View(cadastrarVm);
    }

   [HttpPost]
    public ActionResult Cadastrar(CadastrarTarefaViewModel cadastrarVm)
    {
        if (cadastrarVm.Itens == null)
            cadastrarVm = cadastrarVm with { Itens = new List<ItensDeTarefasViewModel>() };

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
        if (editarVm.Itens == null)
            editarVm = editarVm with { Itens = new List<ItensDeTarefasViewModel>() };

        if (!ModelState.IsValid)
        {
            foreach (var erro in ModelState.Where(x => x.Value?.Errors.Count > 0))
            {
                Console.WriteLine($"  {erro.Key}: {string.Join(", ", erro.Value!.Errors.Select(e => e.ErrorMessage))}");
            }

            return View(editarVm);
        }

        EditarTarefaDto dto = mapeador.Map<EditarTarefaDto>(editarVm);
        Result resultado = servicoTarefa.Editar(dto);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
            {
                Console.WriteLine($"  {erro.Message}");
            }

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

    [HttpPost]
    public ActionResult AdicionarItemCadastro(CadastrarTarefaViewModel cadastrarVm)
    {
        List<ItensDeTarefasViewModel> itens = cadastrarVm.Itens?.ToList() ?? new List<ItensDeTarefasViewModel>();
        itens.Add(new ItensDeTarefasViewModel(null, string.Empty, false));

        ModelState.Clear();

        return View("Cadastrar", cadastrarVm with { Itens = itens });
    }

    [HttpPost]
    public ActionResult RemoverItemCadastro(CadastrarTarefaViewModel cadastrarVm, int index)
    {
        List<ItensDeTarefasViewModel> itens = cadastrarVm.Itens?.ToList() ?? new List<ItensDeTarefasViewModel>();

        if (index >= 0 && index < itens.Count)
            itens.RemoveAt(index);

        ModelState.Clear();

        return View("Cadastrar", cadastrarVm with { Itens = itens });
    }

    [HttpPost]
    public ActionResult AdicionarItemEditar(EditarTarefaViewModel editarVm)
    {
        List<ItensDeTarefasViewModel> itens = editarVm.Itens?.ToList() ?? new List<ItensDeTarefasViewModel>();
        itens.Add(new ItensDeTarefasViewModel(null, string.Empty, false));

        ModelState.Clear();

        return View("Editar", editarVm with { Itens = itens });
    }

    [HttpPost]
    public ActionResult RemoverItemEditar(EditarTarefaViewModel editarVm, int index)
    {
        List<ItensDeTarefasViewModel> itens = editarVm.Itens?.ToList() ?? new List<ItensDeTarefasViewModel>();

        if (index >= 0 && index < itens.Count)
            itens.RemoveAt(index);

        ModelState.Clear();

        return View("Editar", editarVm with { Itens = itens });
    }

    [HttpPost]
    public ActionResult ConcluirItem(Guid tarefaId, Guid itemId)
    {
        Result resultado = servicoTarefa.ConcluirItem(tarefaId, itemId);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Visualizar), new { id = tarefaId });
    }
}