using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CodeBusters.Data;
using CodeBusters.Data.Entities;
using CodeBusters.Models.Assessment;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CodeBusters.Services.Assessment
{
    public class AssessmentService : IAssessmentService
    {
        private readonly int _userId;
        private readonly ApplicationDbContext _context;
        public AssessmentService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;

            var userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var value = userClaims.FindFirst("Id")?.Value;
            var validId = int.TryParse(value, out _userId);
            if (!validId)
                throw new Exception("Attempted to build AssessmentService without User Id claim.");
        }
        public async Task<bool> CreateAssessmentAsync(CreateAssessment model)
        {
            if (await CheckAssessmentByTicketAsync(model.ticketId) != null)
                return false;

            var entity = new AssessmentEntity
            {
                Comments = model.comments,
                TimeRequired = model.timeRequired,
                Cost = model.cost,
                Accepted = model.accepted,
                TicketId = model.ticketId
            };

            _context.Assessments.Add(entity);
            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<AssessmentDetail> GetAssessmentByTicketIdAsync(int TicketId)
        {
            var entity = await _context.Assessments.FindAsync(TicketId);
            if (entity is null)
                return null;

            var assessmentDetail = new AssessmentDetail
            {
                Id = entity.Id,
                Comments = entity.Comments,
                TimeRequired = entity.TimeRequired,
                Cost = entity.Cost,
                Accepted = entity.Accepted
            };

            return assessmentDetail;
        }

        public async Task<IEnumerable<AssessmentsListAll>> GetAllAssessmentsAsync()
        {
            var assessments = await _context.Assessments
            .Select(entity => new AssessmentsListAll
            {
                Id = entity.Id,
                Comments = entity.Comments,
                TimeRequired = entity.TimeRequired,
                Cost = entity.Cost,
                Accepted = entity.Accepted,
                TicketId = entity.TicketId
            }).ToListAsync();

            return assessments;
        }

        public async Task<bool> UpdateAssessmentAsync(UpdateAssessment request)
        {
            var assessmentEntity = await _context.Assessments.FindAsync(request.Id);

            assessmentEntity.Comments = request.Comments;
            assessmentEntity.TimeRequired = request.TimeRequired;
            assessmentEntity.Cost = request.Cost;
            assessmentEntity.Accepted = request.Accepted;

            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        private async Task<AssessmentEntity> CheckAssessmentByTicketAsync(int ticketId)
        {
            return await _context.Assessments.FirstOrDefaultAsync(Assessments => Assessments.TicketId == ticketId);
        }
    }
}