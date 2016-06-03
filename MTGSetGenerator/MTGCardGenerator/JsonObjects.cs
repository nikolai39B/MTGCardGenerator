using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Windows.Media.Imaging;

namespace MTGSetGenerator
{

    [DataContract]
    public class JsonCollection
    {
        [DataMember]
        public List<JsonSet> sets = new List<JsonSet>();

        [DataMember]
        public List<JsonCard> cards = new List<JsonCard>();
    }

    [DataContract]
    public class JsonSet
    {
        [DataMember]
        public string name;

        [DataMember]
        public int id;

        [DataMember]
        public List<int> cardIds = new List<int>();

        [DataMember]
        public string details;

        public string IconPath { get { return CardCollectionManager.GetPathToSetIcon(id, name); } }
        private BitmapImage icon = null;
        public BitmapImage Icon
        {
            get
            {
                if (icon == null)
                {
                    try
                    {
                        icon = new BitmapImage(new Uri(IconPath));
                    }
                    catch (FileNotFoundException)
                    {
                        ErrorWindow windows = new ErrorWindow(string.Format(
                            "Set {0} has no icon file.", name));
                        windows.ShowDialog();
                        icon = null;
                    }
                }
                return icon;
            }
            set
            {
                icon = value;
            }
        }
    }

    [DataContract]
    public class JsonCard
    {
        [DataMember]
        internal string name;

        [DataMember]
        public int id;

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
