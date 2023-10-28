using System.Collections;
using jaid.core;
using jaid.core.enums;
using Newtonsoft.Json;

namespace jaid_tests
{
    public class JaidTemplateTests
    {
        [Theory]
        [InlineData(JaidTemplateType.AmmStagingJiraTemplate)]
        [InlineData(JaidTemplateType.AmmProductionJiraTemplate)]
        [InlineData(JaidTemplateType.TaxHubStagingJiraTemplate)]
        [InlineData(JaidTemplateType.TaxHubProductionJiraTemplate)]
        public void Successfully_Loads_Jira_Templates_By_Type(JaidTemplateType value)
        {
            var jtemplate = new JaidJiraTemplate(value);
            Assert.NotEmpty(jtemplate.GetTemplate());
        }

        [Theory]
        [ClassData(typeof(TemplateVariablesData))]
        public void Successfully_Fills_Amm_Jira_Template_With_Values(TemplateVariables templateVariables, JaidTemplateType templateType)
        {
            var jtemplate = new JaidJiraTemplate(templateType);
            var filledTemplate = jtemplate.GetTemplate(templateVariables);
            Assert.NotEmpty(filledTemplate);
            var jiraTemplate = JsonConvert.DeserializeObject<dynamic>(filledTemplate);
            Assert.Equal(jiraTemplate.fields.customfield_11541.ToString(), templateVariables.ChangeDescription);       
            Assert.Equal(jiraTemplate.fields.customfield_11546.ToString(), templateVariables.ChangeStartTime);       
            Assert.Equal(jiraTemplate.fields.customfield_11547.ToString(), templateVariables.ChangeEndTime);       
            Assert.Equal(jiraTemplate.fields.fixVersions[0].name.ToString(), templateVariables.FixVersionYear);       
            Assert.Equal(jiraTemplate.fields.customfield_11545.name.ToString(), templateVariables.IssueAssignee);       
            Assert.Equal(jiraTemplate.fields.customfield_11544.name.ToString(), templateVariables.IssueAssignee);       
            Assert.Equal(jiraTemplate.fields.customfield_11543.id.ToString(), templateVariables.ChangeType);       
            Assert.Equal(jiraTemplate.fields.customfield_11543.self.ToString(), $"https://ultidev.ultimatesoftware.com/rest/api/2/customFieldOption/{templateVariables.ChangeType}");
            Assert.Equal(jiraTemplate.fields.customfield_10756.name.ToString(), templateVariables.IssueOwner);
            Assert.Equal(jiraTemplate.fields.customfield_11351.ToString(), $"{templateVariables.RollbackDurationTime} minutes");
            Assert.Equal(jiraTemplate.fields.customfield_22425.name.ToString(), templateVariables.IssueAssignee);
            Assert.Equal(jiraTemplate.fields.customfield_22424.ToString(), templateVariables.TestEndingTime);
            Assert.Equal(jiraTemplate.fields.customfield_10756.name.ToString(), templateVariables.IssueOwner);
        }
    }

    // Test Data Class to test Jira Templates    
    public class TemplateVariablesData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] 
                { 
                    new TemplateVariables(
                    ChangeDescription : "__description__", 
                    ChangeStartTime : "__start_time__", 
                    ChangeEndTime : "__end_time__", 
                    FixVersionYear : "__fix_version_year__",
                    ChangeType : "__change_type__",
                    IssueOwner : "__issue_owner__",
                    IssueAssignee : "__issue_assignee__",
                    RollbackDurationTime : "__rollback_duration__",
                    TestEndingTime : "__test_ending_time__"),
                    JaidTemplateType.AmmStagingJiraTemplate
                };
            yield return new object[] 
                { 
                    new TemplateVariables(
                    ChangeDescription : "__description__", 
                    ChangeStartTime : "__start_time__", 
                    ChangeEndTime : "__end_time__", 
                    FixVersionYear : "__fix_version_year__",
                    ChangeType : "__change_type__",
                    IssueOwner : "__issue_owner__",
                    IssueAssignee : "__issue_assignee__",
                    RollbackDurationTime : "__rollback_duration__",
                    TestEndingTime : "__test_ending_time__"),
                    JaidTemplateType.AmmProductionJiraTemplate
                };
            yield return new object[] 
                { 
                    new TemplateVariables(
                    ChangeDescription : "__description__", 
                    ChangeStartTime : "__start_time__", 
                    ChangeEndTime : "__end_time__", 
                    FixVersionYear : "__fix_version_year__",
                    ChangeType : "__change_type__",
                    IssueOwner : "__issue_owner__",
                    IssueAssignee : "__issue_assignee__",
                    RollbackDurationTime : "__rollback_duration__",
                    TestEndingTime : "__test_ending_time__"),
                    JaidTemplateType.TaxHubStagingJiraTemplate
                };
            yield return new object[] 
                { 
                    new TemplateVariables(
                    ChangeDescription : "__description__", 
                    ChangeStartTime : "__start_time__", 
                    ChangeEndTime : "__end_time__", 
                    FixVersionYear : "__fix_version_year__",
                    ChangeType : "__change_type__",
                    IssueOwner : "__issue_owner__",
                    IssueAssignee : "__issue_assignee__",
                    RollbackDurationTime : "__rollback_duration__",
                    TestEndingTime : "__test_ending_time__"),
                    JaidTemplateType.TaxHubProductionJiraTemplate
                };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
