namespace Types
{
    public class audio
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public string artist { get; set; }
        public string title { get; set; }
        public int duration { get; set; }
        public string url { get; set; }
        public int lyrics_id { get; set; }
        public int album_id { get; set; }
        public int genre_id { get; set; }
        public string full_title { 
            get { return artist.Trim() + " - " + title.Trim(); }
        }
        
    }
}