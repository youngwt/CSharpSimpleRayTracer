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

            var vertex = new Vec3(3, 4, 5);
            
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
            var vertex1 = new Vec3(x1, y1, z1);
            var vertex2 = new Vec3(x2, y2, z2);

            // Act
            var result = Vec3.Cross(vertex1, vertex2);

            // Assert
            Assert.That(result.X, Is.EqualTo(ex), "X component is wrong");
            Assert.That(result.Y, Is.EqualTo(ey), "Y component is wrong");
            Assert.That(result.Z, Is.EqualTo(ez), "Z component is wrong");

        }

        [Test]
        public void Can_normalise_a_vector()
        {
            // Arrange

            var vertex = new Vec3(3, 4, 5);

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
            var vec1 = new Vec3(x1, y1, z1);
            var vec2 = new Vec3(x2, y2, z2);

            // Act
            var result = Vec3.Dot(vec1, vec2);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Can_Add_two_vectors()
        {
            // Arrange
            var v1 = new Vec3(1, 2, 3);
            var v2 = new Vec3(4, 5, 6);

            // Act
            var result = v1 + v2;

            // Assert
            Assert.That(result.X, Is.EqualTo(5));
            Assert.That(result.Y, Is.EqualTo(7));
            Assert.That(result.Z, Is.EqualTo(9));
        }

        [Test]
        public void Can_Subtract_two_vectors()
        {
            // Arrange
            var v1 = new Vec3(1, 2, 3);
            var v2 = new Vec3(4, 5, 6);

            // Act
            var result = v1 - v2;

            // Assert
            Assert.That(result.X, Is.EqualTo(-3), "X is wrong");
            Assert.That(result.Y, Is.EqualTo(-3), "Y is wrong");
            Assert.That(result.Z, Is.EqualTo(-3), "Z is wrong");
        }

        [Test]
        public void Can_multiply_two_vectors()
        {
            // Arrange
            var v1 = new Vec3(1, 2, 3);
            var v2 = new Vec3(4, 5, 6);

            // Act
            var result = Vec3.Multiply(v1, v2);

            // Assert
            Assert.That(result.X, Is.EqualTo(4), "X is wrong");
            Assert.That(result.Y, Is.EqualTo(10), "Y is wrong");
            Assert.That(result.Z, Is.EqualTo(18), "Z is wrong");
        }

        [TestCase(0, 1, 1)]
        [TestCase(1, 0, 1)]
        [TestCase(1, 1, 0)]
        public void Will_not_divide_by_zero(
            double x,
            double y,
            double z)
        {
            // Arrange
            var v1 = new Vec3(1, 2, 3);
            var v2 = new Vec3(x, y, z);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => Vec3.Divide(v1, v2));
        }

        [Test]
        public void Can_divide_vector()
        {
            // Arrange
            var v1 = new Vec3(10, 8, 6);
            var v2 = new Vec3(2, 2, 2);

            // Act
            var result = Vec3.Divide(v1, v2);

            // Assert
            Assert.That(result.X, Is.EqualTo(5));
            Assert.That(result.Y, Is.EqualTo(4));
            Assert.That(result.Z, Is.EqualTo(3));
        }

        [TestCase(2, 2, 4, 6)]
        [TestCase(-1, -1, -2, -3)]
        [TestCase(0, 0, 0, 0)]
        public void Can_scale_vector(double scale_factor, 
                                double expectedX, 
                                double expectedY, 
                                double expectedZ)
        {
            // Arrange
            var v1 = new Vec3(1, 2, 3);

            // Act
            var result = v1.Scale(scale_factor);

            // Assert
            Assert.That(result.X, Is.EqualTo(expectedX));
            Assert.That(result.Y, Is.EqualTo(expectedY));
            Assert.That(result.Z, Is.EqualTo(expectedZ));
        }

    }
}
