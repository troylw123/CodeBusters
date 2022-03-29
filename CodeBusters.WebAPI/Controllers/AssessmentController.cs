using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBusters.Models.Assessment;
using CodeBusters.Services.Assessment;
using Microsoft.AspNetCore.Mvc;

namespace CodeBusters.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentService _assessmentService;
        public AssessmentController(IAssessmentService assessmentService)
        {
            _assessmentService = assessmentService;
        }

        [HttpPost("Create Assessment")]
        public async Task<IActionResult> CreateAssessment([FromBody] CreateAssessment model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createResult = await _assessmentService.CreateAssessmentAsync(model);
            if (createResult)
            {
                return Ok("Assessment was created.");
            }
            return BadRequest("Assessment couldn't be created.");
        }
    }
}