using Microsoft.Extensions.Configuration;
using Atlassian.Jira;
using jaid.core.models;
using jaid.core.events;
using jaid.core.enums;

namespace jaid.core.io
{
    /// <summary>
    /// Writer class, creates Jira Issues in Jira Server
    /// </summary>
    public class JaidJiraWriter : JaidJiraBase
    {
        private readonly Jira _jiraClient;

        public JaidJiraWriter(IConfiguration configuration)
        {
            _jiraClient = JaidJiraHelper.CreateJiraClient(configuration);
        }

        /// <summary>
        /// Creates a Jira
        /// </summary>
        /// <param name="issueKey">Jira issue key or id</param>
        /// <returns></returns>
        public async Task<Issue> CreateJiraIssue(JaidTicketType ticketType, JaidJiraCreate jiraCreate)
        {
            this.OnShowProgressInformation(new ProgressInformationArgs("Creating", $"Creating new Jira"));
            try
            {
                var jiraPayload = string.Empty;
                //var issue = new Issue()
                //return await _jiraClient.Issues.CreateIssueAsync()
                return null;
            }
            finally
            {
                OnStopProgressTimer(new StopProgressArgs());
            }
        }
    }
}