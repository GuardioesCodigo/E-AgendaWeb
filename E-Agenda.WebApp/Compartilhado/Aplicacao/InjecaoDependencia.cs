<<<<<<< HEAD
using E_Agenda.WebApp.Modulos.ModuloCategoria.Aplicacao;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Aplicacao;
=======
using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;
using E_Agenda.WebApp.Modulos.ModuloContatos.Infra;
>>>>>>> Contatos

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
<<<<<<< HEAD
        services.AddScoped<ServicoCategoria>();
        services.AddScoped<ServicoDespesa>();
=======
        services.AddSingleton<ContextoJson>();
        services.AddScoped<IRepositorio<Contatos>, RepositorioContatosEmArquivo>();
        services.AddScoped<ServicoContatos>();

>>>>>>> Contatos
    }
}