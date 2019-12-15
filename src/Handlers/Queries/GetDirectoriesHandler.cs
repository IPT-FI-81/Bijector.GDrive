using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Google.Apis.Drive.v3.Data;
using Bijector.GDrive.Messages.Queries;
using Bijector.GDrive.Services;
using Bijector.GDrive.DTOs;
using Bijector.Infrastructure.Handlers;
using Bijector.Infrastructure.Types;

namespace Bijector.GDrive.Handlers.Queries
{
    public class GetDirectoriesHandler : IQueryHandler<GetDirectories, DriveEntityList>
    {
        private readonly IGoogleAuthService authService;
        private readonly IServiceIdValidatorService validatorService;

        public GetDirectoriesHandler(IGoogleAuthService authService, IServiceIdValidatorService validatorService)
        {            
            this.authService = authService;
            this.validatorService = validatorService;
        }        

        public async Task<DriveEntityList> Handle(GetDirectories query, IContext context)
        {
            if(await validatorService.IsValid(context.UserId, query.ServiceId))
            {
                var gDriveService = new GoogleDriveService(query.ServiceId, authService);
                if(string.IsNullOrEmpty(query.Name))
                {

                    var entities = await gDriveService.GetDirectoryAsync();
                    var list = entities.Select(e => new DriveEntity{Name = e.Name, Id = e.Id});
                    return new DriveEntityList{ Entities = list };
                }                    
                else
                {
                    var entities = await gDriveService.GetDirectoryAsync(query.Name);
                    var list = entities.Select(e => new DriveEntity{Name = e.Name, Id = e.Id});
                    return new DriveEntityList{ Entities = list };
                }                    
            }
            return null;
        }
    }
}