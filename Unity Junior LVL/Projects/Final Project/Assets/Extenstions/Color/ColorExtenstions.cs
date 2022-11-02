using UnityEngine;

public class ColorExtenstions
{
    public UFloat CompareColors(Color first, Color second)
    {
        var rCompareResult = Mathf.Abs(first.r - second.r);
        var gCompareResult = Mathf.Abs(first.g - second.g);
        var bCompareResult = Mathf.Abs(first.b - second.b);

        return rCompareResult * gCompareResult * bCompareResult;
    }
}
