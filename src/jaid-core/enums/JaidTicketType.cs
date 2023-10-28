namespace jaid.core.enums
{
    /// <summary>
    /// Ticket type, or intent of the jira ticket
    /// </summary>
    public enum JaidTicketType
    {
        StagingAmmDeployment = 100,
        ProductionAmmDeployment = 200,
        StagingTaxHubDeployment = 300,
        ProductionTaxHubDeployment = 400,
        GenericSdSreTicket = 500
    }
}