using System.Threading.Tasks;
using Bijector.GDrive.Messages.Commands;
using Bijector.GDrive.Services;
using Bijector.Infrastructure.Handlers;
using Bijector.Infrastructure.Queues;
using Bijector.Infrastructure.Types;
using Bijector.GDrive.Messages.Events;

namespace Bijector.GDrive.Handlers.Commands
{
    public class CreateDirectoryHandler : ICommandHandler<CreateDirectory>
    {
        private readonly IGoogleAuthService authService;

        private readonly IServiceIdValidatorService validatorService;

        private readonly IPublisher publisher;

        public CreateDirectoryHandler(IGoogleAuthService authService, IServiceIdValidatorService validatorService, IPublisher publisher)
        {
            this.authService = authService;
            this.validatorService = validatorService;
            this.publisher = publisher;
        }

        public async Task Handle(CreateDirectory command, IContext context)
        {
            bool isOk = false;
            if(await validatorService.IsValid(context.UserId, command.ServiceId))
            {
                var gDriveService = new GoogleDriveService(command.ServiceId, authService);
                isOk = await gDriveService.CreateDirectory(command.FolderId, command.Name);                                            
            }
            if(isOk)
            {
                var succEvent = new DirectoryCreated
                { 
                    Name = command.Name,
                    FolderId = command.FolderId
                };
                var succContext = new BaseContext(context.Id, context.UserId, "Bijector GDrive", "Bijector Workflows");
                await publisher.Publish(succEvent, succContext);
            }
            else
            {
                var badEvent = new CreateDirectoryRejected
                { 
                    Name = command.Name,
                    FolderId = command.FolderId,
                    Reason = "Service does not linked with user"
                };
                var badContext = new BaseContext(context.Id, context.UserId, "Bijector GDrive", "Bijector Workflows");
                await publisher.Publish(badEvent, badContext);
            }
        }
    }
}