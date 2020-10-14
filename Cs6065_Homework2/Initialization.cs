#nullable enable
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Cs6065_Homework2.Data;

namespace Cs6065_Homework2
{
    public class ApplicationInitializationException : ApplicationException
    {
        public ApplicationInitializationException()
        {
        }
        public ApplicationInitializationException(string message)
            : base(message)
        {
        }
        public ApplicationInitializationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public static class Initialization
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            //var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            //await EnsureRolesAsync(roleManager);
            //var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            //await EnsureTestAdminAsync(userManager);
            ILogger<Program>? logger = services.GetRequiredService<ILogger<Program>>();
            if (logger == null)
            {
                throw new ApplicationInitializationException("failed to get logger during initialization.");
            }
            IConfiguration? configuration = services.GetRequiredService<IConfiguration>();
            if (configuration == null)
            {
                throw new ApplicationInitializationException("failed to get configuration during initialization.");
            }
            ApplicationDbContext? dbContext = services.GetRequiredService<ApplicationDbContext>();
            if (dbContext == null)
            {
                throw new ApplicationInitializationException("failed to get dbContext during initialization.");
            }
            bool dbSeeded = await SeedDatabase.SeedAll(logger, configuration, dbContext);
        }
    }
}
