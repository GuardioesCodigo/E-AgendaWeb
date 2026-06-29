using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Infra;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Dominio;

namespace E_Agenda.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)

    {
        services.AddScoped(provider =>
        {
            ContextoJson contextoJson = new ContextoJson();

            contextoJson.Carregar();

            return contextoJson;
        });
        services.AddScoped<RepositorioCompromissoEmArquivo>();
        services.AddScoped<IRepositorioCompromisso, RepositorioCompromissoEmArquivo>();
        services.AddScoped<IRepositorio<Compromisso>, RepositorioCompromissoEmArquivo>();
        
    }
}