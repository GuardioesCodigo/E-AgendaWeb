using E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;
using E_Agenda.WebApp.Compartilhado; // Assumindo que você tem um padrão Result

namespace ControleDeMedicamentos.WebApp.ModuloCompromissos.Aplicacao;

public class ServicoCompromisso
{
    private readonly IRepositorioCompromisso _repositorio;

    public ServicoCompromisso(IRepositorioCompromisso repositorio)
    {
        _repositorio = repositorio;
    }

    public Result Cadastrar(CadastrarCompromissoDto dto)
    {
        // 1. Instanciar a entidade
        var compromisso = new Contatos(dto.Assunto, dto.Data, dto.HoraInicio, dto.HoraTermino, dto.Tipo, dto.Local);

        // 2. Validar (chamando o método que criamos na classe Compromisso)
        List<string> erros = compromisso.Validar();

        if (erros.Count > 0)
            return Result.Fail(erros);

        // 3. Persistir
        compromisso.Id = Guid.NewGuid();
        _repositorio.Cadastrar(compromisso);

        return Result.Ok();
    }

    public Result Editar(EditarCompromissoDto dto)
    {
        // 1. Buscar o existente
        var compromissoExistente = _repositorio.SelecionarPorId(dto.Id);

        if (compromissoExistente == null)
            return Result.Fail("Compromisso não encontrado.");

        // 2. Atualizar os dados
        var compromissoAtualizado = new Contatos(dto.Assunto, dto.Data, dto.HoraInicio, dto.HoraTermino, dto.Tipo, dto.Local);
        compromissoAtualizado.Id = dto.Id;

        // 3. Validar
        List<string> erros = compromissoAtualizado.Validar();
        if (erros.Count > 0)
            return Result.Fail(erros);

        // 4. Salvar
        _repositorio.Editar(dto.Id, compromissoAtualizado);

        return Result.Ok();
    }

    public void Excluir(Guid id)
    {
        _repositorio.Excluir(id);
    }

    public List<Contatos> SelecionarTodos()
    {
        return _repositorio.SelecionarTodos();
    }

    public Contatos? SelecionarPorId(Guid id)
    {
        return _repositorio.SelecionarPorId(id);
    }
}