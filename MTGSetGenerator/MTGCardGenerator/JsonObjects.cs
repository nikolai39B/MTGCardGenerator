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

        [DataMember]
        internal string setDetails;
    }

    [DataContract]
    internal class JsonCard
    {
        [DataMember]
        internal string name;
        [DataMember]
        internal string cost;
        [DataMember]
        internal int cmc;
        [DataMember]
        internal string type;
        [DataMember]
        internal string subtype;
        [DataMember]
        internal string text;
        [DataMember]
        internal string power;
        [DataMember]
        internal string toughness;
        [DataMember]
        internal Rarity rarity;


        internal enum Rarity 
        { 
            COMMON,
            UNCOMMON,
            RARE,
            MYTHIC
        }

    }
}
