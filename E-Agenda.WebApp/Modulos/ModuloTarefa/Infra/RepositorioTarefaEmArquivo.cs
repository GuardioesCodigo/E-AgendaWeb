using System;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloTarefa.Infra;

public class RepositorioTarefaEmArquivo : RepositorioBaseEmArquivo<Tarefa>, IRepositorioTarefa
{
    public RepositorioTarefaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Tarefa> CarregarRegistros()
    {
        return contexto.tarefas;
    }
}
