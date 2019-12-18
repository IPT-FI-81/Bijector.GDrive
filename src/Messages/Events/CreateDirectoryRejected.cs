using Bijector.Infrastructure.Types.Messages;

namespace Bijector.GDrive.Messages.Events
{
    public class CreateDirectoryRejected : IRejectedEvent
    {
        public string Name { get; set; }

        public string FolderId { get; set; }

        public string Reason { get; set; }
    }
}