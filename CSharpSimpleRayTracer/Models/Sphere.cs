using System;
namespace CSharpSimpleRayTracer.Models
{
    /// <summary>
    /// A class to represent a sphere
    /// </summary>
    public class Sphere : I3dObject
    {
        /// <summary>
        /// The location of the sphere
        /// </summary>
        public Vec3 Origin { get; }

        /// <summary>
        /// The radius of the sphere
        /// </summary>
        public double Radius { get; }

        /// <summary>
        /// The square of the radius
        /// </summary>
        public double RSquared
        {
            get
            {
                return Radius * Radius;
            }
        }

        /// <summary>
        /// The colour of the sphere
        /// </summary>
        public Vec3 Colour { get; }

        /// <summary>
        /// Colours the pixel at a point based on the ray, current displays the normal
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public Vec3 DrawPixel(Ray r)
        {
            var N = Normal(r);

            var colour = new Vec3(N.X + 1, N.Y + 1, N.Z + 1).Scale(0.5);

            return colour;
        }

        /// <summary>
        /// Returns the normal of the current point on the sphere
        /// </summary>
        /// <param name="op">The line between the centre and the point on the surface</param>
        /// <returns>The normal of the point</returns>
        public Vec3 Normal(Ray op)
        {
            var t = IsHit(op);

            var pointAtParameter = op.PointAtParameter(t);

            var N = Vec3.Subtract(pointAtParameter, Origin);

            N.Normalise();

            return N;
        }

        public Sphere(Vec3 origin, double radius, Vec3 colour)
        {
            Origin = origin;
            Radius = radius;
            Colour = colour;
        }

        /// <summary>
        /// Determines if the ray ever hits the sphere by returing a paramter t
        /// where t is the distance across the ray to the surface of the sphere
        /// </summary>
        /// <remarks>
        /// A value of less than 0 means the ray is not hit
        /// </remarks>
        public double IsHit(Ray ray)
        {
            var discriminant = CalculateDiscriminant(ray);

            if( discriminant.discriminant < 0)
            {
                return -1.0d;
            } else
            {
                return (-discriminant.b - Math.Sqrt(discriminant.discriminant)) / 2*discriminant.a;
            }
        }

        /// <summary>
        /// Calculates the b^2 - 4ac part of the quadratic equation based on the input ray
        /// </summary>
        /// <param name="ray">The input ray</param>
        private (double a, double b, double discriminant) CalculateDiscriminant(Ray ray)
        {
            var oc = Vec3.Subtract(ray.Origin(), Origin);
            var a = Vec3.Dot(ray.Direction(), ray.Direction());
            var b = 2.0d * Vec3.Dot(oc, ray.Direction());
            var c = Vec3.Dot(oc, oc) - RSquared;

            return (a, b, (b * b - (4 * a * c)));
        }
    }
}
