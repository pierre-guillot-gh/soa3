namespace BioscoopCasus.Domain {
    public class MovieTicket {
        public MovieScreening MovieScreening { get; set; }
        public int RowNr { get; set; }
        public int SeatNr { get; set; }
        public bool IsPremium { get; set; }

        public MovieTicket(MovieScreening movieScreening, int rowNr, int seatNr, bool isPremium) {
            this.MovieScreening = movieScreening;
            this.RowNr = rowNr;
            this.SeatNr = seatNr;
            this.IsPremium = isPremium;
        }

        public double GetPrice() => MovieScreening.PricePerSeat;

        public bool isPremiumTicket() => IsPremium;

        public DateTime GetDateAndTime() => MovieScreening.DateAndTime;

        public override string ToString() {
            return "";
        }
    }
}
