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

        public Sphere(Vec3 origin, double radius, Vec3 colour)
        {
            Origin = origin;
            Radius = radius;
            Colour = colour;
        }

        public bool IsHit(Ray ray)
        {
            var oc = Vec3.Subtract(ray.Origin(), Origin);
            double a = Vec3.Dot(ray.Direction(), ray.Direction());
            double b = 2.0d * Vec3.Dot(oc, ray.Direction());
            double c = Vec3.Dot(oc, oc) - RSquared;

            double discriminant = b * b - (4 * a * c);

            return discriminant > 0;
        }
    }
}
