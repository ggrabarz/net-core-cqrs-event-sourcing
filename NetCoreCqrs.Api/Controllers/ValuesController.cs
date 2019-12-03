using Microsoft.AspNetCore.Mvc;
using NetCoreCqrs.Api.Core.Commands;
using NetCoreCqrs.Api.Core.FakeBus;
using NetCoreCqrs.Api.Core.ReadModel;
using System;

namespace NetCoreCqrs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICommandSender _bus;
        private readonly IReadModelFacade _readmodel;

        public ValuesController(IReadModelFacade readmodel, ICommandSender bus)
        {
            _readmodel = readmodel;
            _bus = bus;
        }

        [HttpGet]
        public ActionResult GetInventoryItems()
        {
            return Ok(_readmodel.GetInventoryItems());
        }

        [HttpGet("{id}")]
        public ActionResult GetItemDetails([FromRoute] Guid id)
        {
            var result = _readmodel.GetInventoryItemDetailsOrDefault(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public ActionResult AddItem([FromQuery] string name)
        {
            _bus.Send(new CreateInventoryItemCommand(Guid.NewGuid(), name));
            return StatusCode(201);
        }

        [HttpPost("{id}/ChangeName")]
        public ActionResult ChangeName([FromRoute] Guid id, [FromQuery] string name, [FromQuery] int version)
        {
            _bus.Send(new RenameInventoryItemCommand(id, name, version));
            return NoContent();
        }

        [HttpPost("{id}/IncreaseAmmount")]
        public ActionResult CheckIn([FromRoute] Guid id, [FromQuery] int number, [FromQuery] int version)
        {
            _bus.Send(new CheckInItemsToInventoryCommand(id, number, version));
            return NoContent();
        }

        [HttpPost("{id}/DecreaseAmmount")]
        public ActionResult Remove([FromRoute] Guid id, [FromQuery] int number, [FromQuery] int version)
        {
            _bus.Send(new RemoveItemsFromInventoryCommand(id, number, version));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Deactivate([FromRoute] Guid id, [FromQuery] int version)
        {
            _bus.Send(new DeactivateInventoryItemCommand(id, version));
            return NoContent();
        }
    }
}
