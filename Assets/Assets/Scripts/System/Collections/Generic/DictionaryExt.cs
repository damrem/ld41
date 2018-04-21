namespace System.Collections.Generic
{
	public static class DictionaryExt
	{
		public static V Get<K, V> (this Dictionary<K, V> dictionary, K key)
		{
			V val;
			dictionary.TryGetValue (key, out val);
			return val;
		}

		public static void Set<K, V>(this Dictionary<K,V> dictionary, K key, V val)
		{
			dictionary [key] = val;
		}

        public static string Stringify<K,V>(this Dictionary<K, V> dictionary)
        {
            string result = "";
            foreach(KeyValuePair<K,V> pair in dictionary)
            {
                result += pair.Key.ToString() + ": " + pair.Value.ToString() + "; ";
            }
            return result;
        }

        public static Dictionary<K,V> Clone<K,V>(this Dictionary<K,V> dictionary)
        {
            Dictionary<K, V> clone = new Dictionary<K, V>();
            foreach (KeyValuePair<K, V> pair in dictionary)
                clone.Add(pair.Key, pair.Value);
            return clone;
        }

        public static void RemoveValue<K, V>(this Dictionary<K, V> dictionary, V value, bool all = true)
        {
            Dictionary<K, V> clone = dictionary.Clone();
            foreach(KeyValuePair<K,V> pair in clone)
            {
                if (pair.Value.Equals(value))
                {
                    dictionary.Remove(pair.Key);
                    if (!all) return;
                }
            }
        }
	}
}

