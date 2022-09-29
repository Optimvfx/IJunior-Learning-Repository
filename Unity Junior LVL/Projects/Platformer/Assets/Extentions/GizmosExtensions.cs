using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class GizmosExtensions
    { 
        public static void DrawLine(Vector2 from, Vector2 to, Color color)
        {
            var prewiusColor = Gizmos.color;
            Gizmos.color = color;

            Gizmos.DrawLine(from, to);

            Gizmos.color = prewiusColor;
        }
    }
}
