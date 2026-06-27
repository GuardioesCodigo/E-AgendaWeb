using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;
using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFuncionarios.Apresentacao;

namespace E_Agenda.WebApp.Modulos.ModuloContatos.Aplicacao;

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

    public void Cadastrar(CadastrarContatosViewModel model)
    {
        // 1. Mapeia a ViewModel para o domínio
        var novoContato = _mapper.Map<Contatos>(model);

        // 2. Executa as validações do domínio
        var erros = novoContato.Validar();
        if (erros.Count > 0)
            throw new Exception(string.Join(" | ", erros));

        // 3. Persistência
        novoContato.Id = Guid.NewGuid();
        _repositorio.Cadastrar(novoContato);
        _contexto.Salvar();
    }

    public void Editar(EditarContatosViewModel model)
    {
        // 1. Mapeia a ViewModel para o domínio
        var compromissoEditado = _mapper.Map<Contatos>(model);

        // 2. Executa as validações do domínio
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

    public List<Contatos> SelecionarTodos() => _repositorio.SelecionarTodos();

    public Contatos? SelecionarPorId(Guid id) => _repositorio.SelecionarPorId(id);
}