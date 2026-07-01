using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Aplicacao;
using E_Agenda.WebApp.Modulos.ModuloContatos.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoContatos>();
       
        services.AddScoped<ServicoCompromisso>();

    }
}