namespace BioscoopCasus.Domain {
    public class MovieScreening {

        public DateTime DateAndTime { get; set; }
        public double PricePerSeat { get; set; }
        public Movie Movie { get; set; }

        public MovieScreening(Movie movie, DateTime dateAndTime, Double pricePerSeat) {
            this.Movie = movie;
            this.DateAndTime = dateAndTime;
            this.PricePerSeat = pricePerSeat;
        }

        public override string ToString() {
            return "";
        }
    }
}

