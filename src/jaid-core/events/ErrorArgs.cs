using jaid.common.enums;

namespace jaid.core.events
{
    public class ErrorArgs : BaseMessageArgs
    {
        public string Message { get; }
        
        public ErrorArgs(string message) : base(MessageSeverity.Error, string.Empty, message)
        {
            this.Message = message;
        }

        public ErrorArgs(string title,
                         string message) : base(MessageSeverity.Error, title, message)
        {
            this.Message = message;
        }
    }
}