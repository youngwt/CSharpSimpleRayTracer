using System;
using System.IO;
using System.Text;
using ImageMagick;

namespace CSharpSimpleRayTracer
{
    public class RayTracer
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public void SaveImageFromPPM(string imageAsPPM, string name) {

            MagickImage image;

            using(var stream = new MemoryStream())
            {
                var writer = new StreamWriter(stream);
                writer.Write(imageAsPPM);
                writer.Flush();
                stream.Position = 0;

                image = new MagickImage(stream);
            }

            image.Write(name);
        }
    }
}
