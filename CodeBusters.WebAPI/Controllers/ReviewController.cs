using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBusters.Models.Review;
using CodeBusters.Services.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBusters.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateReview([FromBody] ReviewCreate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _reviewService.CreateReviewAsync(request))
                return Ok("Review was created successfully.");

            return BadRequest("Review could not be created.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var review = await _reviewService.GetAllReviewsAsync();
            return Ok(review);
        }

        [HttpGet("{ticketId:int}")]
        public async Task<IActionResult> GetReviewByTicketId([FromRoute] int ticketId)
        {
            var detail = await _reviewService.GetReviewByTicketIdAsync(ticketId);

            return detail is not null ? Ok(detail) : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReviewAsync([FromBody] ReviewUpdate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _reviewService.UpdateReviewAsync(request)
            ? Ok("Review updated successfully.")
            : BadRequest("Review could not be updated.");
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteReview([FromRoute] int Id)
        {
            return await _reviewService.DeleteReviewAsync(Id)
            ? Ok($"Review number {Id} was deleted successfully.")
            : BadRequest($"Review number {Id} could not be deleted");
        }
    }
}