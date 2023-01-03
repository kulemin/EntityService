using EntityService.Models;

namespace EntityService.Services
{
    public interface IEntitiesService
    {
        public Entity GetById(Guid guid);
        public void Insert(Entity entity);
        public bool CheckById(Guid guid);
        public void UpdateById(Guid guid, decimal amount);
    }
}
