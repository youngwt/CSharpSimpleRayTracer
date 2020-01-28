using System;
using System.Drawing;

namespace CSharpSimpleRayTracer.Models
{
    /// <summary>
    /// Class to represent the frame buffer that will be worked on by the raytracer
    /// </summary>
    public class FrameModel
    {

        /// <summary>
        /// Width of the final image
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Height of the final image
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// A bitmap frame buffer
        /// </summary>
        public Bitmap FrameBuffer { get; }

        /// <summary>
        /// The lower left corner of the frame in UV space
        /// </summary>
        public Vec3 LowerBound { get; }

        /// <summary>
        /// The unit in UV space representing 1 pixel width
        /// </summary>
        public Vec3 dx { get; }

        /// <summary>
        /// The unit in UV space representing 1 pixel height
        /// </summary>
        public Vec3 dy { get; }

        public FrameModel(int width, int height)
        {
            Width = width;
            Height = height;

            // for now scale the UV space to be 100th of the resolution
            LowerBound = new Vec3(Width * -0.01, Height * -0.01, -1);
            dx = new Vec3(Math.Abs(LowerBound.X) * 2, 0, 0);
            dy = new Vec3(0, Math.Abs(LowerBound.Y) * 2, 0);

            FrameBuffer = new Bitmap(Width, Height);
        }

        /// <summary>
        /// Saves the frame buffer as an image to disk
        /// </summary>
        /// <param name="filename"></param>
        public void SaveFrameBufferToDisk(string path)
        {
            FrameBuffer.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
