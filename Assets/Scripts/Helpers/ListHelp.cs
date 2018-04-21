using System.Collections.Generic;

namespace Helpers
{
    public class ListHelpers
    {
        public static List<T> FromEnumerable<T>(IEnumerable<T> enumerable)
        {
            List<T> list = new List<T>();
            foreach (T item in enumerable)
            {
                list.Add(item);
            }
            return list;
        }
    }
}