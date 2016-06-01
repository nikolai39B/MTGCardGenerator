using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MTGSetGenerator
{
    static class CardCollectionManager
    {
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
            jsonCollectionFilePath = Directory.GetCurrentDirectory() + "\\CardCollection.json";
            ReadJsonCollection();
        }

        /// <summary>
        /// Read and deserialize the json collection into memory.
        /// </summary>
        public static void ReadJsonCollection()
        {

            // Read from File
            string json;

            // Provided we actually have a file already
            if (File.Exists(jsonCollectionFilePath))
            {
                // Read all file text into string, then into byte array, then into stream
                json = File.ReadAllText(jsonCollectionFilePath);
                byte[] byteArray = Encoding.UTF8.GetBytes(json);
                MemoryStream jsonStream = new MemoryStream(byteArray);

                // Deserialize into data object
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(JsonCollection));
                Collection = (JsonCollection)serializer.ReadObject(jsonStream);
            }
            else
            {
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

            //Write out to file
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            string jsonText = reader.ReadToEnd();
            File.WriteAllText(jsonCollectionFilePath, jsonText);
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
            Collection.cards.Add(newCard);
            WriteJsonCollection();
        }

    }
}
