using System.Threading.Tasks;
using Google.Apis.Drive.v3.Data;
using Bijector.GDrive.Messages.Queries;
using Bijector.GDrive.Services;
using Bijector.Infrastructure.Handlers;
using Bijector.Infrastructure.Types;
using Bijector.GDrive.DTOs;

namespace Bijector.GDrive.Handlers.Queries
{
    public class GetDriveEntityHandler : IQueryHandler<GetDriveEntity, DriveEntity>
    {
        private readonly IGoogleAuthService authService;
        private readonly IServiceIdValidatorService validatorService;

        public GetDriveEntityHandler(IGoogleAuthService authService, IServiceIdValidatorService validatorService)
        {            
            this.authService = authService;
            this.validatorService = validatorService;
        }        

        public async Task<DriveEntity> Handle(GetDriveEntity query, IContext context)
        {
            if(await validatorService.IsValid(context.UserId, query.ServiceId))
            {
                var gDriveService = new GoogleDriveService(query.ServiceId, authService);
                var entity = await gDriveService.Get(query.Id);
                return new DriveEntity { Name = entity.Name, Id = entity.Id };
            }
            return null;
        }
    }
}