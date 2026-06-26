using Microsoft.AspNetCore.Mvc;
using E_Agenda.WebApp.Compartilhado.Apresentacao.Extensions;
using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Aplicacao;

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

}

