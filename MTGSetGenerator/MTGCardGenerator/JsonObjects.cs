using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace MTGSetGenerator
{

    [DataContract]
    internal class JsonCollection
    {
        [DataMember]
        internal List<JsonSet> sets = new List<JsonSet>();

        [DataMember]
        internal List<JsonCard> cards = new List<JsonCard>();
    }

    [DataContract]
    internal class JsonSet
    {
        [DataMember]
        internal List<int> cardIds = new List<int>();
    }

    [DataContract]
    internal class JsonCard
    {
        [DataMember]
        internal string name;
        internal string cost;
        internal int cmc;
        internal string type;
        internal string subtype;
        internal string text;
        internal int power;
        internal int toughness;
        internal char rarity;
    }
}
