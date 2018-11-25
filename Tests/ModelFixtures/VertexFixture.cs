using System;
using CSharpSimpleRayTracer.Models;
using NUnit.Framework;

namespace Tests.ModelFixtures
{
    [TestFixture]
    public class VertexFixture
    {
        [Test]
        public void Can_get_square_of_vertex()
        {
            // Arrange

            var vertex = new Vertex(3, 4, 5);
            
            // Act
            var length = vertex.LengthSquared();

            // Assert
            Assert.That(length, Is.EqualTo(50));
        }


        [TestCase(0, 1, 0, 1, 0, 0, 0, 0, -1)]
        [TestCase(1, 0, 0, 0, 1, 0, 0, 0, 1)]
        public void Can_get_cross_product(
            double x1,
            double y1,
            double z1,
            double x2,
            double y2,
            double z2,
            double ex,
            double ey,
            double ez)
        {
            // Arrange
            var vertex1 = new Vertex(x1, y1, z1);
            var vertex2 = new Vertex(x2, y2, z2);

            // Act
            var result = vertex1.Cross(vertex2);

            // Assert
            Assert.That(result.X, Is.EqualTo(ex), "X component is wrong");
            Assert.That(result.Y, Is.EqualTo(ey), "Y component is wrong");
            Assert.That(result.Z, Is.EqualTo(ez), "Z component is wrong");

        }

        [Test]
        public void Can_normalise_a_vector()
        {
            // Arrange

            var vertex = new Vertex(3, 4, 5);

            // Act
            vertex.Normalise();

            // Assert

            Assert.That(vertex.X, Is.EqualTo(0.424264).Within(0.0001));
            Assert.That(vertex.Y, Is.EqualTo(0.565685).Within(0.0001));
            Assert.That(vertex.Z, Is.EqualTo(0.707107).Within(0.0001));
        }

        [TestCase(0, 1, 0, 1, 0, 0, 0)]
        [TestCase(1, 2, 3, 1, 5, 7, 32)]
        public void Can_get_dot_product(
            double x1,
            double y1,
            double z1,
            double x2,
            double y2,
            double z2,
            double expected)
        {
            // Arrange
            var vec1 = new Vertex(x1, y1, z1);
            var vec2 = new Vertex(x2, y2, z2);

            // Act
            var result = vec1.Dot(vec2);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
