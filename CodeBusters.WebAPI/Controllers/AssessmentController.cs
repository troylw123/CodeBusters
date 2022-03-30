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

        [HttpGet("{ticketId:int}")]
        public async Task<IActionResult> GetByTicketId([FromRoute] int TicketId)
        {
            var assessmentDetail = await _assessmentService.GetAssessmentByTicketIdAsync(TicketId);

            if (assessmentDetail is null)
            {
                return NotFound();
            }
            return Ok(assessmentDetail);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssessment()
        {
            var assessments = await _assessmentService.GetAllAssessmentsAsync();
            return Ok(assessments);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAssessment([FromBody] UpdateAssessment request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _assessmentService.UpdateAssessmentAsync(request)
            ? Ok("Assessment was successfully updated.")
            : BadRequest("Assessment could not be updated.");
        }
    }
}