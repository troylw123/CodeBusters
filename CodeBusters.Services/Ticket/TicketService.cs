using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CodeBusters.Data;
using CodeBusters.Data.Entities;
using CodeBusters.Models.Ticket;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CodeBusters.Services.Ticket
{
    public class TicketService : ITicketService
    {
        private readonly int _userId;
        private readonly ApplicationDbContext _dbContext;
        public async Task<IEnumerable<TicketListItem>> GetAllTicketsAsync()
        {
            var tickets = await _dbContext.Tickets
                .Where(entity => entity.UserID == _userId)
                .Select(entity => new TicketListItem
                {
                    Id = entity.Id,
                    Title = entity.Title,
                })
                .ToListAsync();

            return tickets;
        }

        public async Task<TicketDetail> GetTicketByIdAsync(int ticketId)
        {
            // Find the first ticket that has the given Id and an OwnerId that matches the requesting userId
            var ticketEntity = await _dbContext.Tickets
                .FirstOrDefaultAsync(e =>
                    e.Id == ticketId 
                );
                
            // If ticketEntity is null then return null, otherwise initialize and return a new TicketDetail
            return ticketEntity is null ? null : new TicketDetail
            {
                Id = ticketEntity.Id,
                Title = ticketEntity.Title,
                Description = ticketEntity.Description,
                Category = ticketEntity.CategoryId,
                UserID = ticketEntity.UserID,
                isArchived = ticketEntity.isArchived
            };
        }

        public async Task<bool> UpdateTicketAsync(TicketUpdate request)
        {
            var ticketEntity = await _dbContext.Tickets.FindAsync(request.Id);


            // By using the null conditional operator we can check if it's null at the same time we check the OwnerId
            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-(Links to an external site.)

            if (ticketEntity?.UserID != _userId)
                return false;


            ticketEntity.Title = request.Title;
            ticketEntity.Description = request.Description;

            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteTicketAsync(int ticketId)
        {
            // Find the ticket by the given Id
            var ticketEntity = await _dbContext.Tickets.FindAsync(ticketId);

            // Validate the ticket exists and is owned by the user
            if (ticketEntity?.UserID != _userId)
                return false;

            // Remove the ticket from the DbContext and assert that the one change was saved
            _dbContext.Tickets.Remove(ticketEntity);
            return await _dbContext.SaveChangesAsync() == 1;
        }

        public TicketService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            
            var userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var value = userClaims.FindFirst("Id")?.Value;
            var validId = int.TryParse(value, out _userId);
            if (!validId)
                throw new Exception("Attempted to build TicketService without User Id claim.");

            _dbContext = dbContext;
        }
        public async Task<bool> CreateTicketAsync(TicketCreate request)
        {
            var ticketEntity = new TicketEntity
            {
                Title = request.Title,
                Description = request.Description,
                CategoryId = request.CategoryId,
                UserID = request.UserID
            };
            _dbContext.Tickets.Add(ticketEntity);

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> ChangeArchiveStatusAsync(int ticketId)
        {
            var ticketEntity = await _dbContext.Tickets.FindAsync(ticketId);
            if (ticketEntity is null)
            return false;

            if (ticketEntity.isArchived)
            {
                ticketEntity.isArchived = false;
            }

            else if (!ticketEntity.isArchived)
            {
                ticketEntity.isArchived = true;
            }

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;

        }
    }
}