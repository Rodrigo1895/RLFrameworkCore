using Exemplo.Web;
using Exemplo.Web.Data.Contextos;
using Exemplo.Web.Dominio;
using Exemplo.Web.Middlewares;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Exempslo.Web
{
    public class Startup : IStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region API

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });            

            services.AddVersionamentoApiConfiguracao()
                .AddSwaggerConfiguracao();

            #endregion API

            #region RLFramework

            services.AddPaginacaoConfig(Configuration) 
                .AddAutenticacao(Configuration)
                .AddNotificacaoConfig(options =>
                {
                    options.AddJsonLocalizacaoArquivo(Constants.NomeErroSource, typeof(Constants).Assembly, "Exemplo.Web.Dominio.Localizacao.Arquivos");
                    options.AddIdioma("pt-BR", padrao: true);
                });

            #endregion RLFramework

            #region Injeção Dependência

            services.AddDependencyInjectionExtension(Configuration);
            services.AddAppServicosDependencia();

            #endregion Injeção Dependência

            #region RabbitMq

            services.AddRabbitMqConfig(Configuration);

            #endregion RabbitMq
        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(err => err.UseCustomErrors(environment));
            }

            app.UseCors("AllowAllOrigins");

            var scope = app.Services.CreateScope();
            IApiVersionDescriptionProvider provider = scope.ServiceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

            #region Banco de Dados

            // Cria as tabelas no Banco
            var db = scope.ServiceProvider.GetRequiredService<AppContexto>();
            db.Database.Migrate();
            // Insere dados iniciais para testes
            SeedData.AddSeedData(app);

            #endregion Banco de Dados

            #region RL Framework

            app.UseAutoMapperHelper();

            #endregion RL Framework

            #region API

            app.UseVersionamentoApiConfiguracao()
                .UseSwaggerConfiguracao(provider);

            #endregion API

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }
    }

    public interface IStartup
    {
        IConfiguration Configuration { get; }
        void ConfigureServices(IServiceCollection services);
        void Configure(WebApplication app, IWebHostEnvironment environment);
    }

    public static class StartupExtenssions
    {
        public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder webAppBuilder) where TStartup : IStartup
        {
            var startup = Activator.CreateInstance(typeof(TStartup), webAppBuilder.Configuration) as IStartup;
            if (startup == null) throw new ArgumentException("Classe Startup.cs inválida!");

            startup.ConfigureServices(webAppBuilder.Services);

            var app = webAppBuilder.Build();
            startup.Configure(app, app.Environment);

            app.Run();

            return webAppBuilder;
        }
    }
}