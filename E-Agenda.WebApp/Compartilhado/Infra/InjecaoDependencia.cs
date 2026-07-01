using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloCategoria.Dominio;
using E_Agenda.WebApp.Modulos.ModuloCategoria.Infra;
using E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;
using E_Agenda.WebApp.Modulos.ModuloContatos.Infra;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Dominio;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Dominio;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Dominio;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Infra;
using E_Agenda.WebApp.Modulos.ModuloDespesa.Infra;

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
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

        services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmSql>();
        services.AddScoped<IRepositorioDepesa, RepositorioDespesaEmSql>();
        services.AddScoped<IRepositorioTarefa, RepositorioTarefaEmSql>();

        services.AddScoped<IRepositorio<Contatos>, RepositorioContatosEmArquivo>();
        services.AddScoped<IRepositorioContatos, RepositorioContatosEmArquivo>();
        services.AddScoped<IRepositorio<Contatos>, RepositorioContatosEmArquivo>();
        services.AddScoped<IRepositorioCompromisso, RepositorioCompromissoEmArquivo>();
        services.AddScoped<IRepositorio<Compromisso>, RepositorioCompromissoEmArquivo>();
    }
}