using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBusters.Data;
using CodeBusters.Data.Entities;
using CodeBusters.Models.Assessment;
using Microsoft.EntityFrameworkCore;

namespace CodeBusters.Services.Assessment
{
    public class AssessmentService : IAssessmentService
    {
        private readonly ApplicationDbContext _context;
        public AssessmentService(ApplicationDbContext context)
        {
            _context = context;
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

        private async Task<AssessmentEntity> CheckAssessmentByTicketAsync(int ticketId)
        {
            return await _context.Assessments.FirstOrDefaultAsync(Assessments => Assessments.TicketId == ticketId);
        }
    }
}