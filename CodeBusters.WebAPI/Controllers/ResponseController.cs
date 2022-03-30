using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBusters.Models.Responses;
using CodeBusters.Services.Response;
using Microsoft.AspNetCore.Mvc;

namespace CodeBusters.WebAPI.Controllers
{
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
    }
}