using System;
using System.IO;
using System.Reflection;
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

        [TearDown]
        public void Teardown()
        {
            // remove all images created during the tests
            string currentDirectory = TestContext.CurrentContext.TestDirectory; //Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var files = Directory.GetFiles(currentDirectory, "*.png", SearchOption.TopDirectoryOnly);

            foreach(var file in files)
            {
                File.Delete(file);
            }
        }

        [Test]
        public void Test_project_is_using_nUnit_correctly()
        {
            Assert.True(true);
        }

        [Test]
        public void TestRunner_can_run_main_method()
        {
            //var rayTracer = new RayTracer(100, 100);
            RayTracer.Main(new string[0]);
        }

        [Test]
        public void Can_save_ppm_as_png()
        {
            // Arrange

            // use a string builder to build a 2d image of a gradient

            var nx = 200;
            var ny = 100;

            var rayTracer = new RayTracer(nx, ny);

            for (var j = ny - 1; j >= 0; j--)
            {
                for (var i = 0; i < nx; i++)
                {

                    var col = new Vec3(
                        (float)i / (float)nx,
                        (float)j / (float)ny,
                        51);

                    var ir = Math.Round(255.99 * col.X);
                    var ig = Math.Round(255.99 * col.Y);

                    #pragma warning disable CS0618 // Type or member is obsolete
                    rayTracer.AddToImageBuffer($"{ir} {ig} {col.Z}");
                    #pragma warning restore CS0618 // Type or member is obsolete
                }
            }

            // Act
            var filename = "test_ppm_as_png.png";
            rayTracer.SaveImageFromPPM(filename);

            // Assert
            Assert.That(VerifyImage("can_save_ppm_as_png_expected_output.png", filename), Is.True);
        }

        [Test]
        public void Can_Draw_Background()
        {
            // Arrange
            var rayTracer = new RayTracer(200, 100);

            rayTracer.DrawBackground();

            var fileName = "Can_Draw_Background.png";
            rayTracer.SaveFrameBufferToDisk(TestContext.CurrentContext.WorkDirectory+"/"+fileName);

            // Assert
            Assert.That(VerifyImage(fileName, fileName), Is.True);
        }

        [Test]
        public void can_detect_sphere()
        {
            // Arrange

            // Act

            //var result = RayTracer.isHitSphere();

            // Assert
            //Assert.That(result, Is.True);
        }

        /// <summary>
        /// Compares 2 images and returns true if they are the same
        /// </summary>
        /// <returns><c>true</c>, if image was verifyed, <c>false</c> otherwise.</returns>
        /// <param name="expectedFileName">Expected file name.</param>
        /// <param name="resultFileName">Result file name.</param>
        private bool VerifyImage(string expectedFileName, string resultFileName)
        {
            var dir = TestContext.CurrentContext.TestDirectory;
            MagickImage expectedImage;
            using (var expectedOutput = new FileStream($"{dir}../../../../Resources/{expectedFileName}", FileMode.Open))
            {
                expectedImage = new MagickImage(expectedOutput);
            }


            MagickImage resultingImage;
            using (var resultAsStream = new FileStream($"{TestContext.CurrentContext.WorkDirectory}/{resultFileName}", FileMode.Open))
            {
                resultingImage = new MagickImage(resultAsStream);
            }

            return Math.Abs(resultingImage.Compare(expectedImage, ErrorMetric.Absolute)) < 0.1;
        }
    }
}
