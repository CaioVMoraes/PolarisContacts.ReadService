using Microsoft.Extensions.DependencyInjection;
using PolarisContacts.ReadService.Application.Interfaces.Repositories;
using PolarisContacts.ReadService.Domain.Settings;
using PolarisContacts.ReadService.Infrastructure.Repositories;

namespace PolarisContacts.ReadService.CrossCutting.DependencyInjection.Extensions.AddInfrastructureLayer;

public static partial class AddInfrastructureLayerExtensions
{
    public static IServiceCollection AddSettings(this IServiceCollection services) =>
        services.AddBindedSettings<DbSettings>();

    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services.AddScoped<IUsuarioRepository, UsuarioRepository>()
                .AddScoped<IContatoRepository, ContatoRepository>()
                .AddScoped<ITelefoneRepository, TelefoneRepository>()
                .AddScoped<ICelularRepository, CelularRepository>()
                .AddScoped<IEmailRepository, EmailRepository>()
                .AddScoped<IRegiaoRepository, RegiaoRepository>()
                .AddScoped<IEnderecoRepository, EnderecoRepository>()
                .AddScoped<IDatabaseConnection, DatabaseConnection>();

    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services
            .AddSettings()
            .AddRepositories();
}