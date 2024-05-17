using ApplicationFormPractice.Application.Contract;
using ApplicationFormPractice.Domain.DTOs.Request;
using ApplicationFormPractice.Domain.DTOs.Response;
using ApplicationFormPractice.SharedKernel.GenericModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace ApplicationFormPractice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ApplicationFormController : ControllerBase
    {
        private readonly IApplicationFormService _applicationFormService;

        public ApplicationFormController(IApplicationFormService applicationFormService)
        {
            _applicationFormService = applicationFormService;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(ResponseWrapper<List<string>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult GetQuestionTypes()
        {
            var response = _applicationFormService.GetQuestionTypes();

            if (!response.IsSuccessful)
            {
                return BadRequest("Unable to retrieve question types at the moment");
            }

            return Ok(response);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AddNewQuestion(AddQuestionRequest request)
        {
            var response = await _applicationFormService.AddNewQuestion(request);

            if (!response.IsSuccessful)
            {
                return BadRequest("Unable to submit question at the moment");
            }

            return Ok(response);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(ResponseWrapper<List<CustomQuestionDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetCustomQuestions()
        {
            var response = await _applicationFormService.GetCustomQuestions();

            if (!response.IsSuccessful)
            {
                return BadRequest("Unable to submit question at the moment");
            }

            return Ok(response);
        }

        [HttpPut("[action]")]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> EditCustomQuestions(EditCustomQuestionRequest request)
        {
            var response = await _applicationFormService.EditCustomQuestion(request);

            if (!response.IsSuccessful)
            {
                return BadRequest("Unable to edit question at the moment");
            }

            return Ok(response);
        }

        [HttpDelete("[action]")]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteCustomQuestions(string questionId)
        {
            var response = await _applicationFormService.DeleteCustomQuestion(questionId);

            if (!response.IsSuccessful)
            {
                return BadRequest("Unable to delete question at the moment");
            }

            return Ok(response);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> SubmitApplication(SubmitApplicationRequest request)
        {
            var response = await _applicationFormService.SubmitApplication(request);

            if (!response.IsSuccessful)
            {
                return BadRequest("Unable to submit application at the moment");
            }

            return Ok(response);
        }
    }
}
