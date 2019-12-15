using System.Threading.Tasks;
using Bijector.GDrive.Messages.Commands;
using Bijector.GDrive.Services;
using Bijector.Infrastructure.Handlers;
using Bijector.Infrastructure.Queues;
using Bijector.Infrastructure.Types;

namespace Bijector.GDrive.Handlers.Commands
{
    public class RenameDriveEntityHandler : ICommandHandler<RenameDriveEntity>
    {
        private readonly IGoogleAuthService authService;

        private readonly IServiceIdValidatorService validatorService;
        
        private readonly IPublisher publisher;

        public RenameDriveEntityHandler(IGoogleAuthService authService, IServiceIdValidatorService validatorService, IPublisher publisher)
        {
            this.authService = authService;
            this.validatorService = validatorService;
            this.publisher = publisher;
        }

        public async Task Handle(RenameDriveEntity command, IContext context)
        {
            if(await validatorService.IsValid(context.UserId, command.ServiceId))
            {
                var gDriveService = new GoogleDriveService(command.ServiceId, authService);
                await gDriveService.ReName(command.Id, command.NewName);

                var succEvent = new DriveEntityRenamed
                {
                    NewName = command.NewName,
                    Id = command.Id
                };
                var succContext = new BaseContext(context.Id, context.UserId, "Bijector GDrive", "Bijector Workflows");
                await publisher.Publish(succEvent, succContext);
            }
            else
            {
                var badEvent = new RenameDriveEntityRejected
                {
                    Id = command.Id,
                    NewName = command.NewName,
                    Reason = "User does not linked with service"
                };
                var badContext = new BaseContext(context.Id, context.UserId, "Bijector GDrive", "Bijector Workflows");
                await publisher.Publish(badEvent, badContext);
            }
        }
    }
}