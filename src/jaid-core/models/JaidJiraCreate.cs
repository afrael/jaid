namespace jaid.core.models
{
    public class JaidJiraCreate
    {

        /// <summary>
        /// Description of the issue/jira
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Date time indicating the start of the change window
        /// </summary>
        public DateTime ChangeStartDateTime { get; set; }
        
        /// <summary>
        /// Date time indicating the end of the change window
        /// </summary>
        public DateTime ChangeEndDateTime { get; set; }
        
        /// <summary>
        /// Year fix Version
        /// </summary>
        public string YearFixVersion { get; set; }
        
        /// <summary>
        /// Name of the person who will implement the change
        /// </summary>
        public string Asignee { get; set; }
        
        /// <summary>
        /// Name of the owner of the change
        /// </summary>
        public string Owner { get; set; }
        
        /// <summary>
        /// Define the change type, valid values are
        /// 126575 - Standard
        /// 82505 -  Normal
        /// 126576 - Major
        /// 35155 -  Emergency
        /// </summary>
        public string ChangeType { get; set; }
    }
}