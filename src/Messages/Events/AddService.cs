using Bijector.Infrastructure.Types.Messages;

namespace Bijector.GDrive.Messages.Events
{
    public class AddService : IEvent
    {
        public int UserServiceId { get; set; }

        public string ServiceName { get; set; }

        public string UserServiceName { get; set; }

        public string ServiceType { get; set; }
    }
}