using jaid.common.enums;

namespace jaid.core.events
{
    public class InformationArgs : BaseMessageArgs
    {
        public InformationArgs(string title,
                               string message) : base(MessageSeverity.Information, title, message)
        {
            this.Title = title;
            this.Message = message;
        }

        public string Message { get; }

        public string Title { get; }
    }
}