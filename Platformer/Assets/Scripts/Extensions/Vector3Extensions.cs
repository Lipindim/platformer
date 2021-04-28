using UnityEngine;


namespace Platformer
{
    public static class Vector3Extensions
    {
        public static Vector3 Update(this Vector3 vector, float deltaX = 0, float deltaY = 0, float deltaZ = 0)
        {
            return new Vector3(vector.x + deltaX, vector.y + deltaY, vector.z + deltaZ);
        }

        public static Vector3 Change(this Vector3 vector, float? newX = null, float? newY = null, float? newZ = null)
        {
            return new Vector3(newX ?? vector.x, newY ?? vector.y, newZ ?? vector.z);
        }
    }
}
