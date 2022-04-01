using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBusters.Models.Review;

namespace CodeBusters.Services.Review
{
    public interface IReviewService
    {
        Task<bool> CreateReviewAsync(ReviewCreate request);
        Task<IEnumerable<ReviewListItem>> GetAllReviewsAsync();
        Task<ReviewDetail> GetReviewByTicketIdAsync(int ticketId);
        Task<bool> UpdateReviewAsync(ReviewUpdate request);
        Task<bool> DeleteReviewAsync(int TicketId);
    }
}