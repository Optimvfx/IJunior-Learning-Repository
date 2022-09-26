using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathExtention
{
    public static bool AreEquel(float first, float second, float dispersion)
    {
        return Mathf.Abs(Mathf.Max(first, second) - Mathf.Min(first, second)) < dispersion;
    }
}
