using BioscoopCasus.Domain;

namespace BioscoopCasus {
    public static class Program {
        static void Main(string[] args) {
            Movie movie = new("Spongebob");
            MovieScreening movieScreening = new(movie, DateTime.Now, 10);
            MovieTicket movieTicket1 = new(movieScreening, 1, 2, false);
            MovieTicket movieTicket2 = new(movieScreening, 1, 3, true);
            MovieTicket movieTicket3 = new(movieScreening, 1, 4, false);
            MovieTicket movieTicket4 = new(movieScreening, 1, 5, true);

            Order order = new(1, true);
            order.AddSeatReservation(movieTicket1);
            order.AddSeatReservation(movieTicket2);
            order.AddSeatReservation(movieTicket3);
            order.AddSeatReservation(movieTicket4);

            Console.WriteLine($"Order price: {order.CalculatePrice()}");

            order.Export(TicketExportFormat.PLAINTEXT);
            order.Export(TicketExportFormat.JSON);
        }
    }
}
