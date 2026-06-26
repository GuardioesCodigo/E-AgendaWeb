using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloCategoria.Dominio;
using E_Agenda.WebApp.Modulos.ModuloCategoria.Infra;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Dominio;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Infra;

namespace E_Agenda.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)
    {
   

        services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmArquivo>();
        services.AddScoped<IRepositorioDepesa, RepositorioDespesaEmArquivo>();

    }
}
