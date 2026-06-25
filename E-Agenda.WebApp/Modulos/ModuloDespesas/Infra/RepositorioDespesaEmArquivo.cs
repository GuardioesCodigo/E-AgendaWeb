using System;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloDespesas.Infra;

public class RepositorioDespesaEmArquivo : RepositorioBaseEmArquivo<Despesa>, IRepositorioDepesa
{
    public RepositorioDespesaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Despesa> CarregarRegistros()
    {
        return contexto.despesas;
    }
}
