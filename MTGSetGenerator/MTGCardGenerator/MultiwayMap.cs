using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGSetGenerator
{
    class MultiwayMap<F, G>
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
        public MultiwayMap(Dictionary<F, G> initialValues)
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
        public MultiwayMap(Dictionary<G, F> initialValues)
        {
            foreach (var pair in initialValues)
            {
                Add(pair.Value, pair.Key);
            }
        }

        //--------------//
        // Private Data //
        //--------------//
        private Dictionary<F, G> fToGMap = new Dictionary<F, G>();
        private Dictionary<G, F> gToFMap = new Dictionary<G, F>();

        
        //---------------//
        // Adding Values //
        //---------------//
        /// <summary>
        /// Adds an entry with the given values. Will not overwrite existing entries.
        /// </summary>
        /// <param name="fKey">The key of type F.</param>
        /// <param name="gKey">The key of type G.</param>
        /// <exception cref="ArgumentException">Thrown if the collection already contains an entry with one of the given keys.</exception>
        public void Add(F fKey, G gKey)
        {
            // Check to make sure we don't have duplicates.
            if (fToGMap.Keys.Contains(fKey))
            {
                throw new ArgumentException("Collection already contains an entry with the given fKey.");
            }
            if (gToFMap.Keys.Contains(gKey))
            {
                throw new ArgumentException("Collection already contains an entry with the given gKey.");
            }

            // Assign the new values
            fToGMap[fKey] = gKey;
            gToFMap[gKey] = fKey;
        }

        /// <summary>
        /// Adds an entry with the given values. Will update existing entries if necessary.
        /// </summary>
        /// <param name="fKey">The key of type F.</param>
        /// <param name="gKey">The key of type G.</param>
        public void AddOrUpdate(F fKey, G gKey)
        {
            // Check to make sure we don't have duplicates.
            if (fToGMap.Keys.Contains(fKey))
            {
                fToGMap.Remove(fKey);
            }
            if (gToFMap.Keys.Contains(gKey))
            {
                gToFMap.Remove(gKey);
            }

            // Assign the new values
            fToGMap[fKey] = gKey;
            gToFMap[gKey] = fKey;
        }


        //-----------------//
        // Removing Values //
        //-----------------//

        /// <summary>
        /// Removes the entry with the given key from the collection.
        /// </summary>
        /// <param name="key">The key whose entry to remove.</param>
        public void Remove(F key)
        {
            G gMapKey = fToGMap[key];
            fToGMap.Remove(key);
            gToFMap.Remove(gMapKey);
        }

        /// <summary>
        /// Removes the entry with the given key from the collection.
        /// </summary>
        /// <param name="key">The key whose entry to remove.</param>
        public void Remove(G key)
        {
            F fMapKey = gToFMap[key];
            gToFMap.Remove(key);
            fToGMap.Remove(fMapKey);
        }


        //-------------------//
        // Bracket Overloads //
        //-------------------//
        public G this[F key]
        {
            get
            {
                return fToGMap[key];
            }
            set
            {
                AddOrUpdate(key, value);
            }
        }
        public F this[G key]
        {
            get
            {
                return gToFMap[key];
            }
            set
            {
                AddOrUpdate(value, key);
            }
        }
    }
}
