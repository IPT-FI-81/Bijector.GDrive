using System.Threading.Tasks;
using Google.Apis.Drive.v3.Data;
using Bijector.GDrive.Messages.Queries;
using Bijector.GDrive.Services;
using Bijector.Infrastructure.Handlers;
using Bijector.Infrastructure.Types;

namespace Bijector.GDrive.Handlers.Queries
{
    public class GetDriveEntityHandler : IQueryHandler<GetDriveEntity, File>
    {
        private readonly IGoogleAuthService authService;
        private readonly IServiceIdValidatorService validatorService;

        public GetDriveEntityHandler(IGoogleAuthService authService, IServiceIdValidatorService validatorService)
        {            
            this.authService = authService;
            this.validatorService = validatorService;
        }        

        public async Task<File> Handle(GetDriveEntity query, IContext context)
        {
            if(await validatorService.IsValid(context.UserId, query.ServiceId))
            {
                var gDriveService = new GoogleDriveService(query.ServiceId, authService);
                return await gDriveService.Get(query.Id);
            }
            return null;
        }
    }
}