using System;
using jaid.common.enums;

namespace jaid.core.events
{
    public class ProgressInformationArgs : BaseMessageArgs
    {
        public ProgressInformationArgs(string waitMessage,
                                       string text) : base(MessageSeverity.Unknown, waitMessage, text)
        {
            this.WaitMessage = waitMessage;
            this.Text = text;
        }

        public string Text { get; }

        public string WaitMessage { get; }
    }

    public class StopProgressArgs : EventArgs
    {
        
    }
}