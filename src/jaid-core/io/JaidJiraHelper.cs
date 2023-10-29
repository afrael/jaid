
using Microsoft.Extensions.Configuration;
using Atlassian.Jira;

namespace jaid.core.io
{
    /// <summary>
    /// Jira class helper
    /// Resolves credentials and creates a JiraClient
    /// </summary>
    public class JaidJiraHelper
    {
        private static Jira _jiraClient;

        private JaidJiraHelper(string jiraEndpoint, string jiraUser, string jiraPassword)
        {
            try
            {
                _jiraClient ??= Jira.CreateRestClient(jiraEndpoint, jiraUser, jiraPassword);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a Jira Client to connect to the Jira API
        /// </summary>
        /// <returns></returns>
        public static Jira CreateJiraClient(IConfiguration configurationManager)
        {
            var jiraSettings = ResolveJiraSettings(configurationManager);
            _ = new JaidJiraHelper(jiraSettings.urlEndpoint, jiraSettings.user, jiraSettings.password);
            return _jiraClient;
        }

        public static (string urlEndpoint, string user, string password) ResolveJiraSettings(IConfiguration configuration)
        {
            const string envVarPrefix = "___ENV___";
            var jiraApiEndpoint = configuration["JiraSettings:ApiEndPoint"];

            if (string.IsNullOrWhiteSpace(jiraApiEndpoint))
                return (string.Empty, string.Empty, string.Empty);

            var user = configuration["JiraSettings:JiraUser"].StartsWith(envVarPrefix)
                        ? ResolveFromEnvironmentVariable(configuration["JiraSettings:JiraUser"])
                        : configuration["JiraSetting:JiraUser"];

            var pwd = configuration["JiraSettings:JiraPwd"].StartsWith(envVarPrefix)
                        ? ResolveFromEnvironmentVariable(configuration["JiraSettings:JiraPwd"])
                        : configuration["JiraSetting:JiraPwd"];

            return (jiraApiEndpoint, user, pwd);
        }

        private static string ResolveFromEnvironmentVariable(string key)
        {
            var envVar = Environment.GetEnvironmentVariable(key.Substring(9));
            return envVar ?? string.Empty;
        }
    }
}