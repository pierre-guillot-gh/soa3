using BioscoopCasus.Domain;

namespace BioscoopCasus.Interfaces {
    public interface ICalculatePrice {
       public decimal CalculatePrice(MovieTicket ticket, bool IsStudentOrder);
    }
}