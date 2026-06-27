using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloCompromissos.Aplicacao;
using E_Agenda.WebApp.Modulos.ModuloCompromissos.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloCompromissos.Apresentacao
{
    public class CompromissoController : Controller
    {
        private readonly ServicoCompromisso _servico;
        private readonly IMapper _mapper;

        public CompromissoController(ServicoCompromisso servico, IMapper mapper)
        {
            _servico = servico;
            _mapper = mapper;
        }

        public IActionResult Listar()
        {
                    // 1. Chame o serviço
            var listaCompromissos = _servico.SelecionarTodos(); 
            
            // 2. O AutoMapper precisa converter de 'Compromisso' (Domínio) para 'ListarCompromissoViewModel'
            var model = _mapper.Map<IEnumerable<ListarCompromissoViewModel>>(listaCompromissos);
            
            // 3. Retorne o modelo mapeado
            return View(model);
        }

        [HttpGet]
        public IActionResult Cadastrar() => View(new CadastrarCompromissoViewModel());

        [HttpPost]
        public IActionResult Cadastrar(CadastrarCompromissoViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                // O controller apenas passa o modelo para o serviço
               var dto = _mapper.Map<CadastrarCompromissoDto>(model);
               _servico.Cadastrar(dto);

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
            if (compromisso is null) return NotFound();

            var model = _mapper.Map<EditarCompromissoViewModel>(compromisso);
            return View(model);
        }

        [HttpPost]
        public IActionResult Editar(Guid id, EditarCompromissoViewModel model)
        {
            // 1. O tipo aqui deve ser o seu modelo de Edição
            if (!ModelState.IsValid) return View(model);

            try
            {
                // 2. Garanta que o ID da URL seja passado para o modelo
                model.Id = id;

                // 3. O Controller não deve mapear para Funcionario aqui.
                // O seu serviço já faz o trabalho de Mapear e Salvar!
                var dto = _mapper.Map<EditarCompromissoDto>(model);

                _servico.Editar(dto);
                
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