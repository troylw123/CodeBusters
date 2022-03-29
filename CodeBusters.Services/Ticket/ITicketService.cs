using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBusters.Models.Ticket;

namespace CodeBusters.Services.Ticket
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketListItem>> GetAllTicketsAsync();
    }
}