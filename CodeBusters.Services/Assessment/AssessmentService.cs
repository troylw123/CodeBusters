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
            if (await GetAssessmentByTicketAsync(model.ticketId) != null)
                return false;

            var entity = new AssessmentEntity
            {
                comments = model.comments,
                timeRequired = model.timeRequired,
                cost = model.cost,
                accepted = model.accepted,
                ticketId = model.ticketId
            };

            _context.Assessments.Add(entity);
            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }


        private async Task<AssessmentEntity> GetAssessmentByTicketAsync(int ticketId)
        {
            return await _context.Assessments.FirstOrDefaultAsync(Assessments => Assessments.ticketId == ticketId);
        }
    }
}