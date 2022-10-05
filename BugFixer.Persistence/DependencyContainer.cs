using BugFixer.Application.Services.Implementations;
using BugFixer.Application.Services.Interfaces;
using BugFixer.Domain.Interfeces;
using BugFixer.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BugFixer.Persistence;

public class DependencyContainer
{
    public static void RegisterDependncies(IServiceCollection services)
    {
        #region Repositories

        services.AddScoped<IUserRepository, UserRepository>();

        #endregion

        #region Services

        services.AddScoped<IUserService, UserService>();

        #endregion
    }
}