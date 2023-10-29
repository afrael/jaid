namespace jaid.core
{
    /// <summary>
    /// A record with all the fillable values in a jira template
    /// </summary>
    /// <param name="ChangeDescription">The change description, the summary, or title</param>
    /// <param name="ChangeStartTime">The moment the change starts</param>
    /// <param name="ChangeEndTime">The moment the change ends</param>
    /// <param name="FixVersionYear">The current year</param>
    /// <param name="ChangeType">The change type</param>
    /// <param name="IssueOwner">The owner, or reporter</param>
    /// <param name="IssueAssignee">Who will carry ou tthe change</param>
    /// <param name="RollbackDurationTime">Estimated rollback duration</param>
    /// <param name="TestEndingTime">Time when the testing will end</param>
    public record JaidJiraTemplateVars(string ChangeDescription,
        string ChangeStartTime,
        string ChangeEndTime,
        string FixVersionYear,
        string ChangeType,
        string IssueOwner,
        string IssueAssignee,
        string ChangeStartingTime,
        string ChangeEndingTime,
        string TestStartingTime,
        string TestEndingTime,
        string RollbackDurationTime,
        string DeploymentDocumentation);
}