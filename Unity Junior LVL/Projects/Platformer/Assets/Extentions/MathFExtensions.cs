using UnityEngine;

namespace Extensions
{
    public static class MathFExtensions
    {
        public static bool Equals(float first, float second, float maximalDispersion)
        {
            return Mathf.Max(first, second) - Mathf.Min(first, second) <= maximalDispersion;
        }
    }
}
