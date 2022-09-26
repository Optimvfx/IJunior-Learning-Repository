using UnityEngine;

namespace Extensions
{
    public static class GameObjectExtensions
    {
        public class MonoBehivour2D : MonoBehaviour
        {
            public Vector2 Forward => transform.right;
        }
    }
}
