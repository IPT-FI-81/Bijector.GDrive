using System;
using Bijector.Infrastructure.Types.Messages;

namespace Bijector.GDrive.Messages.Commands
{
    public class MoveDriveEntity : BaseDriveEntityMessage, ICommand
    {
        public MoveDriveEntity(Guid serviceId, string entityId, string destinationId, string sourceId) : base(serviceId)
        {            
            EntityId = entityId;
            DestinationId = destinationId;
            SourceId = sourceId;
        }
        
        public string EntityId { get; }
        public string DestinationId { get; }
        public string SourceId { get; }
    }
}