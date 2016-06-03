using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MTGSetGenerator
{
    static class CardCollectionManager
    {
        static CardCollectionManager()
        {
            // Set up json file directory
            string jsonCollectionFileFolder = Path.Combine(Directory.GetCurrentDirectory(), "data");
            jsonCollectionFilePath = Path.Combine(jsonCollectionFileFolder, "card_collection.json");

            // Create the directory if necessary
            if (!Directory.Exists(jsonCollectionFileFolder))
            {
                Directory.CreateDirectory(jsonCollectionFileFolder);
            }

            // Create the json file if necessary
            if (!File.Exists(jsonCollectionFilePath))
            {
                // NOTE: if the json format for storing card collections changes, the default file contents here should change as well
                string defaultFileContents = "{\"cards\":[],\"sets\":[]}";
                File.WriteAllText(jsonCollectionFilePath, defaultFileContents);
            }       
        }

        //-----------------//
        // Json Management //
        //-----------------//

        private static string jsonCollectionFilePath;
        private static JsonCollection Collection { get; set; }
        
        /// <summary>
        /// Initialize the internal json memory objects.
        /// </summary>
        public static void InitializeJsonCollection()
        {            
            ReadJsonCollection();
        }

        /// <summary>
        /// Read and deserialize the json collection into memory.
        /// </summary>
        public static void ReadJsonCollection()
        {
            try
            {
                // Read all file text into a string, then into a byte array, then into a stream
                string json = File.ReadAllText(jsonCollectionFilePath);
                byte[] byteArray = Encoding.UTF8.GetBytes(json);
                MemoryStream jsonStream = new MemoryStream(byteArray);

                // Deserialize into the Collection data object
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(JsonCollection));
                Collection = (JsonCollection)serializer.ReadObject(jsonStream);
                FinishJsonCollectionPropertyInitialization();
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format(
                    "Error in CardCollectionManager.ReadJsonCollection():\n{0}", e.Message),
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Collection = new JsonCollection();
            }            
        }

        /// <summary>
        /// Serialize and write the card collection into a json file.
        /// </summary>
        public static void WriteJsonCollection()
        {
            // Serialize our collection of cards
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(JsonCollection));
            serializer.WriteObject(stream, Collection);

            // Write out to file
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            string jsonText = reader.ReadToEnd();
            File.WriteAllText(jsonCollectionFilePath, jsonText);
        }

        /// <summary>
        /// Check all lists in the card collection and turn any null references into empty lists. Also sets the highest set and card id fields.
        /// </summary>
        private static void FinishJsonCollectionPropertyInitialization()
        {
            // Check the list of cards in the collection
            if (Collection.cards == null)
            {
                Collection.cards = new List<JsonCard>();
            }

            // Check the list of sets in the collection
            if (Collection.sets == null)
            {
                Collection.sets = new List<JsonSet>();
            }
            
            // Check the list of card ids for each set, and also set the highest set id field
            foreach (var set in Collection.sets)
            {
                if (set.cardIds == null)
                {
                    set.cardIds = new List<int>();
                }
                highestSetId = Math.Max(set.id, highestSetId);
            }

            // Set the highest card id field
            foreach (var card in Collection.cards)
            {
                highestCardId = Math.Max(card.id, highestCardId);
            }
        }


        //-----------------------//
        // Collection Management //
        //-----------------------//

        public static List<JsonSet> Sets { get { return Collection.sets; } }
        public static List<JsonCard> Cards { get { return Collection.cards; } }

        private static int highestSetId = -1;
        private static int highestCardId = -1;

        /// <summary>
        /// Add a new json card to the card collection.
        /// </summary>
        /// <param name="newCard">The card to add.</param>
        public static void AddCard(JsonCard newCard)
        {
            if (!Collection.cards.Contains(newCard))
            {
                Collection.cards.Add(newCard);
                WriteJsonCollection();
            }

            highestCardId = Math.Max(newCard.id, highestCardId);
        }

        /// <summary>
        /// Removes a json card from the card collection.
        /// </summary>
        /// <param name="card">The card to remove.</param>
        public static void RemoveCard(JsonCard card)
        {
            if (Collection.cards.Contains(card))
            {
                Collection.cards.Remove(card);
                WriteJsonCollection();
            }
        }

        /// <summary>
        /// Add a new json set to the card collection.
        /// </summary>
        /// <param name="newSet">The set to add.</param>
        public static void AddSet(JsonSet newSet)
        {
            if (!Collection.sets.Contains(newSet))
            {
                Collection.sets.Add(newSet);
                WriteJsonCollection();
            }

            highestSetId = Math.Max(newSet.id, highestSetId);
        }

        /// <summary>
        /// Removes a json set from the card collection.
        /// </summary>
        /// <param name="set">The set to remove.</param>
        public static void RemoveSet(JsonSet set)
        {
            if (Collection.sets.Contains(set))
            {
                Collection.sets.Remove(set);
                WriteJsonCollection();
            }
        }

        /// <summary>
        /// Returns a new set id that is not used by any existing set.
        /// </summary>
        public static int RequestUniqueSetId()
        {
            highestSetId++;
            return highestSetId;
        }

        /// <summary>
        /// Returns a new set id that is not used by any existing set.
        /// </summary>
        public static int RequestUniqueCardId()
        {
            highestCardId++;
            return highestCardId;
        }


        //------------------//
        // Image Management //
        //------------------//

        /// <summary>
        /// Gets the full path to the file where the icon of the given set should be stored.
        /// </summary>
        /// <param name="setId">The id of the set whose path to return.</param>
        /// <param name="setName">The name of the set whose path to return.</param>
        /// <returns>The path to the set icon.</returns>
        public static string GetPathToSetIcon(int setId, string setName)
        {
            return Path.Combine(
                Directory.GetCurrentDirectory(),
                string.Format("data/{0}_{1}_icon.png", setId, setName));
        }

        /// <summary>
        /// Writes the icon for the given set to './data/SET-ID_SET-NAME_icon.png
        /// </summary>
        /// <param name="setId">The id of the set whose icon to write.</param>
        /// <param name="setName">The name of the set whose icon to write.</param>
        /// <param name="setIcon">The icon of the set whose icon to write.</param>
        /// <returns>The full path to the new image file.</returns>
        public static string WriteSetIconImage(int setId, string setName, BitmapImage setIcon)
        {
            // Build the icon path
            string iconPath = GetPathToSetIcon(setId, setName);

            // Make sure we have an icon to save
            if (setIcon == null)
            {
                throw new InvalidOperationException(string.Format(
                    "Cannot write icon for set {0}; set has no icon.", setName));
            }

            // Encode and save the image
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(setIcon));
            using (var stream = new FileStream(iconPath, FileMode.Create))
            {
                encoder.Save(stream);
            }

            return iconPath;
        }
    }
}
