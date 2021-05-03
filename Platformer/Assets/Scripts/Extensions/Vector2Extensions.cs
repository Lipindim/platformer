using UnityEngine;


namespace Assets.Scripts.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 Change(this Vector2 vector, float? newX = null, float? newY = null)
        {
            return new Vector2(newX ?? vector.x, newY ?? vector.y);
        }
    }
}
