using System.Threading.Tasks;
using Bijector.GDrive.Messages.Commands;
using Bijector.GDrive.Services;
using Bijector.Infrastructure.Handlers;
using Bijector.Infrastructure.Queues;
using Bijector.Infrastructure.Types;
using Bijector.GDrive.Messages.Events;

namespace Bijector.GDrive.Handlers.Commands
{
    public class MoveDriveEntityHandler : ICommandHandler<MoveDriveEntity>
    {
        private readonly IGoogleAuthService authService;

        private readonly IServiceIdValidatorService validatorService;

        private readonly IPublisher publisher;

        public MoveDriveEntityHandler(IGoogleAuthService authService, IServiceIdValidatorService validatorService, IPublisher publisher)
        {
            this.authService = authService;
            this.validatorService = validatorService;
            this.publisher = publisher;
        }

        public async Task Handle(MoveDriveEntity command, IContext context)
        {
            bool isOk = false;
            if(await validatorService.IsValid(context.UserId, command.ServiceId))
            {
                var gDriveService = new GoogleDriveService(command.ServiceId, authService);
                isOk = await gDriveService.Move(command.EntityId, command.DestinationId, command.SourceId);                                            
            }
            if(isOk)
            {
                var succEvent = new DriveEntityMoved
                { 
                    EnitityId = command.EntityId,
                    SourceId = command.SourceId, 
                    DestionationId = command.DestinationId 
                };
                var succContext = new BaseContext(context.Id, context.UserId, "Bijector GDrive", "Bijector Workflows");
                await publisher.Publish(succEvent, succContext);
            }
            else
            {
                var badEvent = new MoveDriveEntityRejected
                { 
                    EnitityId = command.EntityId,
                    SourceId = command.SourceId, 
                    DestionationId = command.DestinationId,
                    Reason = "Service does not linked with user"
                };
                var badContext = new BaseContext(context.Id, context.UserId, "Bijector GDrive", "Bijector Workflows");
                await publisher.Publish(badEvent, badContext);
            }
        }
    }
}