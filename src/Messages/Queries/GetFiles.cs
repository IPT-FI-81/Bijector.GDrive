using System.Collections.Generic;
using Google.Apis.Drive.v3.Data;
using Bijector.Infrastructure.Types.Messages;
using Bijector.GDrive.DTOs;

namespace Bijector.GDrive.Messages.Queries
{
    public class GetFiles : BaseDriveEntityMessage, IQuery<DriveEntityList>
    {
        public GetFiles(){}

        public GetFiles(int serviceId, string name) : base(serviceId)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}