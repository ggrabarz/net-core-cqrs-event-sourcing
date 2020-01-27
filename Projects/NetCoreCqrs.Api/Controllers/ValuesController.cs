using Microsoft.AspNetCore.Mvc;
using NetCoreCqrs.Application.App_Infrastructure.CqrsBuildingBlocks;
using NetCoreCqrs.Application.Commands.Inventory;
using NetCoreCqrs.ReadModel.App_Infrastructure.CqrsBuildingBlocks;
using NetCoreCqrs.ReadModel.Queries.Inventory;
using NetCoreCqrs.ReadModel.Queries.Inventory.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreCqrs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public ValuesController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public async Task<ActionResult> GetInventoryItems()
        {
            return Ok(await _queryDispatcher.DispatchAsync<GetInventoryItemsQuery, IEnumerable<InventoryItemListDto>>(new GetInventoryItemsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetItemDetails([FromRoute] Guid id)
        {
            var result = await _queryDispatcher.DispatchAsync<GetItemDetailsQuery, InventoryItemDetailsDto>(new GetItemDetailsQuery(id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddItem([FromQuery] string name)
        {
            var newItemId = Guid.NewGuid();
            await _commandDispatcher.DispatchAsync(new CreateInventoryItemCommand(newItemId, name));
            return StatusCode(201, newItemId.ToString());
        }

        [HttpPost("{id}/ChangeName")]
        public async Task<ActionResult> ChangeName([FromRoute] Guid id, [FromQuery] string name, [FromQuery] int version)
        {
            await _commandDispatcher.DispatchAsync(new RenameInventoryItemCommand(id, name, version));
            return NoContent();
        }

        [HttpPost("{id}/IncreaseAmmount")]
        public async Task<ActionResult> CheckIn([FromRoute] Guid id, [FromQuery] int number, [FromQuery] int version)
        {
            await _commandDispatcher.DispatchAsync(new CheckInItemsToInventoryCommand(id, number, version));
            return NoContent();
        }

        [HttpPost("{id}/DecreaseAmmount")]
        public async Task<ActionResult> Remove([FromRoute] Guid id, [FromQuery] int number, [FromQuery] int version)
        {
            await _commandDispatcher.DispatchAsync(new RemoveItemsFromInventoryCommand(id, number, version));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Deactivate([FromRoute] Guid id, [FromQuery] int version)
        {
            await _commandDispatcher.DispatchAsync(new DeactivateInventoryItemCommand(id, version));
            return NoContent();
        }
    }
}
