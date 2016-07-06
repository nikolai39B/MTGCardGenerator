using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGSetGenerator
{
    class MultiwayMap<TKeyOne, TKeyTwo>
    {
        /// <summary>
        /// Initializes an empty MultiwayMap.
        /// </summary>
        public MultiwayMap()
        {
        }

        /// <summary>
        /// Initializes a MultiwayMap using the given dictionary as the starting values.
        /// </summary>
        /// <param name="initialValues">The starting values for the map.</param>
        public MultiwayMap(Dictionary<TKeyOne, TKeyTwo> initialValues)
        {
            foreach (var pair in initialValues)
            {
                Add(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// Initializes a MultiwayMap using the given dictionary as the starting values.
        /// </summary>
        /// <param name="initialValues">The starting values for the map.</param>
        public MultiwayMap(Dictionary<TKeyTwo, TKeyOne> initialValues)
        {
            foreach (var pair in initialValues)
            {
                Add(pair.Value, pair.Key);
            }
        }

        //--------------//
        // Private Data //
        //--------------//
        private Dictionary<TKeyOne, TKeyTwo> tKeyOneToTKeyTwoMap = new Dictionary<TKeyOne, TKeyTwo>();
        private Dictionary<TKeyTwo, TKeyOne> tKeyTwoToTKeyOneMap = new Dictionary<TKeyTwo, TKeyOne>();

        
        //---------------//
        // Adding Values //
        //---------------//
        /// <summary>
        /// Adds an entry with the given values. Will not overwrite existing entries.
        /// </summary>
        /// <param name="tKeyOneKey">The key of type TKeyOne.</param>
        /// <param name="tKeyTwoKey">The key of type TKeyTwo.</param>
        /// <exception cref="ArgumentException">Thrown if the collection already contains an entry with one of the given keys.</exception>
        public void Add(TKeyOne tKeyOneKey, TKeyTwo tKeyTwoKey)
        {
            // Check to make sure we don't have duplicates.
            if (tKeyOneToTKeyTwoMap.Keys.Contains(tKeyOneKey))
            {
                throw new ArgumentException("Collection already contains an entry with the given fKey.");
            }
            if (tKeyTwoToTKeyOneMap.Keys.Contains(tKeyTwoKey))
            {
                throw new ArgumentException("Collection already contains an entry with the given gKey.");
            }

            // Assign the new values
            tKeyOneToTKeyTwoMap[tKeyOneKey] = tKeyTwoKey;
            tKeyTwoToTKeyOneMap[tKeyTwoKey] = tKeyOneKey;
        }

        /// <summary>
        /// Adds an entry with the given values. Will update existing entries if necessary.
        /// </summary>
        /// <param name="fKey">The key of type F.</param>
        /// <param name="gKey">The key of type G.</param>
        public void AddOrUpdate(TKeyOne tKeyOneKey, TKeyTwo tKeyTwoKey)
        {
            // Check to make sure we don't have duplicates.
            if (tKeyOneToTKeyTwoMap.Keys.Contains(tKeyOneKey))
            {
                tKeyOneToTKeyTwoMap.Remove(tKeyOneKey);
            }
            if (tKeyTwoToTKeyOneMap.Keys.Contains(tKeyTwoKey))
            {
                tKeyTwoToTKeyOneMap.Remove(tKeyTwoKey);
            }

            // Assign the new values
            tKeyOneToTKeyTwoMap[tKeyOneKey] = tKeyTwoKey;
            tKeyTwoToTKeyOneMap[tKeyTwoKey] = tKeyOneKey;
        }


        //-----------------//
        // Removing Values //
        //-----------------//

        /// <summary>
        /// Removes the entry with the given key from the collection.
        /// </summary>
        /// <param name="key">The key whose entry to remove.</param>
        public void Remove(TKeyOne key)
        {
            TKeyTwo gMapKey = tKeyOneToTKeyTwoMap[key];
            tKeyOneToTKeyTwoMap.Remove(key);
            tKeyTwoToTKeyOneMap.Remove(gMapKey);
        }

        /// <summary>
        /// Removes the entry with the given key from the collection.
        /// </summary>
        /// <param name="key">The key whose entry to remove.</param>
        public void Remove(TKeyTwo key)
        {
            TKeyOne fMapKey = tKeyTwoToTKeyOneMap[key];
            tKeyTwoToTKeyOneMap.Remove(key);
            tKeyOneToTKeyTwoMap.Remove(fMapKey);
        }


        //------------------//
        // Accessing Values //
        //------------------//
        public TKeyTwo this[TKeyOne key]
        {
            get
            {
                return tKeyOneToTKeyTwoMap[key];
            }
            set
            {
                AddOrUpdate(key, value);
            }
        }
        public TKeyOne this[TKeyTwo key]
        {
            get
            {
                return tKeyTwoToTKeyOneMap[key];
            }
            set
            {
                AddOrUpdate(value, key);
            }
        }

        public List<TKeyOne> KeyOnes { get { return tKeyOneToTKeyTwoMap.Keys.ToList(); } }
        public List<TKeyTwo> KeyTwos { get { return tKeyTwoToTKeyOneMap.Keys.ToList(); } }
        public List<Tuple<TKeyOne, TKeyTwo>> KeyPairs 
        {
            get
            {
                return (from pair in tKeyOneToTKeyTwoMap
                        select new Tuple<TKeyOne, TKeyTwo>(pair.Key, pair.Value)
                        ).ToList();
            }
        }
    }
}
