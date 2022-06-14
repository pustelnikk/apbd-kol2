using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kol2b.Models
{
    public class Musician
    {
        public int IdMusician { get;set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public virtual ICollection<MusicianTrack> MusicianTrack { get; set; }
    }
}
