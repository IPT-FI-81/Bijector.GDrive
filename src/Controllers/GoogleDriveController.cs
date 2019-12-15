using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bijector.Infrastructure.Dispatchers;
using Bijector.GDrive.Messages.Commands;
using Bijector.Infrastructure.Types;
using System;
using System.Threading.Tasks;

namespace Bijector.GDrive.Controllers
{
    [Route("GDrive")]
    public class GoogleDriveController : Controller
    {
        private readonly IQueryDispatcher queryDispatcher;

        private readonly ICommandDispatcher commandDispatcher;

        public GoogleDriveController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
            this.commandDispatcher = commandDispatcher;
        }

        [Authorize]
        [HttpPost("ReName")]
        public async Task<IActionResult> ReName(RenameDriveEntity command)
        {
            var context = new BaseContext(Guid.Empty, Guid.Parse(User.Identity.Name), null, "Bijector GDrive");
            await commandDispatcher.SendAsync(command, context);
            return Accepted();
        }

        [Authorize]
        [HttpPost("Move")]
        public async Task<IActionResult> ReName(MoveDriveEntity command)
        {
            var context = new BaseContext(Guid.Empty, Guid.Parse(User.Identity.Name), null, "Bijector GDrive");
            await commandDispatcher.SendAsync(command, context);
            return Accepted();
        }
    }
}