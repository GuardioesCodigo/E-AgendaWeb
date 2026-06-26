using System;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloItensTarefa.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloItensTarefa.Infra;

public class RepositorioItensTarefaEmArquivo : RepositorioBaseEmArquivo<ItensTarefa>, IRepositorioItensTarefa
{
    public RepositorioItensTarefaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<ItensTarefa> CarregarRegistros()
    {
        return contexto.itensTarefas;
    }
}