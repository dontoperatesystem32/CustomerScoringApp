using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScoringSystem_web_api.Dto;
using ScoringSystem_web_api.Models.CustomerModels;
using ScoringSystem_web_api.Services.Abstraction;

namespace ScoringSystem_web_api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ScoringController : Controller
    {
        private readonly IScoringService _scoringService;
        private readonly IMapper _mapper;
        public ScoringController(
            IScoringService scoringService,
            IMapper mapper
        ){
            _scoringService = scoringService;
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

            var accounts = _mapper.Map<ICollection<Account>>(customerDto.Accounts);
            var customer = _mapper.Map<Customer>(customerDto);
            customer.Accounts = accounts;

            var response = _scoringService.EvaluateCustomer(customer);

            if (!response.Success)
            {
                ModelState.AddModelError("ScoringService", response.ErrorMessage ?? "An error occurred");
                return BadRequest(ModelState);
            }

            return Ok(response);
        }

    }
}
