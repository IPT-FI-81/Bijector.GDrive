using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Bijector.Infrastructure.Dispatchers;
using Bijector.GDrive.Messages.Commands;
using Bijector.GDrive.Messages.Queries;
using Bijector.Infrastructure.Types;

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
            var context = new BaseContext(0, int.Parse(User.Identity.Name), null, "Bijector GDrive");
            await commandDispatcher.SendAsync(command, context);
            return Accepted();
        }

        [Authorize]
        [HttpPost("Move")]
        public async Task<IActionResult> ReName(MoveDriveEntity command)
        {
            var context = new BaseContext(0, int.Parse(User.Identity.Name), null, "Bijector GDrive");
            await commandDispatcher.SendAsync(command, context);
            return Accepted();
        }

        [Authorize]
        [HttpGet("Files")]
        public async Task<IActionResult> GetFiles(GetFiles query)
        {
            var context = new BaseContext(0, int.Parse(User.Identity.Name), null, "Bijector GDrive");
            return new JsonResult(await queryDispatcher.QueryAsync(query, context));
        }

        [Authorize]
        [HttpGet("Directories")]
        public async Task<IActionResult> GetDirectories(GetDirectories query)
        {
            var context = new BaseContext(0, int.Parse(User.Identity.Name), null, "Bijector GDrive");
            return new JsonResult(await queryDispatcher.QueryAsync(query, context));
        }

        [Authorize]
        [HttpGet("Entity")]
        public async Task<IActionResult> GetDriveEntity(GetDriveEntity query)
        {
            var context = new BaseContext(0, int.Parse(User.Identity.Name), null, "Bijector GDrive");
            return new JsonResult(await queryDispatcher.QueryAsync(query, context));
        }
    }
}