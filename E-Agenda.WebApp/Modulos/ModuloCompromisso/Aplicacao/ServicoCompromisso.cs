using AutoMapper;
using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Dominio;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Apresentacao;

namespace E_Agenda.WebApp.Modulos.ModuloCompromisso.Aplicacao
{
    public class ServicoCompromisso
    {
        private readonly IRepositorio<Compromisso> _repositorio;
        private readonly ContextoJson _contexto;
        private readonly IMapper _mapper;

        public ServicoCompromisso(IRepositorio<Compromisso> repositorio, ContextoJson contexto, IMapper mapper)
        {
            _repositorio = repositorio;
            _contexto = contexto;
            _mapper = mapper;
        }

        public void Cadastrar(Compromisso novoCompromisso)
        {
            // 1. Mapeia a ViewModel para a Entidade

            // 2. Validação de Domínio
            var erros = novoCompromisso.Validar();
            
            if (erros.Count > 0)
                throw new Exception(string.Join(" | ", erros));

            // 3. Persistência
            novoCompromisso.Id = Guid.NewGuid();
            _repositorio.Cadastrar(novoCompromisso);
            _contexto.Salvar();
        }

        public void Editar(EditarCompromissoViewModel model)
        {
            // 1. Mapeia a ViewModel para a Entidade
            var compromissoEditado = _mapper.Map<Compromisso>(model);
            
            // 2. Validação de Domínio
            var erros = compromissoEditado.Validar();
            
            if (erros.Count > 0)
                throw new Exception(string.Join(" | ", erros));

            // 3. Persistência
            _repositorio.Editar(compromissoEditado.Id, compromissoEditado);
            _contexto.Salvar();
        }

        public void Excluir(Guid id)
        {
            _repositorio.Excluir(id);
            _contexto.Salvar();
        }

        public List<Compromisso> SelecionarTodos() => _repositorio.SelecionarTodos();

        public Compromisso? SelecionarPorId(Guid id) => _repositorio.SelecionarPorId(id);
    }
}