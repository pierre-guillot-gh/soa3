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

        static MovieScreening movieScreening = new MovieScreening(new Movie(""), DateTime.Today, 10);
        static MovieTicket movieTicket = new MovieTicket(movieScreening, 1, 2, false);

        public int GetOrderNr() {
            return OrderNr;
        }

        public void AddSeatReservation(MovieTicket ticket) {
            Tickets.Add(ticket);
        }

        public double CalculatePrice() {
            double sum = 0;
            var tickets = Tickets;

            for (int i = 0; i < Tickets.Count(); i++) {
                var ticket = tickets[i];
                double ticketPrice = ticket.GetPrice();

                //Check if its a non-student order and not weekend
                if (!IsStudentOrder && i % 2 == 1 && !movieScreening.DateAndTime.IsWeekend()) {
                    sum += 0;
                }
                //Check if its student order
                else if (IsStudentOrder && i % 2 == 1) {
                    sum += 0;
                }
                //Check if there are more than 6 tickets and if its a non-student
                else if (!IsStudentOrder && tickets.Count() >= 6) {
                    sum *= 0.9;
                }
                //Check for student-order & premium ticket
                else if (IsStudentOrder && ticket.isPremiumTicket()) {
                    sum += (ticketPrice + 2.00);
                }
                //Check if non-student order & premium ticket
                else if (!IsStudentOrder && ticket.isPremiumTicket()) {
                    sum += (ticketPrice + 3.00);
                }
                //Check if count is or exceeds 6, non-student order & premium ticket, add extra discount  
                else if (!IsStudentOrder && ticket.isPremiumTicket() && tickets.Count() >= 6) {
                    sum += ((ticketPrice + 3.00) * 0.9);
                }
                //Check if count is or exceeds 6, non-student order & premium ticket, add extra discount  
                else if (IsStudentOrder && ticket.isPremiumTicket() && tickets.Count() >= 6) {
                    sum += ((ticketPrice + 2.00) * 0.9);
                }

            }

            return sum;

        }

        public void Export(TicketExportFormat exportFormat) {
            if (exportFormat == TicketExportFormat.PLAINTEXT) {
                Console.WriteLine($"Order Number: {OrderNr}");
                Console.WriteLine($"Is Student Order: {IsStudentOrder}");
                Console.WriteLine("Tickets:");
                foreach (var ticket in Tickets) {
                    Console.WriteLine($"  {ticket}");
                }
                Console.WriteLine($"Total Price: {CalculatePrice()}");
                File.WriteAllText("path.txt", this.ToString());
            } else if (exportFormat == TicketExportFormat.JSON) {
                var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                Console.WriteLine(json);
                File.WriteAllText("/path.json", json);
            }
        }
    }
}

