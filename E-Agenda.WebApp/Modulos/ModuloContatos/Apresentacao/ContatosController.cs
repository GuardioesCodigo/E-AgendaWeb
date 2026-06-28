using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;

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

        [HttpPost]
        public IActionResult Cadastrar(CadastrarContatosViewModel model)
    {
        // 1. Validação automática dos Data Annotations da ViewModel
        if (!ModelState.IsValid) return View(model);

        // 2. Mapeia para a entidade de Domínio
        var contato = _mapper.Map<Contatos>(model);

        // 3. CHAMA A SUA VALIDAÇÃO MANUAL
        List<string> erros = contato.Validar();

        // 4. Se houver erros, devolve para a tela
        if (erros.Count > 0)
        {
            foreach (var erro in erros)
            {
                ModelState.AddModelError("", erro); // Adiciona o erro ao ModelState
            }
            return View(model); // Retorna a tela com a lista de erros
        }

        try
        {
            _servico.Cadastrar(contato);
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
            var funcionario = _servico.SelecionarPorId(id);
            if (funcionario == null) return NotFound();

            var model = _mapper.Map<ExcluirContatosViewModel>(funcionario);
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