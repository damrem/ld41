using Helpers;

namespace System.Collections.Generic
{
	public static class ListExt
	{
		public static List<T> Flatten<T>(this List<List<T>> listList, bool dedupe=true)
		{
			List<T> flat = new List<T> ();

			listList.ForEach (delegate(List<T> subList) {
				subList.ForEach(delegate(T item) {
					flat.Add(item);
				});
			});

			if (dedupe)
				flat = Dedupe (flat);

			return flat;
		}

		public static List<T> Dedupe<T>(this List<T> list)
		{
			List<T> deduped = new List<T> ();
			list.ForEach (delegate (T item){
				if(!deduped.Contains(item))
					deduped.Add(item);
			});
			return deduped;
		}

		public static T[] ToArray<T>(this List<T> list)
		{
			T[] array = new T[list.Count];
			for (int i = 0; i < list.Count; i++) {
				array [i] = list [i];
			}
			return array;
		}

        public delegate T Filler0<T>();
        public delegate T Filler1<T>(T item);
        public delegate T Filler2<T>(T item, int index);
        public delegate T Filler3<T>(T item, int index, List<T> list);

        public static List<T> Fill<T>(this List<T> list, Filler0<T> filler)
        {
            for (int i = 0; i < list.Capacity; i++)
                list.Insert(i, filler());
            return list;
        }

        public static List<T> Fill<T>(this List<T> list, Filler1<T> filler)
        {
            for (int i = 0; i < list.Capacity; i++)
                list.Insert(i, filler(list[i]));
            return list;
        }

        public static List<T> Fill<T>(this List<T> list, Filler2<T> filler)
        {
            for(int i = 0; i < list.Capacity; i++)
                list.Insert(i, filler(list[i], i));
            return list;
        }

        public static List<T> Fill<T>(this List<T> list, Filler3<T> filler)
        {
            for (int i = 0; i < list.Capacity; i++)
                list.Insert(i, filler(list[i], i, list));
            return list;
        }

        public delegate U Mapper<T, U>(T item);
        public static List<U> Map<T, U>(this List<T> list, Mapper<T, U> mapper)
        {
            int capacity = list.Count;
            var output = new List<U>(capacity);
            for (int i = 0; i < capacity; i++)
                output.Insert(i, mapper(list[i]));
            return output;
        }

        public delegate U IndexedMapper<T, U>(T item, int index);
        public static List<U> Map<T, U>(this List<T> list, IndexedMapper<T, U> mapper)
        {
            int capacity = list.Count;
            var output = new List<U>(capacity);
            for (int i = 0; i < capacity; i++)
                output.Insert(i, mapper(list[i], i));
            return output;
        }

        public static string Stringify<T>(this List<T> list)
        {
            //Dbg.Log("list", list.Count);
            T[] array = list.ToArray();
            //Dbg.Log("array", array.Length);
            string[] stringArray = array.Map((T item) => (item == null) ? "null" : item.ToString());
            //Dbg.Log("stringArray", stringArray.Length);
            return stringArray.Join(";");
        }

        public static bool ContainsAny<T>(this List<T> container, List<T> containeds)
        {
            for (int i = 0; i < containeds.Count; i++)
                if (container.Contains(containeds[i]))
                    return true;
            return false;
        }

        public static bool Any<T>(this List<T> list, Delegates.TBool<T> callback)
        {
            for (int i = 0; i < list.Count; i++)
                if (callback(list[i]))
                    return true;
            return false;
        }
        public static bool None<T>(this List<T> list, Delegates.TBool<T> callback)
        {
            return !list.Any(callback);
        }

        public static List<T> Filter<T>(this List<T> list, Delegates.TBool<T> callback)
        {
            List<T> filteredList = new List<T>();
            foreach (T item in list)
                if (callback(item))
                    filteredList.Add(item);
            return filteredList;
        }

        public static List<T> And<T>(this List<T> list1, List<T> list2)
        {
            List<T> andList = new List<T>();
            foreach(T item1 in list1)
                if (list2.Contains(item1)) andList.Add(item1);
            return andList;
        }


    }


}

