using AutoMapper;
using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Dominio;
using E_Agenda.WebApp.Modulos.ModuloContatos.Apresentacao;
using E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloContatos.Aplicacao;

public class ServicoContatos
{
    private readonly IRepositorio<Contatos> _repositorio;
    private readonly ContextoJson _contexto;
    private readonly IMapper _mapper;
    private readonly IRepositorioCompromisso _repositorioCompromisso;

    public ServicoContatos(IRepositorio<Contatos> repositorio, ContextoJson contexto, IMapper mapper, IRepositorioCompromisso repositorioCompromisso)
    {
        _repositorio = repositorio;
        _contexto = contexto;
        _mapper = mapper;
        _repositorioCompromisso = repositorioCompromisso;
    }

    public void Cadastrar(CadastrarContatosViewModel model)
{
    // 1. Mapeamento
    var contato = _mapper.Map<Contatos>(model); // Usei 'contato' em vez de 'novoContato'
    contato.Id = Guid.NewGuid();
    
    // 2. Persistência (Verifique o nome correto do seu campo de repositório!)
    // Se o seu campo no construtor for _repositorio, use _repositorio aqui
    _repositorio.Cadastrar(contato); 

    // 3. Vinculação de Compromissos
    // Verifica se a lista de compromissos no model não está vazia antes do foreach
    if (model.Compromissos != null)
    {
        foreach (var item in model.Compromissos.Where(c => c.Marcado))
        {
            var compromisso = _repositorioCompromisso.SelecionarPorId(item.Id);
            if (compromisso != null)
            {
                compromisso.ContatoId = contato.Id;
                _repositorioCompromisso.Editar(compromisso.Id, compromisso);
            }
        }
    }
}
    public void Editar(EditarContatosViewModel model)
    {
        var contatoEditado = _mapper.Map<Contatos>(model);
        
        var erros = contatoEditado.Validar();
        var todosOsContatos = _repositorio.SelecionarTodos();
        erros.AddRange(contatoEditado.ValidarDuplicidade(todosOsContatos));
        
        if (erros.Any())
            throw new Exception(string.Join(" | ", erros));

        _repositorio.Editar(contatoEditado.Id, contatoEditado);
        _contexto.Salvar();
    }

    public void Excluir(Guid id)
    {
        bool temVinculo = _repositorioCompromisso.ExisteVinculoComContato(id);

        if (temVinculo)
        {
            throw new Exception("Não é possível excluir este contato, pois existem compromissos vinculados a ele.");
        }

        _repositorio.Excluir(id);
        _contexto.Salvar();
    }

    public List<Contatos> SelecionarTodos() => _repositorio.SelecionarTodos();
    
    public Contatos? SelecionarPorId(Guid id) => _repositorio.SelecionarPorId(id);
}