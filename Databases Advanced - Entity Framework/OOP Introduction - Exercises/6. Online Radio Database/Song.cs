namespace _6.Online_Radio_Database
{
    public class Song
    {
        private string name;
        private string singer;
        private int seconds;
        private int minutes;
		
		public Song(string singer, string name, int minutes, int seconds)
        {
            this.Singer = singer;
            this.Name = name;
            this.Minutes = minutes;
            this.Seconds = seconds;
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (value.Length < 3 || value.Length > 30)
                {
                    throw new InvalidSongNameException();
                }

                this.name = value;
            }
        }

        public string Singer
        {
            get { return this.singer; }
            set
            {
                if (value.Length < 3 || value.Length > 20)
                {
                    throw new InvalidArtistNameException();
                }
                this.singer = value;
            }
        }

        public int Seconds
        {
            get { return this.seconds; }
            set
            {
                if (value < 0 || value > 59)
                {
                    throw new InvalidSongSecondsException();
                }

                this.seconds = value;
            }
        }

        public int Minutes
        {
            get { return this.minutes; }
            set
            {
                if (value < 0 || value > 14)
                {
                    throw new InvalidSongMinutesException();
                }

                this.minutes = value;
            }
        }
    }
}
