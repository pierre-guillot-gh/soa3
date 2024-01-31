using BioscoopCasus.Extensions;

namespace BioscoopCasus.Domain {
    public class Order {
        public int OrderNr { get; set; }
        public bool IsStudentOrder { get; set; }
        public List<MovieTicket> Tickets { get; set; }

        public Order(int orderNr, bool isStudentOrder) {
            OrderNr = orderNr;
            IsStudentOrder = isStudentOrder;
            Tickets = new List<MovieTicket>();
        }

        public void AddSeatReservation(MovieTicket ticket) {
            Tickets.Add(ticket);
        }

        public double CalculatePrice() {

        }

        public void Export(TicketExportFormat exportFormat) {

        }
    }
}

