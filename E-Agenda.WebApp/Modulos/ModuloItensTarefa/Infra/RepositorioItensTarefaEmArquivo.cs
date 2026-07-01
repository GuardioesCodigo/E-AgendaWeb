using System;
using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloItensTarefa.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloItensTarefa.Infra;

public class RepositorioItensTarefaEmArquivo : RepositorioBaseEmArquivo<ItensDeTarefas>, IRepositorioItensTarefa
{
    public RepositorioItensTarefaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    public interface IRepositorioItemTarefa : IRepositorio<ItensDeTarefas>
    {
        List<ItensDeTarefas> SelecionarItensPorTarefa(Guid tarefaId);
    }
    protected override List<ItensDeTarefas> CarregarRegistros()
    {
        return contexto.itensTarefas;
    }
}