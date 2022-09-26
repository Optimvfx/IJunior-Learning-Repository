using Extensions;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class VectorExtensionsTests
    {        
        //Identyty
        [TestCase(0f, 0f, 0f, 0f, 0f, 0.1f)]
        [TestCase(-15f, -15f, -15f, -15f, 0f, 0.1f)]

        //Positive
        [TestCase(10f, 10f, 10f, 15f, 5f, 0.1f)]
        [TestCase(10f, 10f, 15f, 15f, 7f, 0.1f)]

        //Negative
        [TestCase(-10f, -10f, -15f, -15f, 7f, 0.1f)]
        [TestCase(-10f,-10f, -15f, -10f, 5f, 0.1f)]

        //Combined
        [TestCase(-10, 10, 10, -10, 28.3f, 0.2f)]
        [TestCase(10, 10, -15, -15, 35.55f, 0.2f)]
        [TestCase(10, -10, -15, -10, 25f, 0.2f)]
        public void VectorExtensionsGetDistanceTests(float firstXPoistion, float firstYPoistion, float secondXPoistion, float secondYPoistion, float exteptedDistanse, float maximalDispersion)
        {
            var distance = VectorExtensions.GetDistance(new Vector2(firstXPoistion, firstYPoistion), new Vector2(secondXPoistion, secondYPoistion));

            Debug.Log($"First: X {firstXPoistion} Y {firstYPoistion}, Second: X {secondXPoistion} Y {secondYPoistion}, Distance {distance}.");

            Assert.True(MathFExtensions.Equals(distance, exteptedDistanse, maximalDispersion));
        }

        //Identyty
        [TestCase(0f, 0f, 0f, 0f, 0f, 3f)]
        [TestCase(-15f, -15f, -15f, -15f, 0f, 3f)]

        //Positive
        [TestCase(10f, 10f, 10f, 15f, 11.3f, 3f)]
        [TestCase(10f, 10f, 15f, 15f, 0f, 3f)]

        //Negative
        [TestCase(-10f, -10f, -15f, -15f, 0f, 3f)]
        [TestCase(-10f, -10f, -15f, -10f, 11.3f, 3f)]

        //Combined
        [TestCase(-10, 10, 10, -10, 180f, 3f)]
        [TestCase(10, 10, -15, -15, 180f, 3f)]
        [TestCase(10, -10, -15, -10, 101.1f, 3f)]
        public void VectorExtensionsGetAngleTests(float firstXPoistion, float firstYPoistion, float secondXPoistion, float secondYPoistion, float exteptedAngle, float maximalDispersion)
        {
            var angle = VectorExtensions.GetAngleBetwinPoints(new Vector2(firstXPoistion, firstYPoistion), new Vector2(secondXPoistion, secondYPoistion), Vector2.up);

            Debug.Log($"First: X {firstXPoistion} Y {firstYPoistion}, Second: X {secondXPoistion} Y {secondYPoistion}, Angle == {angle}.");

            Assert.True(MathFExtensions.Equals(angle, exteptedAngle, maximalDispersion));
        }
    }
}
