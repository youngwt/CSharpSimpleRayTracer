using System;
using System.IO;
using System.Text;
using CSharpSimpleRayTracer;
using CSharpSimpleRayTracer.Models;
using ImageMagick;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class RayTracerTests
    {
        [Test]
        public void Test_project_is_using_nUnit_correctly()
        {
            Assert.True(true);
        }

        [Test]
        public void TestRunner_can_run_main_method()
        {
            RayTracer.Main(new string[0]);
        }

        [Test]
        public void Can_save_ppm_as_png()
        {
            // Arrange

            // use a string builder to build a 2d image of a gradient

            var nx = 200;
            var ny = 100;

            var ppmBuilder = new StringBuilder();
            ppmBuilder.AppendLine("P3"); // Use ASCII Characters
            ppmBuilder.AppendLine($"{nx} {ny}"); // Image dimensions
            ppmBuilder.AppendLine("255"); // Max value for each component colour

            for (var j = ny - 1; j >= 0; j--)
            {
                for (var i = 0; i < nx; i++)
                {

                    var col = new Vertex(
                        (float)i / (float)nx,
                        (float)j / (float)ny,
                        51);

                    var ir = Math.Round(255.99 * col.X);
                    var ig = Math.Round(255.99 * col.Y);

                    ppmBuilder.AppendLine($"{ir} {ig} {col.Z}");
                }
            }

            // get the image we expect to compare in the assert
            var dir = TestContext.CurrentContext.TestDirectory;
            MagickImage expectedImage;
            using (var expectedOutput = new FileStream($"{dir}../../../../Resources/can_save_ppm_as_png_expected_output.png", FileMode.Open))
            {
                expectedImage = new MagickImage(expectedOutput);
            }

            // Act
            var program = new RayTracer();
            var filename = "test_ppm_as_png.png";
            program.SaveImageFromPPM(ppmBuilder.ToString(), filename);

            // Assert
            // load the resulting image as a magickImage object
            MagickImage resultingImage;
            using (var resultAsStream = new FileStream($"{TestContext.CurrentContext.WorkDirectory}/{filename}", FileMode.Open))
            {
                resultingImage = new MagickImage(resultAsStream);
            }

            Assert.That(resultingImage.Compare(expectedImage, ErrorMetric.Absolute), Is.EqualTo(0d));
        }
    }
}
