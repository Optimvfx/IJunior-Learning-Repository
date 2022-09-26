using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extension
{
    public static class MathExtension
    {
        public static bool AreEquel(float first, float second, float dispersion)
        {
            return Mathf.Abs(Mathf.Max(first, second) - Mathf.Min(first, second)) < dispersion;
        }
    }
}
