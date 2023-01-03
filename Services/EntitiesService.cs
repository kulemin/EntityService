using EntityService.Models;

namespace EntityService.Services
{
    public class EntitiesService : IEntitiesService
    {
        private readonly Dictionary<Guid, Entity> Entities = new Dictionary<Guid, Entity>();
        public Entity GetById(Guid guid)
        {
            return Entities[guid];
        }

        public void Insert(Entity entity)
        {
            Entities[entity.Id] = entity;
        }

        public bool CheckById(Guid guid)
        {
            return Entities.ContainsKey(guid);
        }

        public void UpdateById(Guid guid, decimal amount)
        {
            Entities[guid].Amount = amount;
        }
    }
}
