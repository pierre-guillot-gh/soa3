using BioscoopCasus.Behaviours;
using BioscoopCasus.Domain;

namespace BioscoopCasus {
    public static class Program {
        static void Main(string[] args) {
            Movie movie = new("Spongeboawb");
            MovieScreening movieScreening = new(movie, DateTime.Now, 10);
            MovieTicket movieTicket1 = new(movieScreening, 1, 2, false);
            MovieTicket movieTicket2 = new(movieScreening, 1, 3, true);

            Order order = new(1, true, new ExportPlainText());
            order.AddSeatReservation(movieTicket1);
            order.AddSeatReservation(movieTicket2);
            Console.WriteLine("-----------------------\r\nOrder Number: {0}", order.OrderNr);
            Console.WriteLine($"Order price: {order.CalculatePrice()}");
            Console.WriteLine("Is Student Order: {0}", order.IsStudentOrder);
            Console.WriteLine("Tickets:");
            order.Export();

            Order order2 = new(2, true, new ExportJson());
            order2.AddSeatReservation(movieTicket1);
            order2.AddSeatReservation(movieTicket2);
            Console.WriteLine("-----------------------\r\nOrder Number: {0}", order2.OrderNr);
            Console.WriteLine($"Order price: {order2.CalculatePrice()}");
            Console.WriteLine("Is Student Order: {0}", order2.IsStudentOrder);
            Console.WriteLine("Tickets:");
            order2.Export();
        }
    }
}
