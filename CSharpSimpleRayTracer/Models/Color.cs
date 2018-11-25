using System;
namespace CSharpSimpleRayTracer.Models
{
    public class Color : Vec3
    {

        public double R { get { return X; } set => X = value; }
        public double G { get { return Y; } set => Y = value; }
        public double B { get { return Z; } set => Z = value; }

    }
}
