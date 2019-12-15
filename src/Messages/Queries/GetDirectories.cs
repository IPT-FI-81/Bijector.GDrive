using System.Collections.Generic;
using Google.Apis.Drive.v3.Data;
using Bijector.Infrastructure.Types.Messages;
using System;

namespace Bijector.GDrive.Messages.Queries
{
    public class GetDirectories : BaseDriveEntityMessage, IQuery<IEnumerable<File>>
    {
        public GetDirectories(Guid serviceId, string name) : base(serviceId)
        {
            Name = name;
        }

        public string Name { get; }
    }
}