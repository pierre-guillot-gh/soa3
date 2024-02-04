using BioscoopCasus.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BioscoopCasus.Domain
{
    public class Order
    {
        [JsonPropertyName("OrderNumber")] public int OrderNr { get; set; }
        [JsonPropertyName("IsStudentOrder")] public bool IsStudentOrder { get; set; }
        [JsonPropertyName("Tickets")] public List<MovieTicket> Tickets { get; set; }

        public Order(int orderNr, bool isStudentOrder)
        {
            OrderNr = orderNr;
            IsStudentOrder = isStudentOrder;
            Tickets = new List<MovieTicket>();
        }

        static MovieScreening movieScreening = new MovieScreening(new Movie(""), DateTime.Today, 10);
        static MovieTicket movieTicket = new MovieTicket(movieScreening, 1, 2, false);

        public int GetOrderNr()
        {
            return OrderNr;
        }

        public void AddSeatReservation(MovieTicket ticket)
        {
            Tickets.Add(ticket);
        }

        public double CalculatePrice()
        {
            double sum = 0;
            var tickets = Tickets;

            bool isWeekend = Tickets.Where(t => (int)t.MovieScreening.DateAndTime.DayOfWeek > 5).ToList().Count() >= 1;

            List<MovieTicket> filtered = Tickets.Where((t, i) => (i % 2 == 1 && !((int)t.MovieScreening.DateAndTime.DayOfWeek < 5)) || (i % 2 == 1 && !IsStudentOrder)).ToList();

            bool isGroupDiscount = (isWeekend && Tickets.Count >= 6);

            foreach (MovieTicket movieTicket in filtered)
            {
                if (movieTicket.IsPremium && IsStudentOrder)
                {
                    if (isGroupDiscount) sum += (movieTicket.MovieScreening.PricePerSeat + 3) * 0.9;
                    else sum += movieTicket.MovieScreening.PricePerSeat + 3;
                }
                else if (movieTicket.IsPremium && !IsStudentOrder)
                {
                    if (isGroupDiscount) sum += (movieTicket.MovieScreening.PricePerSeat + 2) * 0.9;
                    else sum += movieTicket.MovieScreening.PricePerSeat + 2;
                }
                else
                {
                    if (isGroupDiscount) sum += movieTicket.MovieScreening.PricePerSeat * 0.9;
                    else sum += movieTicket.MovieScreening.PricePerSeat;
                }
            }
            return sum;
        }

        public void Export(TicketExportFormat exportFormat)
        {
            if (exportFormat == TicketExportFormat.PLAINTEXT)
            {
                Console.WriteLine($"Order Number: {OrderNr}");
                Console.WriteLine($"Is Student Order: {IsStudentOrder}");
                Console.WriteLine("Tickets:");
                foreach (var ticket in Tickets)
                {
                    Console.WriteLine($"  {ticket}");
                }
                Console.WriteLine($"Total Price: {CalculatePrice()}");
                File.WriteAllText("path.txt", this.ToString());
            }
            else if (exportFormat == TicketExportFormat.JSON)
            {
                var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                Console.WriteLine(json);
                File.WriteAllText("path.json", json);
            }
        }
    }
}

