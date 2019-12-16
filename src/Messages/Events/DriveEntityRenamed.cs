using Bijector.Infrastructure.Types.Messages;

namespace Bijector.GDrive.Messages.Commands
{
    public class DriveEntityRenamed : IEvent
    {
        public string EntityId { get; set; }
        public string NewName { get; set; }
    }
}