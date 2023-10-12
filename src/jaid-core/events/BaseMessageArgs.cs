using jaid.common.enums;

namespace jaid.core.events
{
    public class BaseMessageArgs : EventArgs
    {
         readonly MessageSeverity _severity;
         readonly string _title;
         readonly IEnumerable<string> _messages;
         readonly string _message;

        public BaseMessageArgs(MessageSeverity severity,
                               string title,
                               string message)
        {
            _severity = severity;
            _title = title;
            _message = message;
        }
        
        public BaseMessageArgs(MessageSeverity severity,
                               string title,
                               IEnumerable<string> messages)
        {
            _severity = severity;
            _title = title;
            _messages = messages;
        }
    }
}