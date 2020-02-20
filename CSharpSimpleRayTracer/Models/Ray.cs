using System;
namespace CSharpSimpleRayTracer.Models
{
    public class Ray
    {

        public Vec3 A { get; set; }
        public Vec3 B { get; set; }

        public Ray(Vec3 a, Vec3 b)
        {
            A = a;
            B = b;
        }

        public Vec3 Origin(){
            return A;
        }

        public Vec3 Direction()
        {
            return B;
        }

        public Vec3 PointAtParameter(double t)
        {
            return A + B.Scale(t);
        }

    }
}
