using System.Threading.Tasks;
using Bijector.GDrive.Messages.Commands;
using Bijector.GDrive.Services;
using Bijector.Infrastructure.Handlers;
using Bijector.Infrastructure.Types;

namespace Bijector.GDrive.Handlers.Commands
{
    public class MoveDriveEntityHandler : ICommandHandler<MoveDriveEntity>
    {
        private readonly IGoogleAuthService authService;

        private readonly IServiceIdValidatorService validatorService;

        public MoveDriveEntityHandler(IGoogleAuthService authService, IServiceIdValidatorService validatorService)
        {
            this.authService = authService;
            this.validatorService = validatorService;
        }

        public async Task Handle(MoveDriveEntity command, IContext context)
        {
            if(await validatorService.IsValid(context.UserId, command.ServiceId))
            {
                var gDriveService = new GoogleDriveService(command.ServiceId, authService);
                await gDriveService.Move(command.EntityId, command.DestinationId, command.SourceId);
            }
        }
    }
}