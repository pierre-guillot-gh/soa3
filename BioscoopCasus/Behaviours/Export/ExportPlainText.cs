using BioscoopCasus.Domain;
using BioscoopCasus.Interfaces;

namespace BioscoopCasus.Behaviours.Export {
    public class ExportPlainText : IExport {
        public void Export(Order order) {
            using (StreamWriter writer = new StreamWriter("order.txt")) {
                foreach (var ticket in order.Tickets) {
                    Console.WriteLine(ticket.ToString());
                    writer.WriteLine(ticket.ToString());
                }
                writer.WriteLine("Total Price: {0}", order.CalculatePrice());
            }
        }
    }
}
