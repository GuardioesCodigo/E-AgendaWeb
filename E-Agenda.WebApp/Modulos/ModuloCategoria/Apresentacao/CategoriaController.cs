using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloCategoria.Aplicacao;
using FluentResults;
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

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarCategoriasViewModel cadastrarVm = new CadastrarCategoriasViewModel(string.Empty);
 
        return View(cadastrarVm);
    }
 
    [HttpPost]
    public ActionResult Cadastrar(CadastrarCategoriasViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);
 
        CadastrarCategoriasDto dtos = mapeador.Map<CadastrarCategoriasDto>(cadastrarVm);
        Result resultado = servicoCategorias.Cadastrar(dtos);
 
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
}