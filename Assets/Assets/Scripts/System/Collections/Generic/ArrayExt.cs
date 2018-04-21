using Helpers;

namespace System.Collections.Generic
{
	public static class ArrayExt
	{
		public delegate U Callback<T, U>(T item);

		public static U[] Map<T, U>(this T[] array, Callback<T, U> callback)
		{
			int length = array.Length;

			var output = new U[length];

			for (int i = 0; i < length; i++)
				output [i] = callback (array [i]);
			
			return output;
		}

		public static U[,] Map<T, U>(this T[,] array, Func<T, U> callback)
		{
			int width = array.GetLength(0);
			int height = array.GetLength(1);

			var output = new U[width,height];

			for (int x = 0; x < width; x++)
				for (int y = 0; y < height; y++)
					output[x, y] = callback(array[x, y]);

			return output;
		}



		public static string Join<T>(this T[] array, string separator=",")
		{
			string r = "";
			foreach (T item in array) {
				r += item.ToString () + separator;
			}
			r = r.Remove (r.Length - 1 - separator.Length);
			return r;
		}

		public static string Join<T>(this T[,] array, string separator, string superSeparator)
		{
			string r = "";
			for (int i = 0; i < array.GetLength (0); i++) {
				for (int j = 0; j < array.GetLength (1); j++) {
					T item = array [i, j];
					r += (item != null) ? item.ToString () : "null";
					r += separator;
				}
				r = r.Remove (r.Length - 1 - separator.Length);
				r += superSeparator;
			}
			r = r.Remove (r.Length - 1 - superSeparator.Length);
			return r;
		}

		public static T[] Filter<T>(this T[] array, Delegates.TBool<T> callback)
		{
			List<T> filteredList = new List<T> ();

			foreach (T item in array) {
				if (callback (item))
					filteredList.Add (item);
			}
			return filteredList.ToArray ();
		}

		public static T[] Filter<T>(this T[,] array2d, Delegates.TBool<T> callback)
		{
			List<T> filteredList = new List<T> ();

			foreach (T item in array2d) {
				if (callback (item))
					filteredList.Add (item);
			}
			return filteredList.ToArray ();
		}

		public static int Max(this int[] array)
		{
			int max = array [0];
			foreach (int val in array) {
				if (val > max)
					max = val;
			}
			return max;
		}

		public static int Min(this int[] array)
		{
			int min = array [0];
			foreach (int val in array) {
				if (val < min)
					min = val;
			}
			return min;
		}

		public static List<T> ToList<T>(this T[] array)
		{
			List<T> list = new List<T> ();
			foreach (T item in array)
				list.Add (item);
			return list;
		}

		public static T[] Unshifted<T>(this T[] array, params T[] items)
		{
			T[] unshifted = new T[array.Length + items.Length];
			for (int i = 0; i < items.Length; i++) {
				unshifted [i] = items [i];
			}
			for (int i = 0; i < array.Length; i++) {
				unshifted [items.Length + i] = array [i];
			}
			return unshifted;
		}

        public static bool[,] Smooth(this bool[,] array, int iterationCount = 1, int minNeighborCountToAdd = 4, int maxNeighborCountToRemove = 4, float probabilityToAdd = 1f, float probabilityToRemove = 1f)
		{
			bool[,] smoothed = (bool[,])array.Clone();
			for (int x = 0; x < smoothed.GetLength(0); x++)
			{
				for (int y = 0; y < smoothed.GetLength(1); y++)
				{
					int neighbourWallTiles = array.GetSurroundingWallCount(x, y);

					if (neighbourWallTiles > minNeighborCountToAdd && Rnd.Bool(probabilityToAdd))
						smoothed[x, y] = true;
					else if (neighbourWallTiles < maxNeighborCountToRemove && Rnd.Bool(probabilityToRemove))
						smoothed[x, y] = false;

				}
			}
			return smoothed;
		}

		static int GetSurroundingWallCount(this bool[,] array, int gridX, int gridY)
		{
			int wallCount = 0;
			for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
			{
				for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
				{
					if (neighbourX >= 0 && neighbourX < array.GetLength(0) && neighbourY >= 0 && neighbourY < array.GetLength(1))
					{
						if (neighbourX != gridX || neighbourY != gridY)
						{
							wallCount += array[neighbourX, neighbourY]?1:0;
						}
					}
					else
					{
						wallCount++;
					}
				}
			}

			return wallCount;
		}
	}
}

