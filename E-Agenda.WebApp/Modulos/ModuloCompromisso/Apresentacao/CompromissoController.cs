using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Aplicacao;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Dominio;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Apresentacao;
using E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Agenda.WebApp.Modulos.ModuloCompromisso.Apresentacao
{
    [Route("Compromissos/[action]/{id?}")]
    public class CompromissoController : Controller
    {
        private readonly ServicoCompromisso _servico;
        private readonly IMapper _mapper;
        private readonly IRepositorioCompromisso _repositorioCompromisso;
        private readonly IRepositorioContatos _repositorioContato; 

        public CompromissoController(ServicoCompromisso servico, IMapper mapper,IRepositorioContatos repositorioContato)
        {
            _servico = servico;
            _mapper = mapper;
            _repositorioContato = repositorioContato;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var compromissos = _servico.SelecionarTodos();
            var model = _mapper.Map<IEnumerable<ListarCompromissoViewModel>>(compromissos);
            return View(model);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
             
            var vm = new CadastrarCompromissoViewModel();

            var contatos = _repositorioContato.SelecionarTodos();
            vm.Contatos = contatos.Select(c => new SelectListItem(c.Nome, c.Id.ToString())).ToList();
    
            return View(vm);
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarCompromissoViewModel model)
        {
           if (!ModelState.IsValid)
         {
            // Recarrega a lista antes de retornar a view com erro
            var contatos = _repositorioContato.SelecionarTodos();
            model.Contatos = contatos.Select(c => new SelectListItem(c.Nome, c.Id.ToString())).ToList();
            return View(model);
        }

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
           var compromisso = _servico.SelecionarPorId(id);
            if (compromisso == null) return NotFound();

            var model = _mapper.Map<EditarCompromissoViewModel>(compromisso);
            
            // --- Adicione isso para popular o Select na edição ---
            var contatos = _repositorioContato.SelecionarTodos();
            model.Contatos = contatos.Select(c => new SelectListItem(c.Nome, c.Id.ToString())).ToList();
    // -----------------------------------------------------

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
           try
            {
                _servico.Excluir(id);
                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                // Agora o erro é capturado e enviado via TempData
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Listar");
            }
        }
    }
}