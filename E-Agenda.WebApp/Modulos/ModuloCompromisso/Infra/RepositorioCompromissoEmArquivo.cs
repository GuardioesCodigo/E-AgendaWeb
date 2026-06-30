using System;
using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloCompromisso.Infra;

public class RepositorioCompromissoEmArquivo : RepositorioBaseEmArquivo<Compromisso>,IRepositorioCompromisso
{
    public RepositorioCompromissoEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    public bool ExisteVinculoComContato(Guid id)
    {
        throw new NotImplementedException();
    }

    protected override List<Compromisso> CarregarRegistros()
    {
        return contexto.compromissos;
    }
}
