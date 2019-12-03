using Microsoft.AspNetCore.Mvc;
using NetCoreCqrs.Api.Core;
using System;

namespace NetCoreCqrs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ICommandSender _bus;
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
            return Ok(_readmodel.GetInventoryItemDetails(id));
        }

        [HttpPost]
        public ActionResult AddItem([FromQuery] string name)
        {
            _bus.Send(new CreateInventoryItem(Guid.NewGuid(), name));
            return StatusCode(201);
        }

        [HttpPost("{id}/ChangeName")]
        public ActionResult ChangeName([FromRoute] Guid id, [FromQuery] string name, [FromQuery] int version)
        {
            _bus.Send(new RenameInventoryItem(id, name, version));
            return NoContent();
        }

        [HttpPost("{id}/Deactivate")]
        public ActionResult Deactivate([FromRoute] Guid id, [FromQuery] int version)
        {
            _bus.Send(new DeactivateInventoryItem(id, version));
            return NoContent();
        }

        [HttpPost("{id}/CheckIn")]
        public ActionResult CheckIn([FromRoute] Guid id, [FromQuery] int number, [FromQuery] int version)
        {
            _bus.Send(new CheckInItemsToInventory(id, number, version));
            return NoContent();
        }

        [HttpPost("{id}/Remove")]
        public ActionResult Remove([FromRoute] Guid id, [FromQuery] int number, [FromQuery] int version)
        {
            _bus.Send(new RemoveItemsFromInventory(id, number, version));
            return NoContent();
        }
    }
}
