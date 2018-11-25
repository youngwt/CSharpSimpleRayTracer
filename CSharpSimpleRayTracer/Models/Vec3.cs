using System;
namespace CSharpSimpleRayTracer.Models
{
    public abstract class Vec3
    {

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vec3()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Vec3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Returns the magnitude of the vector
        /// </summary>
        /// <returns>The length of the vector</returns>
        public double Length()
        {
            return Math.Sqrt(LengthSquared());
        }

        /// <summary>
        /// The square of the vector's length
        /// </summary>
        /// <returns>The square of the length</returns>
        public double LengthSquared()
        {
            return X * X + Y * Y + Z * Z;
        }

        /// <summary>
        /// Scales all dimensions so that the length equals 1
        /// </summary>
        public void Normalise()
        {
            var m = this.Length();

            if(m > 0f)
            {
                X = X / m;
                Y = Y / m;
                Z = Z / m;
            }
        }

        /// <summary>
        /// Gets the cross produce of 2 vectors
        /// </summary>
        /// <param name="other">Other.</param>
        public Vertex Cross(Vec3 other)
        {
            return new Vertex(
                Y * other.Z - Z * other.Y,
                Z * other.X - X * other.X,
                X * other.Y - (Y * other.X)
            );
        }

        /// <summary>
        /// Returns the dot product
        /// </summary>
        /// <returns>The dot product</returns>
        /// <param name="other">Other.</param>
        public double Dot(Vec3 other)
        {
            return (X * other.X) + (Y * other.Y) + (Z * other.Z);
        }
    }
}
