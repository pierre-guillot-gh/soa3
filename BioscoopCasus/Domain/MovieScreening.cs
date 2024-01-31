namespace BioscoopCasus.Domain {
    public class MovieScreening {

        public DateTime DateAndTime { get; set; }
        public double PicePerSeat { get; set; }
        public Movie Movie { get; set; }

        public MovieScreening(Movie movie, DateTime dateAndTime, Double pricePerSeat) {
            this.Movie = movie;
            this.DateAndTime = dateAndTime;
            this.PicePerSeat = pricePerSeat;
        }

        public override string ToString() {
            return "";
        }
    }
}

