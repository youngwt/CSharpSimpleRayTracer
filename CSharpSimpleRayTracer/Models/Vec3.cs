using System;
namespace CSharpSimpleRayTracer.Models
{
    public class Vec3 
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        // Colours
        public double R
        {
            get
            {
                return X;
            }
            set
            {
                X = value;
            }
        }

        public double G
        {
            get
            {
                return Y;
            }
            set
            {
                Y = value;
            }
        }

        public double B
        {
            get
            {
                return Z;
            }
            set
            {
                Z = value;
            }
        }

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
        /// Returns the dot product of the 2 vectors
        /// </summary>
        /// <param name="a">The first vector</param>
        /// <param name="b">The second vector</param>
        /// <returns>The dot product of 2 vectors</returns>
        public static double Dot(Vec3 a, Vec3 b)
        {
            return (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
        }

        /// <summary>
        /// Adds 2 vectors and returns the result
        /// </summary>
        public static Vec3 Add(Vec3 a, Vec3 b)
        {
            var x = a.X + b.X;
            var y = a.Y + b.Y;
            var z = a.Z + b.Z;

            return new Vec3(x, y, z);
        }

        /// <summary>
        /// Gets the cross produce of 2 vectors
        /// </summary>
        public static Vec3 Cross(Vec3 a, Vec3 b)
        {
            return new Vec3(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.X,
                a.X * b.Y - (a.Y * b.X)
            );
        }

        /// <summary>
        /// Subtracts the other vector from the current one
        /// </summary>
        /// <param name="other">The vector to subtract</param>
        public static Vec3 Subtract(Vec3 a, Vec3 b)
        {
            var x = a.X - b.X;
            var y = a.Y - b.Y;
            var z = a.Z - b.Z;

            return new Vec3(x, y, z);
        }

        /// <summary>
        /// Multiplies the current vector by the other one
        /// </summary>
        /// <param name="b">The other vector</param>
        public static Vec3 Multiply(Vec3 a, Vec3 b)
        {
            var x = a.X * b.X;
            var y = a.Y * b.Y;
            var z = a.Z * b.Z;

            return new Vec3(x, y, z);
        }

        /// <summary>
        /// Multiplies the current vector by the other one
        /// </summary>
        /// <param name="b">The other vector</param>
        public static Vec3 Divide(Vec3 a, Vec3 b)
        {
            if(b.X == 0 || b.Y == 0 | b.Z == 0)
            {
                throw new ArgumentException("Cannot divide by 0");
            }

            var x = a.X / b.X;
            var y = a.Y / b.Y;
            var z = a.Z /= b.Z;

            return new Vec3(x, y, z);
        }

        public Vec3 Scale(double scale_factor)
        {
            var x = X * scale_factor;
            var y = Y * scale_factor;
            var z = Z * scale_factor;

            return new Vec3(x, y, z);
        }
    }
}
