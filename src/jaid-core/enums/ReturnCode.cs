namespace jaid.core.enums
{
    /// <summary>
    /// Predefined CLI return codes
    /// </summary>
    public enum ReturnCode
    {
        /// <summary>
        /// Successfultl execution
        /// </summary>
        Success = 0,
        
        /// <summary>
        /// Error while reading the Jira endpoint repository
        /// </summary>
        ErrorWhileReadingJira = -10,
        
        /// <summary>
        /// The input command have errors
        /// </summary>
        CommandHasErrors = -20,
        
        /// <summary>
        /// Error while creating the Jira ticket
        /// </summary>
        ErrorWhileCreatingJiraTicket = -30,
        
        /// <summary>
        /// Error while processing the Jira request
        /// </summary>
        ErrorWhileProcessingJiraRequest = -40,
        
        /// <summary>
        /// Unexpected error
        /// </summary>
        UnexpectedError = -100
    }
}
