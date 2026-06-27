using System;
using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloCompromissos.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloCompromissos.Infra;

public class RepositorioCompromissoEmArquivo : RepositorioBaseEmArquivo<Compromisso>, IRepositorioCompromisso
{
    public RepositorioCompromissoEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Compromisso> CarregarRegistros()
    {
        return contexto.compromissos;
    }
}
