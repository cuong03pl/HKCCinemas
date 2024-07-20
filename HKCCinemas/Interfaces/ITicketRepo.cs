using HKCCinemas.DTO;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface ITicketRepo
    {
        Ticket GetTicketById(int ticketId);
        List<TicketViewDTO> GetTickets();
        TicketDTO GetTicketByScheduleId(int scheduleId);
        bool CreateTicket(TicketDTO ticket);
        bool UpdateTicket(int ticketId, TicketDTO ticket);
        bool DeleteTicket(int ticketId);
    }
}
