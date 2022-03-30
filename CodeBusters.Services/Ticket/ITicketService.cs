using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBusters.Models.Ticket;

namespace CodeBusters.Services.Ticket
{
    public interface ITicketService
    {
        Task<bool> CreateTicketAsync(TicketCreate request);
        Task<IEnumerable<TicketListItem>> GetAllTicketsAsync();
        Task<TicketDetail> GetTicketByIdAsync(int noteId);
        Task<bool> UpdateTicketAsync(TicketUpdate request);
        Task<bool> DeleteTicketAsync(int ticketId);
    }
}