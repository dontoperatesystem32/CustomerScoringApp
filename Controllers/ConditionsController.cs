using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ScoringSystem_web_api.Dto;
using ScoringSystem_web_api.Interfaces;
using ScoringSystem_web_api.Models.ConditionModels;

namespace ScoringSystem_web_api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ConditionsController : Controller
    {
        private readonly IConditionRepository _conditionRepository;
        private readonly IMapper _mapper;
        public ConditionsController(IConditionRepository conditionrepository, IMapper mapper)
        {
            _conditionRepository = conditionrepository;
            _mapper = mapper;

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

        [HttpPut("{conditionId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateCondition([FromRoute] int conditionId, [FromBody] BaseConditionDto conditionUpdate)
        {
            if (conditionUpdate == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_conditionRepository.ConditionExists(conditionId))
                return NotFound();

            var conditionMap = _mapper.Map<BaseCondition>(conditionUpdate);

            if (!_conditionRepository.UpdateCondition(conditionMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        //[HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public IActionResult CreateCondition([FromBody] BaseConditionDto conditionCreate)
        //{
        //    if (conditionCreate == null)
        //        return BadRequest(ModelState);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (conditionCreate.ConditionType == null)
        //        return BadRequest(ModelState);


        //    var conditionMap = _mapper.Map<BaseCondition>(conditionCreate);
        //    //Type targetType = Type.GetType(conditionCreate.ConditionType);
        //    //var conditionMap = _mapper.Map(conditionCreate, conditionCreate.GetType(), targetType);


        //    if (!_conditionRepository.CreateCondition(conditionMap))
        //    {
        //        ModelState.AddModelError("", "Something went wrong while savin");
        //        return StatusCode(500, ModelState);
        //    }

        //    return Ok("Successfully created");
        //}
    }
}
