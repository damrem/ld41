using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
	public class Rnd
	{
		static System.Random random = new System.Random();

		public static int Int (int min, int max)
		{
			return random.Next (min, max);
		}

		public static double Double(double min, double max)
		{
			return min + random.NextDouble() * (max - min);
		}

		public static float Float(float min=0, float max=1)
		{
			return (float)Double(min, max);
		}

		public static bool Bool(float probability = 0.5f)
        {
            return random.NextDouble() < Mathf.Clamp01(probability);
        }

        public static T InArray<T>(T[] array)
        {
            return array[Int(0, array.Length - 1)];
        }

        public static T InList<T>(List<T> list)
        {
            return InArray(list.ToArray());
        }
	}
}

