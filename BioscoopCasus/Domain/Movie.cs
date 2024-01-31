namespace BioscoopCasus.Domain {
    public class Movie {
        public string Title { get; set; }
        public List<MovieScreening> Screenings { get; set; }

        public Movie(string title) {
            this.Title = title;
            this.Screenings = new List<MovieScreening>();
        }

        public void AddScreening(MovieScreening screening) => this.Screenings.Add(screening);

        public override string ToString() {
            return "";
        }
    }
}

