using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Aplicacao;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Dominio;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Apresentacao;

namespace E_Agenda.WebApp.Modulos.ModuloCompromisso.Apresentacao
{
    [Route("Contatos/[action]/{id?}")]
    public class ContatosController : Controller
    [Route("Compromissos/[action]/{id?}")]
    public class CompromissoController : Controller
    {
        private readonly ServicoCompromisso _servico;
        private readonly IMapper _mapper;

        public CompromissoController(ServicoCompromisso servico, IMapper mapper)
        {
            _servico = servico;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var compromissos = _servico.SelecionarTodos();
            var model = _mapper.Map<IEnumerable<ListarCompromissoViewModel>>(compromissos);
            return View(model);
        }

        [HttpGet]
        public IActionResult Cadastrar() => View(new CadastrarCompromissoViewModel());

        // No ContatosController.cs
[HttpPost]
public IActionResult Cadastrar(CadastrarContatosViewModel model)
{
    if (!ModelState.IsValid) return View(model);

    // O Controller mapeia UMA VEZ
    var contato = _mapper.Map<Contatos>(model);

    try
    {
        // O Serviço recebe o objeto já mapeado e o valida
        _servico.Cadastrar(contato); 
        return RedirectToAction("Listar");
    }
    catch (Exception ex)
    {
        ModelState.AddModelError("", ex.Message);
        return View(model);
    }
}
        [HttpPost]
        public IActionResult Cadastrar(CadastrarCompromissoViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // Mapeia no Controller para o Domínio
            var compromisso = _mapper.Map<Compromisso>(model);

            try
            {
                _servico.Cadastrar(compromisso); // O serviço recebe a entidade
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
            var contato = _servico.SelecionarPorId(id);
            if (contato == null) return NotFound();

            var model = _mapper.Map<EditarContatosViewModel>(contato);
            var compromisso = _servico.SelecionarPorId(id);
            if (compromisso == null) return NotFound();

            var model = _mapper.Map<EditarCompromissoViewModel>(compromisso);
            return View(model);
        }

        [HttpPost]
        public IActionResult Editar(Guid id, EditarCompromissoViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                model.Id = id;
                // O serviço recebe a ViewModel e mapeia internamente (padrão Contatos)
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
            var compromisso = _servico.SelecionarPorId(id);
            if (compromisso == null) return NotFound();

            var model = _mapper.Map<ExcluirCompromissoViewModel>(compromisso);
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