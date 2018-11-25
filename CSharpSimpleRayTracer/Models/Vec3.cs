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

        /// <summary>
        /// Adds a vector to the current vector
        /// </summary>
        /// <param name="other">The additional vector</param>
        public void Add(Vec3 other)
        {
            X += other.X;
            Y += other.Y;
            Z += other.Z;
        }

        /// <summary>
        /// Subtracts the other vector from the current one
        /// </summary>
        /// <param name="other">The vector to subtract</param>
        public void Subtract(Vec3 other)
        {
            X -= other.X;
            Y -= other.Y;
            Z -= other.Z;
        }

        /// <summary>
        /// Multiplies the current vector by the other one
        /// </summary>
        /// <param name="other">The other vector</param>
        public void Multiply(Vec3 other)
        {
            X *= other.X;
            Y *= other.Y;
            Z *= other.Z;
        }

        /// <summary>
        /// Multiplies the current vector by the other one
        /// </summary>
        /// <param name="other">The other vector</param>
        public void Divide(Vec3 other)
        {
            if(other.X == 0 || other.Y == 0 | other.Z == 0)
            {
                throw new ArgumentException("Cannot divide by 0");
            }

            X /= other.X;
            Y /= other.Y;
            Z /= other.Z;
        }

        public void Scale(double scale_factor)
        {
            X *= scale_factor;
            Y *= scale_factor;
            Z *= scale_factor;
        }
    }
}
