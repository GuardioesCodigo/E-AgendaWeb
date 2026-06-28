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

    public void Cadastrar(Contatos model)
    {
        // 1. Mapeia a ViewModel para o domínio
        var novoFuncionario = _mapper.Map<Contatos>(model);

        // 2. Executa as validações do domínio
        var erros = novoFuncionario.Validar();
        if (erros.Count > 0)
            throw new Exception(string.Join(" | ", erros));

        // 4. Persistência
        novoFuncionario.Id = Guid.NewGuid();
        _repositorio.Cadastrar(novoFuncionario);
        _contexto.Salvar();
    }

    public void Editar(EditarContatosViewModel model)
    {
        // 1. Mapeia a ViewModel para o domínio
        var funcionarioEditado = _mapper.Map<Contatos>(model);

        // 2. Executa as validações do domínio
        var erros = funcionarioEditado.Validar();
        if (erros.Count > 0)
            throw new Exception(string.Join(" | ", erros));

        // 4. Persistência (não geramos Guid.NewGuid pois o ID já existe)
        _repositorio.Editar(funcionarioEditado.Id, funcionarioEditado);
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