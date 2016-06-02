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

        [DataMember]
        public string iconPath;

        private BitmapImage icon = null;
        public BitmapImage Icon
        {
            get
            {
                if (icon == null)
                {
                    icon = new BitmapImage(new Uri(iconPath));
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
        public string name;

        [DataMember]
        public int id;
    }
}
