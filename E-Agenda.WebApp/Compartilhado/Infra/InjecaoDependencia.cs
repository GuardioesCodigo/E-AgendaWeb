using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloCompromissos.Dominio;
using E_Agenda.WebApp.Modulos.ModuloCompromissos.Infra;
using E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;
using E_Agenda.WebApp.Modulos.ModuloContatos.Infra;

namespace E_Agenda.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)

    {
        services.AddScoped<IRepositorioContatos, RepositorioContatosEmArquivo>();
        services.AddScoped<IRepositorio<Contatos>, RepositorioContatosEmArquivo>();
        services.AddScoped<IRepositorioCompromisso, RepositorioCompromissoEmArquivo>();
        services.AddScoped<IRepositorio<Compromisso>, RepositorioCompromissoEmArquivo>();
    }
}
