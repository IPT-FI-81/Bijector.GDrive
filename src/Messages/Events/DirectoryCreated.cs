using Bijector.Infrastructure.Types.Messages;

namespace Bijector.GDrive.Messages.Events
{
    public class DirectoryCreated : IEvent
    {        
        public string FolderId { get; set; }

        public string Name { get; set; }
    }
}