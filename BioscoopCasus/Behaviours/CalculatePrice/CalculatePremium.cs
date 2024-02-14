using BioscoopCasus.Domain;
using BioscoopCasus.Interfaces;

namespace BioscoopCasus.Behaviours.CalculatePrice {
    public class CalculatePremium : ICalculatePrice {
        private const decimal PremiumStudentSurcharge = 2M;
        private const decimal PremiumNonStudentSurcharge = 3M;

        public decimal CalculatePrice(MovieTicket ticket, bool isStudentOrder) {
            decimal ticketPrice = ticket.GetPrice();

            if (ticket.IsPremium) {
                decimal premiumSurcharge = isStudentOrder ? PremiumStudentSurcharge : PremiumNonStudentSurcharge;
                ticketPrice += premiumSurcharge;
            }

            return ticketPrice;
        }
    }
}
