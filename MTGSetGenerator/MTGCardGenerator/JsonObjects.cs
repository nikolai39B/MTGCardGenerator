using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Windows.Media.Imaging;

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
        internal string setName;

        [DataMember]
        internal string setDetails;

        [DataMember]
        internal string setIconPath;

        private BitmapImage setIcon = null;
        internal BitmapImage SetIcon
        {
            get
            {
                if (setIcon == null)
                {
                    setIcon = new BitmapImage(new Uri(setIconPath));
                }
                return setIcon;
            }
            set
            {
                setIcon = value;
            }
        }
    }

    [DataContract]
    internal class JsonCard
    {
        [DataMember]
        internal string name;
    }
}
