using CommandLine;

namespace jaid.models
{
    /*
    jaid will handle the following verbs and options
        - create
            -p <project>
            -t <issue-type>
            -s <summary>
            -w <downtime>
            -r <change-risk-imapct>
            -g <change-type>
            -f <impactful-to-sales>
            -l <labels>
            -d <description>
            -k <risk-assessment>
            -c <components>
            -i <implementer>
            -o <owner>
            -e <tester>
            -q <requestor>
            -h <change-classification>
            -a <start-time>
            -z <end-time>
            -m <description-of-impact>
            -v <devices-affected>
            -y <implementation-steps>
            -n <test-plan>
            -k <rollback-plan>
            -d <data-center>
            -v <environment>

        - get
            -i <jira-id>

        - list
            -q <jiraiquery>
    */

    [Verb("new", HelpText = "Create a Jira Item")]
    public class NewOptions
    {
        [Option('p', "project", Required = true, HelpText = "Jira project type, current values: pro (PRO), sd (SaaS).")]
        public string Project { get; set; } = string.Empty;

        [Option('t', "issue-type", Required = true, HelpText = "Issue type, current values: pro (PRO), sd (SaaS).")]
        public string IssueType { get; set; } = string.Empty;

        [Option('s', "summary", Required = true, HelpText = "Issue summary, or title.")]
        public string Summary { get; set; } = string.Empty;

        [Option('w', "downtime", Default = (string)"No", Required = true, HelpText = "Is downtime required to complete the issue.")]
        public string Downtime { get; set; } = string.Empty;

        [Option('r', "change-risk-impact", Default = (string)"Low", Required = true, HelpText = "What is the impact to sales.")]
        public string ChangeRiskImpact { get; set; } = string.Empty;

        [Option('g', "change-type", Default = (string)"Normal", Required = true, HelpText = "Issue Type")]
        public string ChangeType { get; set; } = string.Empty;

        [Option('f', "impactful-to-sales", Default = (string)"No", Required = true, HelpText = "Issue is impactful to sales")]
        public string ImpactfulToSales { get; set; } = string.Empty;

        [Option('l', "labels", Default = (string)"PaymentServices", Required = true, HelpText = "Issue labels")]
        public string Labels { get; set; } = string.Empty;

        [Option('d', "description", Required = true, HelpText = "Issue description")]
        public string Description { get; set; } = string.Empty;

        [Option('k', "risk-assessment", Required = true, HelpText = "Issue Risk Assessment")]
        public string RiskAssesment { get; set; } = string.Empty;

        [Option('c', "components", Default = (string)"PS US Tax Setup", Required = true, HelpText = "Issue summary, or title")]
        public string Components { get; set; } = string.Empty;

        [Option('i', "Implementer", Default = (string)"Carlos Lores", Required = true, HelpText = "Issue Implementer")]
        public string Implementer { get; set; } = string.Empty;

        [Option('o', "owner", Default = (string)"Afrael Ortiz", Required = true, HelpText = "Issue Owner")]
        public string Owner { get; set; } = string.Empty;

        [Option('e', "tester", Default = (string)"Carlos Lores", Required = true, HelpText = "Issue Tester")]
        public string Tester { get; set; } = string.Empty;

        [Option('q', "requestor", Default = (string)"Afrael Ortiz", Required = true, HelpText = "Issue Requestor")]
        public string Requestor { get; set; } = string.Empty;

        [Option('h', "change-classification", Default = (string)"App Owner - Application Maintenance", Required = true, HelpText = "Issue Change Classification")]
        public string ChangeClassification { get; set; } = string.Empty;

        [Option('a', "start-date-time", Required = true, HelpText = "Issue Start Date Time, beginning of change window")]
        public string StartDateTime { get; set; } = string.Empty;

        [Option('z', "end-date-time", Required = true, HelpText = "Issue End Date Time, end of change window")]
        public string EndDateTime { get; set; } = string.Empty;

        [Option('m', "description-of-impact", Required = true, HelpText = "Description of the Impact of the Issue ")]
        public string DescriptionOfImpact { get; set; } = string.Empty;

        [Option('v', "devices-affected", Required = true, HelpText = "Which devices does this issue affect")]
        public string DevicesAfected { get; set; } = string.Empty;

        [Option('y', "implementation-steps", Required = true, HelpText = "Implementation steps of the issue")]
        public string ImplemtationSteps { get; set; } = string.Empty;

        [Option('n', "test-plan", Required = true, HelpText = "Test plan for the issue")]
        public string TestPlan { get; set; } = string.Empty;

        [Option('k', "rollback-plan", Required = true, HelpText = "Issue Rollback Plan")]
        public string RollbackPlan { get; set; } = string.Empty;

        [Option('d', "data-center", Default = "(string)Atlanta", Required = true, HelpText = "Data center affected by the Issue")]
        public string DataCenter { get; set; } = string.Empty;

        [Option('v', "environment", Default = (string)"ETAXINT-PROD", Required = true, HelpText = "Environment affected by the issue")]
        public string Environment { get; set; }
    }

    [Verb("get", HelpText = "Get a Jira Item")]
    public class GetOptions
    {
        [Option('i', "jira-id", Required = true, HelpText = "Jira project type, current values: pro (PRO), sd (SaaS).")]
        public string JiraId { get; set; } = string.Empty;
    }

    [Verb("list", HelpText = "Execute a Jira Query")]
    public class ListOptions
    {
        [Option('q', "jira-query", Required = true, HelpText = "Jira project type, current values: pro (PRO), sd (SaaS).")]
        public string JiraQuery { get; set; } = string.Empty;
    }
}