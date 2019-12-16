using Bijector.Infrastructure.Types.Messages;

namespace Bijector.GDrive.Messages.Commands
{
    public class RenameDriveEntityRejected : IRejectedEvent
    {
        public string EntityId { get; set; }

        public string NewName { get; set; }

        public string Reason { get; set; }
    }
}