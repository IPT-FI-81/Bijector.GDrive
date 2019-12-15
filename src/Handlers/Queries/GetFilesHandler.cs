using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Drive.v3.Data;
using Bijector.GDrive.Messages.Queries;
using Bijector.GDrive.Services;
using Bijector.Infrastructure.Handlers;
using Bijector.Infrastructure.Types;

namespace Bijector.GDrive.Handlers.Queries
{
    public class GetFilesHandler : IQueryHandler<GetFiles, IEnumerable<File>>
    {
        private readonly IGoogleAuthService authService;
        private readonly IServiceIdValidatorService validatorService;

        public GetFilesHandler(IGoogleAuthService authService, IServiceIdValidatorService validatorService)
        {            
            this.authService = authService;
            this.validatorService = validatorService;
        }        

        public async Task<IEnumerable<File>> Handle(GetFiles query, IContext context)
        {
            if(await validatorService.IsValid(context.UserId, query.ServiceId))
            {
                var gDriveService = new GoogleDriveService(query.ServiceId, authService);
                if(string.IsNullOrEmpty(query.Name))
                    return await gDriveService.GetFilesAsync();
                else
                    return await gDriveService.GetFilesAsync(query.Name);
            }
            return null;
        }
    }
}