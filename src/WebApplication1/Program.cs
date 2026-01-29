using System;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal static class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateSlimBuilder();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            builder.WebHost.UseHttpSys();
        }
        else
        {
            throw new PlatformNotSupportedException("Non-Windows platform detected.");
        }

        var app = builder.Build();
        var logger = app.Logger;

        app.MapGet("/", () => $"Hello {RuntimeInformation.FrameworkDescription}!");

        logger.LogInformation("Framework: {FrameworkDescription}", RuntimeInformation.FrameworkDescription);

        if (args.Any(x => x.Equals("--start-application", StringComparison.OrdinalIgnoreCase)))
        {
            app.Start();

            logger.LogInformation("Application started.");

            app.WaitForShutdown();

            logger.LogInformation("Application stopped.");
        }
        else
        {
            logger.LogInformation("Skipped starting application.");
        }

        try
        {
            logger.LogInformation("Disposing application...");

            if (app is IDisposable disposeable)
            {
                disposeable.Dispose();
            }
            else
            {
                app.DisposeAsync().AsTask().GetAwaiter().GetResult();
            }

            logger.LogInformation("Application disposed.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during disposal of application.");
        }
    }
}

