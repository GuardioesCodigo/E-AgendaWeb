using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;

using E_Agenda.WebApp.Modulos.ModuloContatos.Apresentacao;
using E_Agenda.WebApp.Modulos.ModuloContatos.Aplicacao;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Aplicacao;

namespace E_Agenda.WebApp.Modulos.ModuloContatos.Apresentacao
{
    [Route("Contatos/[action]/{id?}")]
    public class ContatosController : Controller
    {
        private readonly ServicoContatos _servico;
        private readonly IMapper _mapper;
        private readonly ServicoCompromisso _servicoCompromisso;

        public ContatosController(ServicoContatos servico, IMapper mapper, ServicoCompromisso servicoCompromisso)
        {
           _servico = servico;
           _servicoCompromisso = servicoCompromisso; // <--- A LINHA MÁGICA
           _mapper = mapper;
        }

        public IActionResult Listar()
        {
            var contatos = _servico.SelecionarTodos();
            var model = _mapper.Map<IEnumerable<ListarContatosViewModel>>(contatos);
            return View(model);
        }

[HttpGet]
public IActionResult Cadastrar()
{
    var vm = new CadastrarContatosViewModel();
    
    // Busca todos os compromissos que não têm contato (ou todos)
    var todosCompromissos = _servicoCompromisso.SelecionarTodos();
    
    vm.Compromissos = todosCompromissos.Select(c => new CompromissoCheckboxViewModel
    {
        Id = c.Id,
        Assunto = c.Assunto
    }).ToList();
    
    return View(vm);
}
        // No ContatosController.cs
[HttpPost]
public IActionResult Cadastrar(CadastrarContatosViewModel model)
{
    if (!ModelState.IsValid)
    {
        // ERRO ESTAVA AQUI: Você precisa converter a entidade de volta para ViewModel
        // ou simplesmente retornar o 'model' original que já veio da tela.
        return View(model); 
    }

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
public IActionResult Visualizar(Guid id)
{
    var contato = _servico.SelecionarPorId(id);
    
    if (contato == null)
    {
        TempData["Erro"] = "Contato não encontrado.";
        return RedirectToAction("Listar");
    }

    // Mapeia para o ViewModel que criamos
    var model = _mapper.Map<VisualizarContatoViewModel>(contato);

    // Filtra os compromissos vinculados a este contato
    model.Compromissos = _servicoCompromisso.SelecionarTodos()
        .Where(c => c.ContatoId == id)
        .Select(c => c.Assunto)
        .ToList();

    return View(model);
}

        [HttpGet]
        public IActionResult Editar(Guid id)
        {
            var contato = _servico.SelecionarPorId(id);
            if (contato == null) return NotFound();

            var model = _mapper.Map<EditarContatosViewModel>(contato);
            return View(model);
        }

        [HttpPost]
        public IActionResult Editar(Guid id, EditarContatosViewModel model)
        {
            // 1. O tipo aqui deve ser o seu modelo de Edição
            if (!ModelState.IsValid) return View(model);

            try
            {
                // 2. Garanta que o ID da URL seja passado para o modelo
                model.Id = id;

                // 3. O Controller não deve mapear para Funcionario aqui.
                // O seu serviço já faz o trabalho de Mapear e Salvar!
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
            var contato = _servico.SelecionarPorId(id);
            
            if (contato == null)
                return RedirectToAction("Listar");

            // Aqui você transforma a entidade em ViewModel para a View
            var model = _mapper.Map<ExcluirContatosViewModel>(contato);
            
            return View(model); // Agora ele vai encontrar a view 'Excluir.cshtml' que você criar
        }

        [HttpPost]
        public IActionResult Excluir(ExcluirContatosViewModel model)
        {
            try
            {
                _servico.Excluir(model.Id);
                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                // Se ocorrer um erro (como ter vínculo com compromisso), volta para a lista com a mensagem
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Listar");
            }
        }
    }
}