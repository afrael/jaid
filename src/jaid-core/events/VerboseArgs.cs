using jaid.common.enums;

namespace jaid.core.events
{
    public class VerboseArgs : BaseMessageArgs
    {
        public VerboseArgs(string title, string message) : base(MessageSeverity.Verbose, title, message)
        {
            this.Title = title;
            this.Message = message;
        }

        public string Title { get; }

        public string Message { get; }
    }
}