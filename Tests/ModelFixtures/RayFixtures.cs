using System;
using CSharpSimpleRayTracer.Models;
using NUnit.Framework;

namespace Tests.ModelFixtures
{
    [TestFixture]
    public class RayFixtures
    {
        [Test]
        public void Can_get_point_at_parameter()
        {
            // Arrange
            var rayUnderTest = new Ray(
                new Vec3(0, 0, 0),
                new Vec3(1, 1, 1)
            );

            // Act
            var result = rayUnderTest.PointAtParameter(5);

            // Assert
            Assert.That(result.X, Is.EqualTo(5));
            Assert.That(result.Y, Is.EqualTo(5));
            Assert.That(result.Z, Is.EqualTo(5));
        }
    }
}
