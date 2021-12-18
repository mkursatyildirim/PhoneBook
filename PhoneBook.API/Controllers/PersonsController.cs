using Microsoft.AspNetCore.Mvc;
using PhoneBook.API.Dto;
using PhoneBook.API.Services;

namespace PhoneBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReturnDto>> AddPerson([FromBody] PersonDto personDto)
        {
            if (personDto == null)
                return BadRequest();

            var result = await _personService.AddPerson(personDto);

            if (result.IsSuccess)
                return Created("", result);

            return BadRequest(result.Message);
        }
    }
}
