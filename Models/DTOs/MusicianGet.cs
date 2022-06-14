using System.Collections.Generic;

namespace kol2b.Models.DTOs
{
    public class MusicianGet
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }

        public List<Track> Tracks { get; set; }
    }
}
