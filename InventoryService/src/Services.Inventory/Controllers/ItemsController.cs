using Common;
using Microsoft.AspNetCore.Mvc;
using Services.Inventory.Dtos;
using Services.Inventory.Entities;

namespace Services.Inventory.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<InventoryItem> itemsRepository;

        public ItemsController(IRepository<InventoryItem> itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> Get(Guid userId)
        {
            if (userId == Guid.Empty) return BadRequest();

            var items = (await itemsRepository.GetAllAsync(item => item.UserId == userId)).Select(item => item.AsDto());

            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult> Post(GrantItemsDto grantItemsDto)
        {
            var InventoryItem = await itemsRepository.GetAsync(item => item.UserId == grantItemsDto.UserId && item.CatalogItemId == grantItemsDto.CatalogItemId);

            if (InventoryItem == null)
            {
                InventoryItem = new InventoryItem
                {
                CatalogItemId = grantItemsDto.CatalogItemId,
                UserId = grantItemsDto.UserId,
                Quantity = grantItemsDto.Quantity,
                AcquiredDate = DateTimeOffset.UtcNow
                };
            } 
            else
            {
                InventoryItem.Quantity += grantItemsDto.Quantity;
                await itemsRepository.UpdateAsync(InventoryItem);    
            }

            return Ok();
        }
    }
}