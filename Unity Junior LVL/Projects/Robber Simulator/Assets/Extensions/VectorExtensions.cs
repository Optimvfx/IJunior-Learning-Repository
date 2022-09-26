using UnityEngine;

namespace Extensions
{
    public static class VectorExtensions
    {
        public static float GetDistance(Vector2 first, Vector2 second)
        {
            return Mathf.Sqrt(((first.x - second.x) * (first.x - second.x)) + ((first.y - second.y) * (first.y - second.y)));
        }

        public static Degree GetAngleBetwinPoints(Vector2 from, Vector2 target, Vector2 fromLookDirection)
        {
            var targetDirection = target - from;

            float angleBetween = Vector2.SignedAngle(targetDirection, fromLookDirection);

            return new Degree(Mathf.Abs(angleBetween));
        }

        public static Vector2 RotateVector(Vector2 vector, Degree angle)
        {
            return new Vector2((Mathf.Cos(angle.ConvertToRadian()) * vector.x) - (Mathf.Sin(angle.ConvertToRadian()) * vector.y),
                               (Mathf.Sin(angle.ConvertToRadian()) * vector.x) + (Mathf.Cos(angle.ConvertToRadian()) * vector.y));
        }

        public static Vector2 GetNormal(Vector2 from, Vector2 to)
        {
            return (to - from).normalized;
        }

        public struct Degree
        {
            public static readonly float MaximalDegree = 360;

            public readonly float Value;

            public Degree(float value)
            {
                Value = value % MaximalDegree;
            }

            public float ConvertToRadian()
            {
                return Value * Mathf.Deg2Rad;
            }

            public override string ToString()
            {
                return $"Degree {Value}";
            }

            public static implicit operator float(Degree degree) => degree.Value;
        }
    }
}

