using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Drive.v3.Data;
using Bijector.GDrive.Messages.Queries;
using Bijector.GDrive.Services;
using Bijector.Infrastructure.Handlers;
using Bijector.Infrastructure.Types;
using System.Linq;
using Bijector.GDrive.DTOs;

namespace Bijector.GDrive.Handlers.Queries
{
    public class GetFilesHandler : IQueryHandler<GetFiles, DriveEntityList>
    {
        private readonly IGoogleAuthService authService;
        private readonly IServiceIdValidatorService validatorService;

        public GetFilesHandler(IGoogleAuthService authService, IServiceIdValidatorService validatorService)
        {            
            this.authService = authService;
            this.validatorService = validatorService;
        }        

        public async Task<DriveEntityList> Handle(GetFiles query, IContext context)
        {
            if(await validatorService.IsValid(context.UserId, query.ServiceId))
            {
                var gDriveService = new GoogleDriveService(query.ServiceId, authService);
                if(string.IsNullOrEmpty(query.Name))
                {
                    var entities = await gDriveService.GetFilesAsync();
                    var list = entities.Select(e => new DriveEntity{Name = e.Name, Id = e.Id});
                    return new DriveEntityList{ Entities = list };
                }                    
                else
                {
                    var entities = await gDriveService.GetFilesAsync(query.Name);
                    var list = entities.Select(e => new DriveEntity{Name = e.Name, Id = e.Id});
                    return new DriveEntityList{ Entities = list };
                }
            }
            return null;
        }
    }
}