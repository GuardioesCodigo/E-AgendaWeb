using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloCompromissos.Aplicacao;
using E_Agenda.WebApp.Modulos.ModuloContatos.Aplicacao;
using E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;
using E_Agenda.WebApp.Modulos.ModuloContatos.Infra;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
       
        services.AddSingleton<ContextoJson>();
        services.AddScoped<ServicoContatos>();
        services.AddScoped<ServicoCompromisso>();

    }
}