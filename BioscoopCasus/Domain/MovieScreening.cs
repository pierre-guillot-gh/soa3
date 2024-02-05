using Microsoft.VisualBasic;

namespace BioscoopCasus.Domain {
    public class MovieScreening {

        public DateTime DateAndTime { get; set; }
        public decimal PricePerSeat { get; set; }
        public Movie Movie { get; set; }

        public MovieScreening(Movie movie, DateTime dateAndTime, decimal pricePerSeat) {
            this.Movie = movie;
            this.DateAndTime = dateAndTime;
            this.PricePerSeat = pricePerSeat;
        }

        public override string ToString() {
            return $"Movie title: {DateAndTime}\nPrice per seat: {PricePerSeat}";
        }
    }
}

