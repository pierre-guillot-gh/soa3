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
            int ticketCount = Tickets.Count;

            for (int i = 0; i < Tickets.Count; i++) {
                MovieTicket ticket = Tickets[i];
                decimal ticketPrice = ticket.GetPrice();
                bool isPremium = ticket.IsPremium;
                bool isWeekend = ticket.MovieScreening.DateAndTime.IsWeekend();

                if (isPremium) ticketPrice += IsStudentOrder ? 2 : 3; // Premium ticket price adjustment

                if ((i + 1) % 2 == 0 && (!IsStudentOrder || !isWeekend)) {
                    ticketPrice = 0; // Skip every 2nd ticket or apply discount for non-student orders on weekdays
                } else if (!IsStudentOrder && ticketCount >= 6) {
                    ticketPrice *= 0.9M; // Apply 10% discount for groups on weekends
                }

                totalPrice += ticketPrice;
            }

            return totalPrice;
        }

        public void Export() {
            _export.Export(this);
        }
    }
}
