using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CodeBusters.Data;
using CodeBusters.Data.Entities;
using CodeBusters.Models.Review;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CodeBusters.Services.Review
{
    public class ReviewService : IReviewService
    {
        private readonly int _userId;
        private readonly ApplicationDbContext _context;
        public ReviewService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext Context)
        {
            var userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var value = userClaims.FindFirst("Id")?.Value;
            var validId = int.TryParse(value, out _userId);
            if (!validId)
                throw new Exception("Attempted to build Review service without User Id claim.");

            _context = Context;
        }

        public async Task<bool> CreateReviewAsync(ReviewCreate request)
        {
            var reviewEntity = new ReviewEntity
            {
                Rating = request.Rating,
                Comments = request.Comments,
                TicketId = request.TicketId
            };

            _context.Reviews.Add(reviewEntity);

            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<ReviewListItem>> GetAllReviewsAsync()
        {
            var reviews = await _context.Reviews
            // .Where(entity => entity.Id == _userId)
            .Select(entity => new ReviewListItem
            {
                Id = entity.Id,
                Rating = entity.Rating,
                Comments = entity.Comments,
                TicketId = entity.TicketId
            }).ToListAsync();
            return reviews;
        }

        public async Task<ReviewDetail> GetReviewByTicketIdAsync(int ticketId)
        {
            var reviewEntity = await _context.Reviews
            .FirstOrDefaultAsync(e =>
             e.Id == ticketId);

            return reviewEntity is null ? null : new ReviewDetail
            {
                Id = reviewEntity.Id,
                Rating = reviewEntity.Rating,
                Comments = reviewEntity.Comments,
                TicketId = reviewEntity.TicketId,
            };
        }


    }
}