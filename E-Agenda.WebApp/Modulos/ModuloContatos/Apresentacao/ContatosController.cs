using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;
using E_Agenda.WebApp.Modulos.ModuloContatos.Aplicacao;
using E_Agenda.WebApp.Modulos.ModuloContatos.Apresentacao;

namespace E_Agenda.WebApp.Modulos.ModuloContatos.Apresentacao
{
    [Route("Contatos/[action]/{id?}")]
    public class ContatosController : Controller
    {
        private readonly ServicoContatos _servico;
        private readonly IMapper _mapper;

        public ContatosController(ServicoContatos servico, IMapper mapper)
        {
            _servico = servico;
            _mapper = mapper;
        }

        public IActionResult Listar()
        {
            var contatos = _servico.SelecionarTodos();
            var model = _mapper.Map<IEnumerable<ListarContatosViewModel>>(contatos);
            return View(model);
        }

        [HttpGet]
        public IActionResult Cadastrar() => View(new CadastrarContatosViewModel());

        // No ContatosController.cs
[HttpPost]
public IActionResult Cadastrar(CadastrarContatosViewModel model)
{
    if (!ModelState.IsValid) return View(model);

            try
            {
                _servico.Cadastrar(model); 
                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Editar(Guid id)
        {
            // Alterado de 'funcionario' para 'contato'
            var contato = _servico.SelecionarPorId(id);
            if (contato == null) return NotFound();

            var model = _mapper.Map<CadastrarContatosViewModel>(contato);
            return View(model);
        }

        [HttpPost]
        public IActionResult Editar(Guid id, EditarContatosViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                model.Id = id;
                _servico.Editar(model); 
                
                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Excluir(Guid id)
        {
            // Alterado de 'funcionario' para 'contato'
            var contato = _servico.SelecionarPorId(id);
            if (contato == null) return NotFound();

            var model = _mapper.Map<ExcluirContatosViewModel>(contato);
            return View(model);
        }

        [HttpPost, ActionName("Excluir")]
        public IActionResult ConfirmarExcluir(Guid id)
        {
            _servico.Excluir(id);
            return RedirectToAction("Listar");
        }
    }
}