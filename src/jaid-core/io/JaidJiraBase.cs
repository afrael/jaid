using jaid.core.events;

namespace jaid.core.io
{
    /// <summary>
    /// Jaid Jira Base Class
    /// </summary>
    public class JaidJiraBase
    {
        public event EventHandler<ProgressInformationArgs> ShowProgressInformation;
        public event EventHandler<StopProgressArgs> StopProgressTimer;

         public void OnStopProgressTimer(StopProgressArgs stopProgressArgs)
        {
            StopProgressTimer?.Invoke(this, stopProgressArgs);
        }

        public void OnShowProgressInformation(ProgressInformationArgs progressInformationArgs)
        {
            ShowProgressInformation?.Invoke(this, progressInformationArgs);
        }
    }
}