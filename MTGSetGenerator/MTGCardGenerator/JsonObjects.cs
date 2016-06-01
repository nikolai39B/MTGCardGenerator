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
        internal List<JsonSet> sets = new List<JsonSet>();
        internal List<JsonCard> cards = new List<JsonCard>();
    }


    [DataContract]
    internal class JsonSet
    {
        internal List<int> cardIds = new List<int>();
    }



    [DataContract]
    internal class JsonCard
    {
        [DataMember]
        internal string name;
    }
}
