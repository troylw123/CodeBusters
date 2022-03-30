using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBusters.Data;
using CodeBusters.Data.Entities;
using CodeBusters.Models.Responses;

namespace CodeBusters.Services.Response
{
    public class ResponseService : IResponseService
    {
        private readonly ApplicationDbContext _context;
        public ResponseService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateResponseAsync(ResponseCreate request)
        {
            var response = new ResponseEntity
            {
                Text = request.Text,
                AssessmentId = request.AssessmentId
            };
            _context.Responses.Add(response);
            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
            
        }
    }
}