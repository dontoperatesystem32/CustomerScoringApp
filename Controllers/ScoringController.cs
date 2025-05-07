using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ScoringSystem_web_api.Dto;
using ScoringSystem_web_api.Interfaces;
using ScoringSystem_web_api.Models;
using ScoringSystem_web_api.Models.ConditionModels;

namespace ScoringSystem_web_api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ScoringController : Controller
    {
        private readonly IConditionRepository _conditionRepository;
        private readonly IMapper _mapper;
        public ScoringController(IConditionRepository conditionrepository, IMapper mapper)
        {
            _conditionRepository = conditionrepository;
            _mapper = mapper;

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult EvaluateCustomer([FromBody] CustomerDto customerDto)
        {
            if (customerDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var conditions = _conditionRepository.GetConditions();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!conditions.Any())
                return BadRequest(ModelState);

            //assign accounts to the customer


            var accounts = _mapper.Map<ICollection<Account>>(customerDto.Accounts);
            var customer = _mapper.Map<Customer>(customerDto);
            customer.Accounts = accounts;



            //define dictionary with scoring results
            var scoringResults = new Dictionary<string, bool>();

            bool conditionMet;
            bool scoringPassed = true;

            foreach (var condition in conditions)
            {
                conditionMet = condition.EvaluateCustomer(customer);
                scoringResults.Add(condition.ConditionType, conditionMet);
                if (!conditionMet)
                    scoringPassed = false;
            }

            var response = new
            {
                ScoringPassed = scoringPassed,
                ScoringResults = scoringResults
            };


            return Ok(response);
        }




        //[HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public IActionResult EvaluateCustomer([FromForm] CustomerDto customerDto, [FromForm] IEnumerable<AccountDto> accountsDto)
        //{
        //    if (customerDto == null)
        //        return BadRequest(ModelState);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var conditions = _conditionRepository.GetConditions();

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    if (!conditions.Any()) 
        //        return BadRequest(ModelState);

        //    //assign accounts to the customer
        //    var customer = _mapper.Map<Customer>(customerDto);
        //    var accounts = _mapper.Map<ICollection<Account>>(accountsDto);

        //    customer.Accounts = accounts;

        //    //define dictionary with scoring results
        //    var scoringResults = new Dictionary<string, bool>();

        //    bool conditionMet;
        //    bool scoringPassed = true;

        //    foreach (var condition in conditions)
        //    {
        //        conditionMet = condition.EvaluateCustomer(customer);
        //        scoringResults.Add(condition.ConditionType, conditionMet);
        //        if (!conditionMet)
        //            scoringPassed = false;
        //    }

        //    var response = new
        //    {
        //        ScoringPassed = scoringPassed,
        //        ScoringResults = scoringResults
        //    };


        //    return Ok(response);
        //}

        //[HttpGet]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<IConditionRepository>))]
        //public IActionResult GetConditions()
        //{
        //    var conditions = _conditionRepository.GetConditions();

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    return Ok(conditions);
        //}

        //[HttpPut("{conditionId}")]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(500)]
        //public IActionResult UpdateCondition([FromRoute] int conditionId, [FromBody] BaseConditionDto conditionUpdate)
        //{
        //    if (conditionUpdate == null)
        //        return BadRequest(ModelState);
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    if (!_conditionRepository.ConditionExists(conditionId))
        //        return NotFound();

        //    var conditionMap = _mapper.Map<BaseCondition>(conditionUpdate);

        //    if (!_conditionRepository.UpdateCondition(conditionMap))
        //    {
        //        ModelState.AddModelError("", "Something went wrong while updating");
        //        return StatusCode(500, ModelState);
        //    }
        //    return NoContent();
        //}



    }
}
