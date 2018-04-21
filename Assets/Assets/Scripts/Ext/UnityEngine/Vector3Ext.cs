namespace UnityEngine
{
    public static class Vector3Ext
    {
        public static void SetMagnitude(this Vector3 vector, float magnitude)
        {
            vector = vector / vector.magnitude * magnitude;
        }
    }
}

