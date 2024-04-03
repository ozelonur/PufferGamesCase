using UnityEngine;

namespace _GAME_.Scripts.Extensions
{
    public static class RandomExtensions
    {
        public static float GetRandom(float min, float max) => Random.Range(min, max);
        public static int GetRandom(int min, int max) => Random.Range(min, max);
    }
}