using System.Collections.Generic;
using Google.Apis.Drive.v3.Data;
using Bijector.Infrastructure.Types.Messages;
using System;

namespace Bijector.GDrive.Messages.Queries
{
    public class GetDriveEntity : BaseDriveEntityMessage, IQuery<File>
    {
        public GetDriveEntity(){}

        public GetDriveEntity(int serviceId, string id) : base(serviceId)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}