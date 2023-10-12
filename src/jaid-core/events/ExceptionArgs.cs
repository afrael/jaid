using jaid.common.enums;

namespace jaid.core.events
{
    public class ExceptionArgs : BaseMessageArgs
    {
        public string Message { get; }
        
        public ExceptionArgs(string message) : base(MessageSeverity.Exception, string.Empty, message)
        {
            this.Message = message;
        }

        public ExceptionArgs(string title,
                             string message) : base(MessageSeverity.Exception, title, message)
        {
            this.Message = message;
        }
    }
}