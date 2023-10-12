using jaid.common.enums;

namespace jaid.core.events
{
    public class WarningArgs : BaseMessageArgs
    {
        public WarningArgs(string title,
                           string message) : base(MessageSeverity.Warning, title, message)
        {
            this.Title = title;
            this.Message = message;
        }

        public WarningArgs(string title,
                           IEnumerable<string> messages) : base(MessageSeverity.Warning, title, messages)
        {
            this.Title = title;
            this.Messages = messages;
        }

        public string Message { get; set; }

        public string Title { get; set; }

        public IEnumerable<string> Messages { get; set; }

        public bool IsMultiPart => this.Messages == null || !this.Messages.Any();
    }
}