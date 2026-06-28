using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;
using AutoMapper;
using E_Agenda.WebApp.Modulos.ModuloContatos.Apresentacao;

public class ServicoContatos
{
    private readonly IRepositorio<Contatos> _repositorio;
    private readonly ContextoJson _contexto;
    private readonly IMapper _mapper;

    public ServicoContatos(IRepositorio<Contatos> repositorio, ContextoJson contexto, IMapper mapper)
    {
        _repositorio = repositorio;
        _contexto = contexto;
        _mapper = mapper;
    }

    public void Cadastrar(Contatos novoContato) // Receba o objeto já mapeado
{
    var todosOsContatos = _repositorio.SelecionarTodos();
    
    var erros = novoContato.Validar();

    var contatosExistentes = _repositorio.SelecionarTodos();
    erros.AddRange(novoContato.ValidarDuplicidade(contatosExistentes));

    if (erros.Count > 0)
        throw new Exception(string.Join(" | ", erros));

    // 2. Persistência
    novoContato.Id = Guid.NewGuid();
    _repositorio.Cadastrar(novoContato);
    _contexto.Salvar();
}

public void Editar(EditarContatosViewModel model)
{
    // Aqui sim usamos o mapper, pois 'model' é uma ViewModel, não um Contatos
    var contatoEditado = _mapper.Map<Contatos>(model);
    contatoEditado.Id = model.Id;

    // Agora validamos o objeto que o mapper criou
    var erros = contatoEditado.Validar();
    var todosOsContatos = _repositorio.SelecionarTodos();
    erros.AddRange(contatoEditado.ValidarDuplicidade(todosOsContatos));
    
    if (erros.Count > 0)
        throw new Exception(string.Join(" | ", erros));

    // Persistência
    _repositorio.Editar(contatoEditado.Id, contatoEditado);
    _contexto.Salvar();
}

    

    public void Excluir(Guid id)
    {
        _repositorio.Excluir(id);
        _contexto.Salvar();
    }

    public List<Contatos> SelecionarTodos() => _repositorio.SelecionarTodos();
    
    public Contatos? SelecionarPorId(Guid id) => _repositorio.SelecionarPorId(id);

    // Implementar Editar e Excluir seguindo a mesma lógica...
}