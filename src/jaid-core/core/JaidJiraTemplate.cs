using System.Reflection;
using System.Text.RegularExpressions;
using jaid.core.enums;
using Newtonsoft.Json;

namespace jaid.core
{
    /// <summary>
    /// Jira Issue class that encapsulates a fillable template
    /// </summary>
    public class JaidJiraTemplate
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="templateType">Indicates the template type</param>
        public JaidJiraTemplate(JaidTemplateType templateType)
        {
            this.Template = this.RetrieveResourceFromAssembly(this.GetTemplateName(templateType));
        }

        private string Template { get; set; }
        private IDictionary<string, string> TemplateKeyValues { get; set; }

        /// <summary>
        /// Retrieves the template resource name based on the template type
        /// </summary>
        /// <param name="template">The Jira Template Types</param>
        /// <returns>A string with the fillable template</returns>
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

        /// <summary>
        /// Extracts the template from the assembly
        /// </summary>
        /// <param name="resourceName">The resource name to retrieve</param>
        /// <returns></returns>
        private string RetrieveResourceFromAssembly(string resourceName)
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

        /// <summary>
        /// Helper function to convert a string 
        /// from --> PascalTypeVariable
        /// to --> $pascal_type_variable
        /// 
        /// That is the format of the variables in the templates
        /// </summary>
        /// <param name="stringToTransform">The string to transfrom</param>
        /// <returns></returns>
        private string PascalCaseToSnakeCase(string stringToTransform)
        {
            // Use a regular expression to insert underscores before uppercase letters.
            string snakeCase = Regex.Replace(stringToTransform, "(?<=.)([A-Z])", "_$1").ToLower();

            return "$" + snakeCase;
        }

        /// <summary>
        /// Retrieces the template, helper method to facilitate testing
        /// </summary>
        /// <returns>String with the template values</returns>
        public string GetUnfilledTemplate()
        {
            return this.Template;
        }

        /// <summary>
        /// Fills the template with the supplied template variables
        /// </summary>
        /// <param name="templateVariables">A record with all the values to fill in the template</param>
        /// <returns>A string that represent the filled template, a JSON payload</returns>
        public string FillTemplate(JaidJiraTemplateVars templateVariables)
        {
            var json = JsonConvert.SerializeObject(templateVariables);
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
}