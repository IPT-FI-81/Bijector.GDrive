using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Drive.v3.Data;
using Bijector.GDrive.Messages.Queries;
using Bijector.GDrive.Services;
using Bijector.Infrastructure.Handlers;
using Bijector.Infrastructure.Types;

namespace Bijector.GDrive.Handlers.Queries
{
    public class GetDirectoriesHandler : IQueryHandler<GetDirectories, IEnumerable<File>>
    {
        private readonly IGoogleAuthService authService;
        private readonly IServiceIdValidatorService validatorService;

        public GetDirectoriesHandler(IGoogleAuthService authService, IServiceIdValidatorService validatorService)
        {            
            this.authService = authService;
            this.validatorService = validatorService;
        }        

        public async Task<IEnumerable<File>> Handle(GetDirectories query, IContext context)
        {
            if(await validatorService.IsValid(context.Id, query.ServiceId))
            {
                var gDriveService = new GoogleDriveService(query.ServiceId, authService);
                if(string.IsNullOrEmpty(query.Name))
                    return await gDriveService.GetDirectoryAsync();
                else
                    return await gDriveService.GetDirectoryAsync(query.Name);
            }
            return null;
        }
    }
}