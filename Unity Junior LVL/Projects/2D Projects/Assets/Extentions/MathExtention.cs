using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathExtention
{
    public static Vector2Int MirageMaximal(Vector2Int first, Vector2Int second)
    {
        return new Vector2Int(Mathf.Max(first.x,second.x), Mathf.Max(first.y,second.y));
    }
}
