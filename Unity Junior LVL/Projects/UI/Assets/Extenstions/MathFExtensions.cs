using UnityEngine;
using System;

namespace Extensions
{
    public static class MathFExtensions
    {
        public static bool Equals(float first, float second, float maximalDispersion)
        {
            if (maximalDispersion < 0)
                throw new ArgumentException();

            return Mathf.Max(first, second) - Mathf.Min(first, second) <= maximalDispersion;
        }
    }
}
