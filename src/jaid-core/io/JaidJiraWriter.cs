using Microsoft.Extensions.Configuration;
using jaid.core.events;
using jaid.core.enums;
using System.Net.Http.Headers;
using System.Text;

namespace jaid.core.io
{
    /// <summary>
    /// Writer class, creates Jira Issues in Jira Server
    /// </summary>
    public class JaidJiraWriter : JaidJiraBase
    {
        public static HttpClient HttpClient { get; } = new HttpClient();
        public IDictionary<string, string> HttpHeaders { get; set; }
        public (string urlEndpoint, string user, string password) JiraSettings { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        public JaidJiraWriter(IConfiguration configuration)
        {
            // initilize the http client
            this.JiraSettings = JaidJiraHelper.ResolveJiraSettings(configuration);
            HttpClient.BaseAddress = new Uri(JiraSettings.urlEndpoint);
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            // Call the function to get Jira API headers.
            this.HttpHeaders = GetJiraApiHeaders();
        }

        /// <summary>
        /// Get the Jira headers to be able to create or update a Jira/Ticket/Issue
        /// </summary>
        /// <returns>Dictionary of strings with the header values</returns>
        private Dictionary<string, string> GetJiraApiHeaders()
        {
            // Set the Jira username and password from environment variables.
            var user = this.JiraSettings.user;
            var pass = this.JiraSettings.password;

            var headers = new Dictionary<string, string>();

            // Combine the username and password and convert to Base64.
            string pair = $"{user}:{pass}";
            byte[] bytes = Encoding.ASCII.GetBytes(pair);
            string base64 = Convert.ToBase64String(bytes);
            string basicAuthValue = "Basic " + base64;

            // Add the Authorization header to the headers dictionary.
            headers.Add("Authorization", basicAuthValue);

            return headers;
        }

        /// <summary>
        /// Creates a Jira ticket/issue
        /// </summary>
        /// <param name="issueJsonPayload">String that represents the jira/ticket JSON payload</param>
        /// <returns>The newly created ticket</returns>
        private async Task<dynamic?> CreateIssue(string issueJsonPayload)
        {
            HttpResponseMessage? response = null;
            dynamic? responseObject = null;

            // Create an HttpContent object with the request body
            var content = new StringContent(issueJsonPayload, Encoding.UTF8, "application/json");

            try
            {
                // Send the POST request
                response = await HttpClient.PostAsync(this.JiraSettings.urlEndpoint, content);

                // Ensure a successful response
                response.EnsureSuccessStatusCode();

                // Read the response content and convert it to a dynamic object
                var responseContent = await response.Content.ReadAsStringAsync();
                responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error creating issue: {ex.Message}");
            }

            return responseObject;
        }

        private async Task<dynamic?> UpdateIssue(string issueKey, string issueJsonPayload)
        {
            // Get Jira API headers as previously defined
            var headers = this.GetJiraApiHeaders();

            HttpResponseMessage? response = null;
            dynamic? responseObject = null;

            // Create the request URI using the issue key
            var requestUri = $"{this.JiraSettings.urlEndpoint}/{issueKey}";

            try
            {
                // Create an HttpContent object with the input data
                var content = new StringContent(issueJsonPayload, Encoding.UTF8, "application/json");

                // Send the PUT request
                response = await HttpClient.PutAsync(requestUri, content);

                // Ensure a successful response
                response.EnsureSuccessStatusCode();

                // Read the response content and convert it to a dynamic object
                var responseContent = await response.Content.ReadAsStringAsync();
                responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error updating issue: {ex.Message}");
            }

            return responseObject;
        }

        /// <summary>
        /// Creates a Jira
        /// </summary>
        /// <param name="issueKey">Jira issue key or id</param>
        /// <returns></returns>
        public async Task<dynamic?> CreateJiraIssue(JaidTicketType ticketType, JaidJiraTemplateVars vars)
        {
            this.OnShowProgressInformation(new ProgressInformationArgs("Creating", $"Creating new Jira"));
            try
            {
                JaidJiraTemplate templateHelper = InstantiateJiraTemplateHelper(ticketType);
                var filledTemplate = templateHelper.FillTemplate(vars);
                var jiraTicket = await this.CreateIssue(filledTemplate);
                return jiraTicket;
            }
            finally
            {
                OnStopProgressTimer(new StopProgressArgs());
            }
        }

        private JaidJiraTemplate InstantiateJiraTemplateHelper(JaidTicketType ticketType)
        {
            return ticketType switch
            {
                JaidTicketType.StagingAmmDeployment => new JaidJiraTemplate(JaidTemplateType.AmmStagingJiraTemplate),
                JaidTicketType.ProductionAmmDeployment => new JaidJiraTemplate(JaidTemplateType.AmmProductionJiraTemplate),
                JaidTicketType.StagingTaxHubDeployment => new JaidJiraTemplate(JaidTemplateType.TaxHubStagingJiraTemplate),
                JaidTicketType.ProductionTaxHubDeployment => new JaidJiraTemplate(JaidTemplateType.TaxHubProductionJiraTemplate),
                _ => new JaidJiraTemplate(JaidTemplateType.AmmStagingJiraTemplate),
            };
        }
    }
}