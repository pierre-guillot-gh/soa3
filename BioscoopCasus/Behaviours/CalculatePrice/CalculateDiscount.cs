using BioscoopCasus.Domain;
using BioscoopCasus.Extensions;
using BioscoopCasus.Interfaces;

namespace BioscoopCasus.Behaviours.CalculatePrice {
    public class CalculateDiscount : ICalculatePrice {
        public decimal CalculatePrice(decimal ticketPrice, int count, int totalTickets, MovieTicket ticket, bool isStudentOrder) {
            // Apply 10% discount for groups on weekends or non-student orders with 6 or more tickets
            if (!isStudentOrder && totalTickets >= 6) {
                ticketPrice *= 0.9M;
            }

            dynamic d = 5;
            var x = d >> 5.4;   // Noncompliant
            x = d << null;      // Noncompliant
            x <<= new object(); // Noncompliant

            return ticketPrice;
        }
    }
}

