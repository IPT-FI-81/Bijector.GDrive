using Bijector.Infrastructure.Types.Messages;

namespace Bijector.GDrive.Messages.Commands
{
    public class CreateDirectory : BaseDriveEntityMessage, ICommand
    {
        public CreateDirectory(){}

        public CreateDirectory(int serviceId, string name, string folderId) : base(serviceId)
        {
            Name = name;
            FolderId = folderId;
        }

        public string Name { get; set; }
        
        public string FolderId { get; set; }
    }
}