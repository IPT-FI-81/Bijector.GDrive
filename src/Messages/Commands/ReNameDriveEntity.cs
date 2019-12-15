using System;
using Bijector.Infrastructure.Types.Messages;

namespace Bijector.GDrive.Messages.Commands
{
    public class RenameDriveEntity : BaseDriveEntityCommand
    {
        public RenameDriveEntity(Guid serviceId, string id, string newName) : base(serviceId)
        {
            Id = id;
            NewName = newName;
        }

        public string Id { get; }
        public string NewName { get; }
    }
}