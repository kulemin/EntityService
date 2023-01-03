using EntityService.Models;
using EntityService.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Text.RegularExpressions;

namespace EntityService.Controllers
{
    [ApiController]
    [Route("[controller]")]

    
    public class EntityController : ControllerBase
    {
        private static EntitiesService _service = new EntitiesService();

        [HttpGet(Name = "GetEntity")]
        public IActionResult Get(string entityGuid)
        {
            var isGiud = Guid.TryParse(entityGuid, out Guid result);
            
            return !isGiud
                ? BadRequest("Входная строка имела неверный формат или не является Guid.")
                : _service.CheckById(result)
                ? Ok(JsonConvert.SerializeObject(_service.GetById(result))) 
                : BadRequest("Сущность с искомым Guid не найдена.");
        }

        [HttpPost(Name = "InsertEntity")]
        public IActionResult Post(object entity)
        {

            dynamic values = JsonConvert.DeserializeObject(entity.ToString());
            if (!values.ContainsKey("id") || !values.ContainsKey("operationDate") || !values.ContainsKey("amount")){
                var result = !values.ContainsKey("id") ? "Отсутствует параметр 'id' в запросе\r\n" : "";
                if (!values.ContainsKey("operationDate")) result += "Отсутствует параметр 'operationDate' в запросе\r\n";
                if (!values.ContainsKey("amount")) result += "Отсутствует параметр 'amount' в запрос\r\n";
                return BadRequest(result);
            }
            var isGiud = Guid.TryParse(values["id"].ToString(), out Guid guid);
            var isDateTime = DateTime.TryParse(values["operationDate"].ToString(), out DateTime date);
            var isDecimal = Decimal.TryParse(values["amount"].ToString(), out decimal amount);
            if(!isGiud || !isDateTime || !isDecimal)
            {
                var result = !isGiud ? "Поле 'id' имело неверный формат\r\n" : "";
                if (!isDateTime) result += "Поле 'operationDate' имело неверный формат\r\n";
                if (!isDecimal) result += "Поле 'amount' имело неверный формат\r\n";
                return BadRequest(result);
            }
            if (_service.CheckById(guid))
            {
                _service.UpdateById(guid, amount);
                return BadRequest("Сущность с id " + guid + " изменена");
            }
            _service.Insert(new Entity() { Id = guid, OperationDate = date, Amount = amount });
            return Ok("Создана сущность с ключом " + guid);
        }
    }
}
