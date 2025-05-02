using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ScoringSystem_web_api.Interfaces;

namespace ScoringSystem_web_api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ConditionsController : Controller
    {
        private readonly IConditionRepository _conditionRepository;
        public ConditionsController(IConditionRepository conditionrepository)
        {
            _conditionRepository = conditionrepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<IConditionRepository>))]
        public IActionResult GetConditions()
        {
            var conditions = _conditionRepository.GetConditions();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(conditions);
        }
    }
}
