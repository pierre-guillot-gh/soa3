using BioscoopCasus.Domain;
using BioscoopCasus.Extensions;
using BioscoopCasus.Interfaces;

namespace BioscoopCasus.Behaviours.CalculatePrice {
    public class CalculateFree : ICalculatePrice {
        public decimal CalculatePrice(decimal ticketPrice, int count, int totalTickets, MovieTicket ticket, bool isStudentOrder) {
            // Apply free ticket for every second ticket (count + 1) and non-student orders or non-weekend screenings
            if ((count + 1) % 2 == 0 && (!isStudentOrder || !ticket.MovieScreening.DateAndTime.IsWeekend())) {
                ticketPrice = 0;
            }

            return ticketPrice;
        }
    }
}
