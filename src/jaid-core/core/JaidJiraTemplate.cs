using System.Reflection;
using System.Text.RegularExpressions;
using jaid.core.enums;
using Newtonsoft.Json;

namespace jaid.core
{
    public class JaidJiraTemplate
    {
        public JaidJiraTemplate(JaidTemplateType templateType)
        {
            this.Template = this.ResolveTemplate(this.GetTemplateName(templateType));
        }

        private string Template { get; set; }
        private IDictionary<string, string> TemplateKeyValues { get; set; }

        private string GetTemplateName(JaidTemplateType template)
        {
            switch (template)
            {
                case JaidTemplateType.AmmStagingJiraTemplate:
                    return "jaid_core.res.staging-amm-jira.json";

                case JaidTemplateType.AmmProductionJiraTemplate: 
                    return "jaid_core.res.production-amm-jira.json";

                case JaidTemplateType.TaxHubStagingJiraTemplate: 
                    return "jaid_core.res.staging-tax-integration-jira.json";

                case JaidTemplateType.TaxHubProductionJiraTemplate: 
                    return "jaid_core.res.production-tax-integration-jira.json";
                
                default:
                    return string.Empty;
            }
        }

        private string ResolveTemplate(string resourceName)
        {
            string resourceContent = string.Empty;

            // Get the current assembly
            var assembly = Assembly.GetExecutingAssembly();

            // Access the embedded resource stream
            using (var resourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (resourceStream != null)
                {
                    // Read data from the stream into memory (e.g., into a byte array)
                    byte[] buffer = new byte[resourceStream.Length];
                    resourceStream.Read(buffer, 0, buffer.Length);

                    // You can now work with the data in memory as needed
                    resourceContent = System.Text.Encoding.UTF8.GetString(buffer);
                }
            }

            return resourceContent;
        }

        private string PascalCaseToSnakeCase(string input)
        {
            // Use a regular expression to insert underscores before uppercase letters.
            string snakeCase = Regex.Replace(input, "(?<=.)([A-Z])", "_$1").ToLower();

            return "$" + snakeCase;
        }

        public string GetTemplate()
        {
            return this.Template;
        }

        public string GetTemplate(TemplateVariables vars)
        {
            var json = JsonConvert.SerializeObject(vars);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            
            foreach (var kv in dictionary)
            {
               var searchKey = this.PascalCaseToSnakeCase(kv.Key);
               var searchKeyValue = kv.Value;
               this.Template = this.Template.Replace(searchKey, searchKeyValue);
            }

            return this.Template;
        }

    }

    public record TemplateVariables(string ChangeDescription, 
        string ChangeStartTime, 
        string ChangeEndTime, 
        string FixVersionYear,
        string ChangeType,
        string IssueOwner,
        string IssueAssignee,
        string RollbackDurationTime,
        string TestEndingTime);
}