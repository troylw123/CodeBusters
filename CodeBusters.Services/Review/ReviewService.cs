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
        public ReviewService(ApplicationDbContext Context, IHttpContextAccessor httpContextAccessor)
        
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
            if (await CheckReviewByTicketIdAsync(request.TicketId) != null)
                throw new Exception("Only 1 review allowed per ticket.");

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

        public async Task<bool> UpdateReviewAsync(ReviewUpdate request)
        {
            var reviewEntity = await _context.Reviews.FindAsync(request.Id);

            // if (reviewEntity?.OwnerId != _userId)
            // return false;

            reviewEntity.Rating = request.Rating;
            reviewEntity.Comments = request.Comments;

            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteReviewAsync(int Id)
        {
            var reviewEntity = await _context.Reviews.FindAsync(Id);

            // if (reviewEntity?.Owner != _userId)
            // return false;

            _context.Reviews.Remove(reviewEntity);
            return await _context.SaveChangesAsync() == 1;
        }
        private async Task<ReviewEntity> CheckReviewByTicketIdAsync(int ticketId)
        {
            return await _context.Reviews.FirstOrDefaultAsync(Review => Review.TicketId == ticketId);
        }

    }
}