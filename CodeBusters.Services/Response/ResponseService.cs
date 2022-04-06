using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CodeBusters.Data;
using CodeBusters.Data.Entities;
using CodeBusters.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CodeBusters.Services.Response
{
    public class ResponseService : IResponseService
    {
        private readonly ApplicationDbContext _context;
        private readonly int _userId;
        public ResponseService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            var userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var value = userClaims.FindFirst("Id")?.Value;
            var validId = int.TryParse(value, out _userId);
            if (!validId)
                throw new Exception("Attempted to build AssessmentService without User Id claim.");

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

        public async Task<IEnumerable<ResponseListItem>> GetAllResponsesAsync()
        {
            var responses = await _context.Responses
            .Select(entity => new ResponseListItem
                {
                    Id = entity.Id,
                    Text = entity.Text,
                    AssessmentId = entity.AssessmentId
                })
                .ToListAsync();
            return responses;
        }

        public async Task<IEnumerable<ResponseListItem>> GetResponsesByAssessmentIdAsync(int assessmentId)
        {
            var responses = await _context.Responses
                .Where(entity => entity.AssessmentId == assessmentId)
                .Select(entity => new ResponseListItem
                {
                    Id = entity.Id,
                    Text = entity.Text,
                    AssessmentId = entity.AssessmentId
                })
                .ToListAsync();

            return responses;
        }

        public async Task<bool> UpdateResponseAsync(ResponseEntity request)
        {
            var responseEntity = await _context.Responses.FindAsync(request.Id);

            responseEntity.Text = request.Text;

            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteResponseAsync(int id)
        {
            var responseEntity = await _context.Responses.FindAsync(id);
            _context.Responses.Remove(responseEntity);
            return await _context.SaveChangesAsync() == 1;
        }
    }
}