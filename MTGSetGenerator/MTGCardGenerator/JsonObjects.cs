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
        public List<int> cardIds = new List<int>();

        [DataMember]
        public string setName;

        [DataMember]
        public string setDetails;

        [DataMember]
        public string setIconPath;

        private BitmapImage setIcon = null;
        public BitmapImage SetIcon
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
    public class JsonCard
    {
        [DataMember]
        public string name;
    }
}
