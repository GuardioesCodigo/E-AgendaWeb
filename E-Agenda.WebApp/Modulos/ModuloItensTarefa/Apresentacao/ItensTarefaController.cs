using Microsoft.AspNetCore.Mvc;
using E_Agenda.WebApp.Modulos.ModuloItensTarefa.Dominio;
using E_Agenda.WebApp.Modulos.ModuloItensTarefa.Apresentacao;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Dominio;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Aplicacao;

namespace E_Agenda.WebApp.Modulos.ModuloItensTarefa.Apresentacao;

public class ItensTarefaController : Controller
{
    private readonly ServicoTarefa servicoTarefa;
    private readonly IRepositorioTarefa _repositorioTarefa;

    public ItensTarefaController(IRepositorioTarefa repositorioTarefa, ServicoTarefa servicoTarefa)
    {
        _repositorioTarefa = repositorioTarefa;
        this.servicoTarefa = servicoTarefa;
    }

    // GET: Cadastrar (Exibe a tela de cadastro)
    public IActionResult Cadastrar(Guid tarefaId)
    {
        // Cria um modelo inicial com o ID da tarefa pai
        var vm = new ItensDeTarefasViewModel(null, "", false, tarefaId);
        return View("Formulario", vm);
    }

    // POST: Cadastrar (Processa o salvamento)
    [HttpPost]
    public IActionResult Cadastrar(ItensDeTarefasViewModel vm)
    {
        if (!ModelState.IsValid) return View("Formulario", vm);

        var tarefa = _repositorioTarefa.SelecionarPorId(vm.ItensDeTarefaId);
        if (tarefa == null) return NotFound();

        var novoItem = new ItensDeTarefas(vm.Titulo);
        tarefa.AdicionarItem(novoItem); // Certifique-se que este método existe na classe Tarefa
        
        tarefa.PercentualConcluido = CalcularPercentual(tarefa);
        _repositorioTarefa.Editar(tarefa.Id, tarefa);
        
        // Redireciona de volta para a edição da tarefa pai
        return RedirectToAction("Editar", "Tarefa", new { id = vm.ItensDeTarefaId });
    }

    // GET: Editar
    public IActionResult Editar(Guid id, Guid tarefaId)
    {
        var tarefa = _repositorioTarefa.SelecionarPorId(tarefaId);
        var item = tarefa?.ItemTarefa.FirstOrDefault(i => i.Id == id);
        
        if (item == null) return NotFound();

        var vm = new ItensDeTarefasViewModel(item.Id, item.Titulo, item.StatusConclusao, tarefaId);
        return View("Formulario", vm);
    }

    // POST: Editar
    [HttpPost]
    public IActionResult Editar(ItensDeTarefasViewModel vm)
    {
        if (!ModelState.IsValid) return View("Formulario", vm);

        var tarefa = _repositorioTarefa.SelecionarPorId(vm.ItensDeTarefaId);
        if (tarefa == null) return NotFound();

        var item = tarefa.ItemTarefa.FirstOrDefault(i => i.Id == vm.Id);
        
        if (item != null)
        {
            item.Titulo = vm.Titulo;
            item.StatusConclusao = vm.StatusConclusao;
            
            tarefa.PercentualConcluido = CalcularPercentual(tarefa);
            _repositorioTarefa.Editar(tarefa.Id, tarefa);
        }

        return RedirectToAction("Editar", "Tarefa", new { id = vm.ItensDeTarefaId });
    }

    private decimal CalcularPercentual(Tarefa tarefa)
    {
        if (tarefa.ItemTarefa.Count == 0) return 0;
        var concluidos = tarefa.ItemTarefa.Count(i => i.StatusConclusao);
        return (decimal)concluidos * 100 / tarefa.ItemTarefa.Count;
    }
}