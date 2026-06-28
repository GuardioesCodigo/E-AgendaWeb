using E_Agenda.WebApp.Modulos.ModuloCategoria.Aplicacao;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Aplicacao;
using E_Agenda.WebApp.Compartilhado.Infra.Arquivos;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Aplicacao;


public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoCategoria>();
        services.AddScoped<ServicoDespesa>();
        services.AddScoped<ServicoTarefa>();
        services.AddScoped<ServicoContatos>();
    }
}