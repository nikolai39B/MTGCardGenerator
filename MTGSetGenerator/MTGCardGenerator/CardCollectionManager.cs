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


        public static void InitializeJsonCollection()
        {
            collectionFilePath = Directory.GetCurrentDirectory() + "\\CardCollection.json";
            ReadJsonCollection();
            // do other init stuff that normally wouldn't happen with a read
        }



        public static void ReadJsonCollection()
        {

            //Read from File
            string json;

            //Provided we actually have a file already
            if (File.Exists(collectionFilePath))
            {

                //Read all file text into string, then into byte array, then into stream
                json = File.ReadAllText(collectionFilePath);
                byte[] byteArray = Encoding.UTF8.GetBytes(json);
                MemoryStream jsonStream = new MemoryStream(byteArray);

                //Deserialize into data object
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(JsonCollection));
                Collection = (JsonCollection)serializer.ReadObject(jsonStream);
            }
            else
            {
                Collection = new JsonCollection();
            }
        }



        public static void WriteJsonCollection()
        {
            // serialization
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(JsonCollection));
            serializer.WriteObject(stream, Collection);

            //Write out to file
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            string jsonText = reader.ReadToEnd();
            File.WriteAllText(collectionFilePath, jsonText);
        }



        public static void AddCard(JsonCard newCard)
        {
            Collection.cards.Add(newCard);
            WriteJsonCollection();
        }

        public static JsonCollection Collection { get; set; }
        private static string collectionFilePath;
    }
}
