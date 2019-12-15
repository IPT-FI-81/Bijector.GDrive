using System;
using Bijector.Infrastructure.Types.Messages;

namespace Bijector.GDrive.Messages.Commands
{
    public abstract class BaseDriveEntityCommand : ICommand
    {
        public BaseDriveEntityCommand(Guid serviceId)
        {
            ServiceId = serviceId;
        }

        public Guid ServiceId { get; }
    }
}