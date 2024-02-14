using BioscoopCasus.Behaviours.CalculatePrice;
using BioscoopCasus.Extensions;
using BioscoopCasus.Interfaces;
using System.Text.Json.Serialization;

namespace BioscoopCasus.Domain {
    public class Order {
        [JsonPropertyName("OrderNumber")] public int OrderNr { get; set; }
        [JsonPropertyName("IsStudentOrder")] public bool IsStudentOrder { get; set; }
        [JsonPropertyName("Tickets")] public List<MovieTicket> Tickets { get; set; }

        private readonly IExport _export;

        public Order(int orderNr, bool isStudentOrder, IExport export) {
            OrderNr = orderNr;
            IsStudentOrder = isStudentOrder;
            Tickets = new List<MovieTicket>();
            _export = export;
        }

        public void AddSeatReservation(MovieTicket ticket) {
            Tickets.Add(ticket);
        }

        public decimal CalculatePrice() {
            decimal totalPrice = 0;
            int totalTickets = Tickets.Count;

            for (int i = 0; i < totalTickets; i++) {
                MovieTicket ticket = Tickets[i];
                decimal ticketPrice = ticket.GetPrice();

                ticketPrice = new CalculatePremium().CalculatePrice(ticketPrice, i, totalTickets, ticket, IsStudentOrder);
                ticketPrice = new CalculateFree().CalculatePrice(ticketPrice, i, totalTickets, ticket, IsStudentOrder);
                ticketPrice = new CalculateDiscount().CalculatePrice(ticketPrice, i, totalTickets, ticket, IsStudentOrder);

                totalPrice += ticketPrice;
            }

            return totalPrice;
        }

        public void Export() {
            _export.Export(this);
        }
    }
}
