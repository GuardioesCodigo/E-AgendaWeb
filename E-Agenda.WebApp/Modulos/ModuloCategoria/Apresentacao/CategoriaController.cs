using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloCategoria.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace E_Agenda.WebApp.Modulos.ModuloCategoria.Apresentacao;

public class CategoriasController(IMapper mapeador, ServicoCategoria servicoCategorias) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarCategoriasDto> dtos = servicoCategorias.SelecionarTodos();

        List<ListarCategoriasViewModel> listarVms = mapeador.Map<List<ListarCategoriasDto>>(dtos)
            .Select(c => new ListarCategoriasViewModel(c.Id, c.Titulo))
            .ToList();

        return View(listarVms);
    }
}