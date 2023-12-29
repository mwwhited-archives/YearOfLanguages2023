using HashEm.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace HashEm;

internal class Program
{

    //    // https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16
    //    // Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=D:\Data\MyDB1.mdf

    //    // https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli

    //    var searchRoot = @"E:\";
    //    var metadata = Path.Combine(searchRoot, "metadata");

    static async Task Main(string[] args) =>
        await Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                //config.AddCommandLine(args,
                //    //CommandLine.BuildParameters<TemplateEngineSettings>()
                //    //           .AddParameters<FileTemplatingSettings>()
                //    );
            })
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<HashDbContext>(options =>
                {
                    options.UseSqlite(context.Configuration.GetConnectionString("DefaultConnection"));
                });

                //services.AddConfiguration<TemplateEngineSettings>(context.Configuration);
                //services.AddConfiguration<FileTemplatingSettings>(context.Configuration);

                services.AddHostedService<HashingService>();

                //services.TryAddSystemExtensions(context.Configuration);
                //services.TryAddHandlebarServices();
            })
            .StartAsync();
}
