using E_Agenda.WebApp.Modulos.ModuloCategoria.Aplicacao;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Aplicacao;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Aplicacao;

using E_Agenda.WebApp.Modulos.ModuloCompromisso.Aplicacao;
using E_Agenda.WebApp.Modulos.ModuloContatos.Aplicacao;
using E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;
using E_Agenda.WebApp.Modulos.ModuloContatos.Infra;
using E_Agenda.WebApp.Compartilhado.Aplicacao.Logging;


public static class InjecaoDependencia
{
    public static void AddApplicationServices(this 
        IServiceCollection services, 
        ILoggingBuilder logging,
        IConfiguration configuration
    )
    {
        services.AddSerilogLogger(logging, configuration);
        services.AddScoped<ServicoContatos>();
        services.AddScoped<ServicoCompromisso>();
        services.AddScoped<ServicoCategoria>();
        services.AddScoped<ServicoDespesa>();
        services.AddScoped<ServicoTarefa>();
        services.AddScoped<ServicoContatos>();
        services.AddScoped<ServicoCompromisso>();
    }
}