using BioscoopCasus.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BioscoopCasus.Domain {
    public class Order {
        [JsonPropertyName("OrderNumber")] public int OrderNr { get; set; }
        [JsonPropertyName("IsStudentOrder")] public bool IsStudentOrder { get; set; }
        [JsonPropertyName("Tickets")] public List<MovieTicket> Tickets { get; set; }

        public Order(int orderNr, bool isStudentOrder) {
            OrderNr = orderNr;
            IsStudentOrder = isStudentOrder;
            Tickets = new List<MovieTicket>();
        }

        public void AddSeatReservation(MovieTicket ticket) {
            Tickets.Add(ticket);
        }

        public decimal CalculatePrice() {
            decimal totalPrice = 0;

            for (int i = 0; i < Tickets.Count; i++) {
                MovieTicket ticket = Tickets[i];
                decimal ticketPrice = ticket.GetPrice();
                bool isPremium = ticket.IsPremium;
                bool isWeekend = ticket.MovieScreening.DateAndTime.IsWeekend();

                // Apply discount for every 2nd ticket for non-student orders on weekdays or all tickets for student orders
                if ((i + 1) % 2 == 0 && (!IsStudentOrder || !isWeekend)) {
                    ticketPrice = 0; // Skip every 2nd ticket
                } else {
                    // Apply group discount for non-student orders with 6 or more tickets on weekends
                    if (!IsStudentOrder && Tickets.Count >= 6 && isWeekend) {
                        ticketPrice *= 0.9M; // Apply 10% discount for groups on weekends
                    }

                    // Adjust premium ticket price based on student status
                    if (isPremium) {
                        ticketPrice += IsStudentOrder ? 2 : 3; // Premium ticket price adjustment
                    }
                }

                totalPrice += ticketPrice;
            }

            return totalPrice;
        }

        public void Export(TicketExportFormat exportFormat) {
            Console.WriteLine("-----------------------\r\nOrder Number: {0}", OrderNr);
            Console.WriteLine("Is Student Order: {0}", IsStudentOrder);
            Console.WriteLine("Tickets:");

            switch (exportFormat) {
                case TicketExportFormat.PLAINTEXT:
                    ExportToPlainText();
                    break;
                case TicketExportFormat.JSON:
                    ExportToJson();
                    break;
                default:
                    throw new ArgumentException("Invalid export format");
            }
        }

        private void ExportToPlainText() {
            using (StreamWriter writer = new StreamWriter("order.txt")) {
                foreach (var ticket in Tickets) {
                    Console.WriteLine(ticket.ToString());
                    writer.WriteLine(ticket.ToString());
                }
                writer.WriteLine("Total Price: {0}", CalculatePrice());
            }
        }

        private void ExportToJson() {
            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(json);
            File.WriteAllText("order.json", json);
        }

    }
}
