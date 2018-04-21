//using System;
using UnityEngine;

namespace Ext
{
	public static class Vector2Ext
	{
		public static Vector2 UnitVector(this Vector2 vector)
		{
			Vector2 unitVector = new Vector2 ();

			float dx = Mathf.Abs (vector.x);
			float dy = Mathf.Abs (vector.y);

			if (dx > dy){
				unitVector.x = vector.x / dx;
				unitVector.y = 0;
			}
			else{
				unitVector.x = 0;
				unitVector.y = vector.y / dy;
			}

			return unitVector;
		}

        public static void SetMagnitude(this Vector2 vector, float magnitude)
        {
            vector = vector / vector.magnitude * magnitude;
        }
	}
}

