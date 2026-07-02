using System;
using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using System.Linq;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloCompromisso.Infra;

public class RepositorioCompromissoEmArquivo : RepositorioBaseEmArquivo<Compromisso>, IRepositorioCompromisso
{
    public RepositorioCompromissoEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    public bool ExisteVinculoComContato(Guid contatoId)
    {
        return contexto.compromissos.Any(c => c.ContatoId == contatoId);
    }

    protected override List<Compromisso> CarregarRegistros()
    {
        return contexto.compromissos;
    }
}