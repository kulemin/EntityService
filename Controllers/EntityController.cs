using EntityService.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntityService.Controllers
{
    [ApiController]
    [Route("[controller]")]

    
    public class EntityController : ControllerBase
    {
        static Dictionary<Guid, Entity> Entities = new Dictionary<Guid, Entity>();

        [HttpGet(Name = "GetEntity")]
        public Entity Get(string entityGuid)
        {
            return Entities[Guid.Parse(entityGuid)];
        }

        [HttpPost(Name = "SetEntity")]
        public IActionResult Post(Entity entity)
        {
            if (Entities.ContainsKey(entity.Id)) return Problem("Сущность с указанным Id уже существует.");
            entity.Id = entity.Id.GetType().Name == "Guid" ? entity.Id : new Guid();
            entity.OperationDate = entity.OperationDate.GetType().Name == "DateTime" ? entity.OperationDate : DateTime.Now;
            Entities.Add(entity.Id, entity);
            return Ok("Создана сущность с ключом " + entity.Id);
        }
    }
}
