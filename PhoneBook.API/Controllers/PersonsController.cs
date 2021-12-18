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
        private readonly IContactInformationService _contactInformationService;

        public PersonsController(IPersonService personService, IContactInformationService contactInformationService)
        {
            _personService = personService;
            _contactInformationService = contactInformationService;
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

        [HttpDelete("{personId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnDto>> DeletePerson([FromRoute] Guid personId)
        {
            var result = await _personService.DeletePerson(personId);

            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result);
        }

        [HttpPost("{personId}/ContactInformations")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnDto>> AddContactInformation([FromRoute] Guid personId, [FromBody] ContactInformationDto contactInformationDto)
        {
            if (contactInformationDto == null)
                return BadRequest();

            var result = await _contactInformationService.AddContactInformation(personId, contactInformationDto);

            if (result.IsSuccess)
                return Created("", result);

            return NotFound(result.Message);
        }

        [HttpDelete("ContactInformations/{contactInformationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnDto>> DeleteContactInformation([FromRoute] Guid contactInformationId)
        {
            var result = await _contactInformationService.DeleteContactInformation(contactInformationId);

            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAllPersons()
        {
            var persons = await _personService.GetAllPersons();

            if (persons.Count() == 0)
                return NoContent();

            return Ok(persons);
        }

        [HttpGet("{personId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<PersonDto>> GetPerson(Guid personId)
        {
            var person = await _personService.GetPerson(personId);

            if(person == null)
                return NoContent();

            return Ok(person);
        }
    }
}
