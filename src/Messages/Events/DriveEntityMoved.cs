using Bijector.Infrastructure.Types.Messages;

namespace Bijector.GDrive.Messages.Events
{
    public class DriveEntityMoved : IEvent
    {
        public string EnitityId { get; set; }

        public string SourceId { get; set; }

        public string DestionationId { get; set; }
    }
}