using Microsoft.Extensions.Configuration;
using Atlassian.Jira;
using jaid.core.events;

namespace jaid.core.io
{
    /// <summary>
    /// Reader class, reads Jira Issues from Jira Server
    /// </summary>
    public class JaidJiraReader : JaidJiraBase
    {
        private readonly Jira _jiraClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Iconfiguration class with options</param>
        public JaidJiraReader(IConfiguration configuration)
        {
            _jiraClient = JaidJiraHelper.CreateJiraClient(configuration);
        }

        /// <summary>
        /// Fetches the Jira information by id
        /// </summary>
        /// <param name="issueKey">Jira issue key or id</param>
        /// <returns></returns>
        public async Task<Issue> GetJiraIssueDetailsByKey(string issueKey)
        {
            this.OnShowProgressInformation(new ProgressInformationArgs("Fetching", $"Getting Jira details for {issueKey}"));
            try
            {
                return await _jiraClient.Issues.GetIssueAsync(issueKey, CancellationToken.None);
            }
            finally
            {
                OnStopProgressTimer(new StopProgressArgs());
            }
        }

    }
}