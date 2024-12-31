using MicroServices.Service.Dtos;
using MicroServices.Service.Entities;

namespace MicroServices.Service
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        }
    }
}