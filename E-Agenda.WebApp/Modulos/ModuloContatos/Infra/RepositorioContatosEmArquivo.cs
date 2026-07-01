using System;
using System.Collections.Generic;
using System.Linq;
using E_Agenda.WebApp.Compartilhado;
using E_Agenda.WebApp.Compartilhado.Infra;
using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloContatos.Infra;

public class RepositorioContatosEmArquivo : RepositorioBaseEmArquivo<Contatos>, IRepositorio<Contatos>,IRepositorioContatos
{
    public RepositorioContatosEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Contatos> CarregarRegistros()
    {
        // Se contexto.Paciente for nulo, retorna uma lista vazia, caso contrário retorna a lista
        return contexto.contato;
    }
}