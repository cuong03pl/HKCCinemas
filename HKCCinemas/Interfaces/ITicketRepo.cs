using HKCCinemas.DTO;
using HKCCinemas.Helper;
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
        List<TicketViewDTO> Search(QueryObject query);
        int Count();

        //Task<string> GeneratePDFAsync(TicketRequest request);
        //Task SendTicketEmailAsync(string pdfPath, TicketRequest request);


    }
}
