using System;
using System.Drawing;
using System.IO;
using System.Text;
using CSharpSimpleRayTracer.Models;
using ImageMagick;

namespace CSharpSimpleRayTracer
{
    /// <summary>
    /// Main Methods for the Ray Tracer
    /// </summary>
    public class RayTracer
    {
        /// <summary>
        /// Width of the final image
        /// </summary>
        private int Width { get; }

        /// <summary>
        /// Height of the final image
        /// </summary>
        private int Height { get; }

        private Bitmap FrameBuffer { get; }

        /// <summary>
        /// All our pixel values are added one by one in a string builder
        /// </summary>
        private StringBuilder _imageBufferAsPPMString;

        public RayTracer(int x, int y)
        {
            _imageBufferAsPPMString = new StringBuilder();

            // set pixel type
            _imageBufferAsPPMString.AppendLine("P3");

            // set dimensions
            _imageBufferAsPPMString.AppendLine($"{x} {y}");
            Width = x;
            Height = y;

            // set the maximum value
            _imageBufferAsPPMString.AppendLine("255");

            FrameBuffer = new Bitmap(x, y);
        }


        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// Adds a string representation of a pixel to the frame buffer
        /// </summary>
        /// <param name="pixel"></param>
        public void AddToImageBuffer(string pixel)
        {
            _imageBufferAsPPMString.AppendLine(pixel);
        }

        /// <summary>
        /// This method converts the string frame buffer to a PNG file
        /// and saves the output to disk
        /// </summary>
        /// <param name="name">The file name</param>
        public void SaveImageFromPPM(string name) {

            MagickImage image;

            using(var stream = new MemoryStream())
            {
                var writer = new StreamWriter(stream);
                writer.Write(_imageBufferAsPPMString);
                writer.Flush();
                stream.Position = 0;

                image = new MagickImage(stream);
            }

            image.Write(name);
        }

        /// <summary>
        /// Saves the frame buffer as an image to disk
        /// </summary>
        /// <param name="filename"></param>
        public void SaveFrameBufferToDisk(string path)
        {
             FrameBuffer.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }

        /// <summary>
        /// Fills the background with a sky like pattern
        /// </summary>
        public void DrawBackground()
        {
            var lowerBound = new Vec3(-2, -1, -1);
            var dx = new Vec3(4, 0, 0);
            var dy = new Vec3(0, 2, 0);
            var origin = new Vec3(0, 0, 0);

            // Act

            for (int j = Height - 1; j >= 0; j--)
            {
                for (int i = 0; i < Width; i++)
                {
                    var u = (double)i / (double) Width;
                    var v = (double)j / (double) Height;

                    var currentPoint = lowerBound.Add(dx.Scale(u).Add(dy.Scale(v)));
                    var ray = new Ray(origin, currentPoint);

                    var colour = ColourSkyByRay(ray);
                    var r = (int)(colour.X * 255);
                    var g = (int)(colour.Y * 255);
                    var b = (int)(colour.Z * 255);

                    this.AddToImageBuffer($"{r} {g} {b}");

                    var colour2 = Color.FromArgb(255, r, g, b);

                    FrameBuffer.SetPixel(Width-i-1, Height-j-1, colour2);
                }
            }            
        }

        /// <summary>
        /// paints a sky like blue gradient based on the ray's angle
        /// </summary>
        /// <param name="r"></param>
        /// <returns>A colour value</returns>
        private Vec3 ColourSkyByRay(Ray r)
        {
            r.Direction().Normalise();
            var t = 0.5 * r.Direction().Y + 1;

            return new Vec3(1, 1, 1).Scale(1 - t).Add(new Vec3(0.5, 0.7, 1.0).Scale(t));
        }
    }
}
