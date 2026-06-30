using E_Agenda.WebApp.Compartilhado.Apresentacao.Mapping;

namespace E_Agenda.WebApp.Compartilhado.Apresentacao;

public static class InjecaoDependencia
{
    public static void AddPresentationConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews().AddRazorOptions(options =>
        {
            // Reseta a configuração padrão do MVC
            options.ViewLocationFormats.Clear();

            // Localização das Views dos módulos: Modulos/ModuloCaixa/Apresentacao/Views/Listar.cshtml
            options.ViewLocationFormats.Add("/Modulos/Modulo{1}/Apresentacao/Views/{0}.cshtml");

            // Localização das Views compartilhadas: /Compartilhado/Apresentacao/Views/_Layout.cshtml
            options.ViewLocationFormats.Add("/Compartilhado/Apresentacao/Views/{0}.cshtml");
        });

        services.AddAutoMapper(mapperConfig =>
        {
            AutoMapperOptions autoMapperOptions = configuration
                .GetSection(AutoMapperOptions.SectionName)
                .Get<AutoMapperOptions>() ?? new AutoMapperOptions();

            string? LinceseKey = autoMapperOptions.LinceseKey;

            if (!string.IsNullOrWhiteSpace(LinceseKey))
                mapperConfig.LicenseKey = LinceseKey;

            mapperConfig.AddMaps(typeof(Program).Assembly);
        });
    }
}
