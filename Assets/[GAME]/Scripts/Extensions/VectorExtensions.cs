using UnityEngine;

namespace _GAME_.Scripts.Extensions
{
    public static class VectorExtensions
    {
        private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

        public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);

        public static Vector3 ToVector3XZ(this Vector2 vector2, float yValue = 0.0f)
        {
            return new Vector3(vector2.x, yValue, vector2.y);
        }

        public static Vector2 ToVector2XZ(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.z);
        }
    }
}