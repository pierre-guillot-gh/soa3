using BioscoopCasus.Domain;

namespace BioscoopCasus.Interfaces {
    public interface ICalculatePrice {
        public decimal CalculatePrice(decimal ticketPrice, int count, int totalTickets, MovieTicket ticket, bool isStudentOrder);
    }
}