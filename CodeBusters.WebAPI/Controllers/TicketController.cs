using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBusters.Models.Ticket;
using CodeBusters.Services.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBusters.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return Ok(tickets);
        }

        // GET api/Ticket/5
        [HttpGet("{ticketId:int}")]
        public async Task<IActionResult> GetTicketById([FromRoute] int ticketId)
        {
            var detail = await _ticketService.GetTicketByIdAsync(ticketId);

            // Similar to our service method, we're using a ternary to determine our return type
            // If the returned value (detail) is not null, return it with a 200 OK
            // Otherwise return a NotFound() 404 response
            return detail is not null
                ? Ok(detail)
                : NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] TicketCreate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _ticketService.CreateTicketAsync(request))
                return Ok("Ticket created successfully.");

            return BadRequest("Ticket could not be created.");
        }
    }
}
