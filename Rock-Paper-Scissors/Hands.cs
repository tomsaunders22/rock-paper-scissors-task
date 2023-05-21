using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rock_Paper_Scissors
{
    public class Hands
    {
        public int id { get; set; }
        public String description { get; set; }
        public String shortDesc { get; set; }
        public HandType handChosen { get; set; }
        public IList<HandType> beats { get; set; }
        public IDictionary<HandType, String> beatsDesc { get; set; }
    }

    public enum HandType
    {
        Rock,
        Paper,
        Scissors,
        Lizard,
        Spock
    }
}
