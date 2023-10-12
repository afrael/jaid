using Microsoft.Extensions.Configuration;
using Atlassian.Jira;
using jaid.core.events;

namespace jaid.core.io
{
    /// <summary>
    /// Reader class, reads Jira Issues from Jira Server
    /// </summary>
    public class JaidJiraReader
    {
        private readonly Jira _jiraClient;
        public event EventHandler<ProgressInformationArgs> ShowProgressInformation;
        public event EventHandler<StopProgressArgs> StopProgressTimer;

        
        private JaidJiraReader(string jiraEndpoint, string jiraUser, string jiraPassword)
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
        public static JaidJiraReader CreateJiraClient(IConfiguration configurationManager)
        {
            var jiraSettings = ResolveJiraSettings(configurationManager);
            return new JaidJiraReader(jiraSettings.urlEndpoint, jiraSettings.user, jiraSettings.password);
        }
        
        public async Task<Issue> GetJiraIssueDetailsByKey(string issueKey)
        {
            OnShowProgressInformation(new ProgressInformationArgs("Fetching", $"Getting Jira details for {issueKey}"));
            
            try
            {
                return await _jiraClient.Issues.GetIssueAsync(issueKey, CancellationToken.None);
            }
            finally
            {
                OnStopProgressTimer(new StopProgressArgs());
            }
        }

        private static (string urlEndpoint, string user, string password) ResolveJiraSettings(IConfiguration configuration)
        {
            const string envVarPrefix = "___ENV___";

            if (string.IsNullOrWhiteSpace(configuration["JiraSettings:ApiEndPoint"]))
                return(string.Empty, string.Empty, string.Empty);

            var user = configuration["JiraSettings:JiraUser"].StartsWith(envVarPrefix) 
                        ? ResolveFromEnvironmentVariable(configuration["JiraSettings:JiraUser"])
                        : configuration["JiraSetting:JiraUser"];
                        
            var pwd = configuration["JiraSettings:JiraPwd"].StartsWith(envVarPrefix) 
                        ? ResolveFromEnvironmentVariable(configuration["JiraSettings:JiraPwd"])
                        : configuration["JiraSetting:JiraPwd"];

            return (configuration["JiraSetting:ApiEndPoint"], user, pwd);
        }

        private static string ResolveFromEnvironmentVariable(string key)
        {
            var envVar = Environment.GetEnvironmentVariable(key.Substring(9));
            return envVar ?? string.Empty;
        }

        private void OnStopProgressTimer(StopProgressArgs stopProgressArgs)
        {
            StopProgressTimer?.Invoke(this, stopProgressArgs);
        }

        private void OnShowProgressInformation(ProgressInformationArgs progressInformationArgs)
        {
            ShowProgressInformation?.Invoke(this, progressInformationArgs);
        }
    }
}