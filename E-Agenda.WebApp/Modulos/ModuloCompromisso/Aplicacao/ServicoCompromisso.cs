using System;
using System.Collections.Generic;
using E_Agenda.WebApp.Modulos.ModuloCompromissos.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloCompromissos.Aplicacao
{
    public class ServicoCompromisso
    {
        private readonly IRepositorioCompromisso _repositorio;

        public ServicoCompromisso(IRepositorioCompromisso repositorio)
        {
            _repositorio = repositorio;
        }

        public void Cadastrar(CadastrarCompromissoDto dto)
        {
            // 1. Instanciar a entidade CORRETA (Compromisso)
            var compromisso = new Compromisso(dto.Assunto, dto.Data, dto.HoraInicio, dto.HoraTermino, dto.Tipo, dto.Local);

            // 2. Validar
            List<string> erros = compromisso.Validar();

            if (erros.Count > 0)
                throw new Exception(string.Join(" | ", erros));

            // 3. Persistir
            compromisso.Id = Guid.NewGuid();
            _repositorio.Cadastrar(compromisso);
        }

        public void Editar(EditarCompromissoDto dto)
        {
            // 1. Buscar o existente
            var compromissoExistente = _repositorio.SelecionarPorId(dto.Id);

            if (compromissoExistente == null)
                throw new Exception("Compromisso não encontrado.");

            // 2. Atualizar os dados usando a classe Compromisso
            var compromissoAtualizado = new Compromisso(dto.Assunto, dto.Data, dto.HoraInicio, dto.HoraTermino, dto.Tipo, dto.Local);
            compromissoAtualizado.Id = dto.Id;

            // 3. Validar
            List<string> erros = compromissoAtualizado.Validar();
            if (erros.Count > 0)
                throw new Exception(string.Join(" | ", erros));

            // 4. Salvar
            _repositorio.Editar(dto.Id, compromissoAtualizado);
        }

        public void Excluir(Guid id)
        {
            _repositorio.Excluir(id);
        }

        public List<Compromisso> SelecionarTodos()
        {
            return _repositorio.SelecionarTodos();
        }

        public Compromisso? SelecionarPorId(Guid id)
        {
            return _repositorio.SelecionarPorId(id);
        }
    }
}