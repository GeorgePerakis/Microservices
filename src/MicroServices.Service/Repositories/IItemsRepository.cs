using MicroServices.Service.Entities;

namespace MicroServices.Service.Repositories
{
    public interface IItemsRepository
    {
        Task CreateAsync(Item entity);
        Task<IReadOnlyCollection<Item>> GetAllAsync();
        Task<Item> GetAsync(Guid id);
        Task UpdateAsync(Item entity);
        Task RemoveAsync(Guid id);

    }
}