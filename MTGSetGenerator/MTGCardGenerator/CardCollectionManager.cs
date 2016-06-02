using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows;

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
        public static JsonCollection Collection { get; set; }
        
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
        /// Check all lists in the card collection and turn any null references into empty lists.
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
            
            // Check the list of card ids for each set
            foreach (var set in Collection.sets)
            {
                if (set.cardIds == null)
                {
                    set.cardIds = new List<int>();
                }
            }

        }


        //-----------------------//
        // Collection Management //
        //-----------------------//

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
    }
}
