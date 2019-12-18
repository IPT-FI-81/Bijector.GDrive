using Bijector.Infrastructure.Types.Messages;

namespace Bijector.GDrive.Messages.Commands
{
    public class MoveDriveEntity : BaseDriveEntityMessage, ICommand
    {
        public MoveDriveEntity(){}

        public MoveDriveEntity(int serviceId, string entityId, string destinationId) : base(serviceId)
        {            
            EntityId = entityId;
            DestinationId = destinationId;            
        }
        
        public string EntityId { get; set; }
        public string DestinationId { get; set; }        
    }
}