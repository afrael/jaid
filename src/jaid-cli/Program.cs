using Serilog;
using CommandLine;
using Microsoft.Extensions.Configuration;
using jaid.models;
using jaid.core.io;
using Spectre.Console;

namespace jaid
{
    internal class Program
    {
        private static async Task<int> Main(string[] args)
        {
            // Load app settings
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var logFile = (configuration["LogSettings:LogFileLocation"]
                        ?? $"{AppDomain.CurrentDomain.BaseDirectory}/logs/")
                        + "jaid-log.log";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole()
                .WriteTo.File(logFile,
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Hello World?!");

            Console.WriteLine("Hello, World!");

            return await Parser.Default.ParseArguments<NewOptions, GetOptions, ListOptions>(args)
                .MapResult(
                    (NewOptions opts) => RunCreateAndReturnExitCode(configuration, opts),
                    (GetOptions opts) => RunGetAndReturnExitCode(configuration, opts),
                    (ListOptions opts) => RunListAndReturnExitCode(configuration, opts),
                errs => Task.FromResult(1));
        }

        private static async Task<int> RunCreateAndReturnExitCode(IConfiguration config, NewOptions opts)
        {
            return 100;
        }

        private static async Task<int> RunGetAndReturnExitCode(IConfiguration config, GetOptions opts)
        {
            AnsiConsole.Markup("[underline red]Hello[/] World!");
            var jiraReader = new JaidJiraReader(config);
            var jira = await jiraReader.GetJiraIssueDetailsByKey(opts.JiraId);
            return 100;
        }

        private static async Task<int> RunListAndReturnExitCode(IConfiguration config, ListOptions opts)
        {
            return 100;
        }
    }

}