using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotCraftCore.nUtil
{
    class BiMap<T,K> : IDictionary<T,K>
    {
        private IDictionary<T,K> dictForward = new Dictionary<T,K>();
        private IDictionary<K,T> dictBackward = new Dictionary<K,T>();

        public BiMap() { }

        public BiMap(IDictionary<T, K> dict) {
            dictForward = dict;
            var enumerator = dict.GetEnumerator();
            while (enumerator.MoveNext( ))
            {
                KeyValuePair<K,T> tmp = new KeyValuePair<K,T>(enumerator.Current.Value, enumerator.Current.Key);
                dictBackward.Add(tmp);
            }
        }

        public static explicit operator BiMap<T, K>(IDictionary<T, K> dictionary)
        {
            return new BiMap<T, K>(dictionary);
        }

        public void Add(T key, K value)
        {
            if (dictForward.ContainsKey(key))
                throw new ArgumentException("Key {1} already contained in BiMap", key.ToString());
            if (dictBackward.ContainsKey(value))
                throw new ArgumentException("Value {1} already contained in BiMap", value.ToString( ));
            
            dictForward.Add(key, value);
            dictBackward.Add(value, key);

            return;
        }

        public bool ContainsKey(T key)
        {
            return dictForward.ContainsKey(key);
        }

        public bool ContainsValue(K value)
        {
            return dictBackward.ContainsKey(value);
        }

        public ICollection<T> Keys
        {
            get
            {
                return dictForward.Keys;
            }
        }

        public ICollection<K> Values
        {
            get
            {
                return dictBackward.Keys;
            }
        }

        public bool Remove(T key)
        {
            if (dictForward.ContainsKey(key))
            {
                dictBackward.Remove(dictForward[key]);
                return dictForward.Remove(key);
            }

            return false;
        }

        public bool Remove(K value)
        {
            if (dictBackward.ContainsKey(value))
            {
                dictForward.Remove(dictBackward[value]);
                return dictBackward.Remove(value);
            }

            return false;
        }

        public bool TryGetValue(T key, out K value)
        {
            return dictForward.TryGetValue(key, out value);
        }

        public K this[T key]
        {
            get
            {
                return dictForward[key];
            }
            set
            {
                Add(key, value);
            }
        }

        public void Add(KeyValuePair<T, K> item)
        {
            this.Add(item.Key, item.Value);
        }

        public void Clear( )
        {
            dictForward.Clear();
            dictBackward.Clear();
        }

        public bool Contains(KeyValuePair<T, K> item)
        {
            return dictForward.Contains(item);
        }

        public void CopyTo(KeyValuePair<T, K>[] array, int arrayIndex)
        {
            dictForward.CopyTo(array, arrayIndex);
        }

        public void CopyInverseTo(KeyValuePair<K, T>[] array, int arrayIndex)
        {
            dictBackward.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return dictForward.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<T, K> item)
        {
            if (dictForward.Contains(item))
            {
                dictBackward.Remove(new KeyValuePair<K,T>(item.Value, item.Key));
                return dictForward.Remove(item);
            }

            return false;
        }

        public IEnumerator<KeyValuePair<T, K>> GetEnumerator( )
        {
            return dictForward.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator( )
        {
            return dictForward.GetEnumerator();
        }

        public BiMap<K, T> Inverse( )
        {
            var TmpBimap = new BiMap<K,T>();
            var TmpDict = this.dictForward;
            TmpBimap.dictForward = this.dictBackward;
            TmpBimap.dictBackward = TmpDict;

            return TmpBimap;
        }
    }
}
