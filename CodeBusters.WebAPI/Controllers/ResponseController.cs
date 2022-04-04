using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBusters.Data.Entities;
using CodeBusters.Models.Responses;
using CodeBusters.Services.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBusters.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ResponseController : ControllerBase
    {

        private readonly IResponseService _service;
        public ResponseController(IResponseService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateResponse([FromBody] ResponseCreate request)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);

            if (await _service.CreateResponseAsync(request))
            return Ok($"Response successfully added to Assessment #{request.AssessmentId}.");

            return BadRequest($"Response could not be added.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResponses()
        {
            var responses = await _service.GetAllResponsesAsync();
            return Ok(responses);
        }

        [HttpGet("{assessmentId:int}")]
        public async Task<IActionResult> GetResponses([FromRoute] int assessmentId)
        {
            var responses = await _service.GetResponsesByAssessmentIdAsync(assessmentId);
            return Ok(responses);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateResponse([FromBody] ResponseEntity request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _service.UpdateResponseAsync(request)
                ? Ok("Response updated successfully.")
                : BadRequest ("Response could not be updated.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteResponse([FromRoute] int id)
        {
            return await _service.DeleteResponseAsync(id)
                ? Ok($"Response {id} was successfully deleted.")
                : BadRequest($"Response {id} could not be deleted. Please try again.");
        }
    }
}