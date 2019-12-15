using System;

namespace Bijector.GDrive.Messages
{
    public abstract class BaseDriveEntityMessage
    {
        public BaseDriveEntityMessage(Guid serviceId)
        {
            ServiceId = serviceId;
        }

        public Guid ServiceId { get; }
    }
}