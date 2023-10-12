using jaid.common.enums;

namespace jaid.core.events
{
    public class UpdatedFileArgs : BaseMessageArgs
    {
        public UpdatedFileArgs(string tempUpdatedFile,
                               string updatedFile) : base(MessageSeverity.Unknown, tempUpdatedFile, updatedFile)
        {
            this.TempUpdatedFile = tempUpdatedFile;
            this.UpdatedFile = updatedFile;
        }

        public string TempUpdatedFile { get; }

        public string UpdatedFile { get; }
    }
}