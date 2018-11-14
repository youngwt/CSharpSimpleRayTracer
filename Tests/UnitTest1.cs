using System;
using CSharpSimpleRayTracer;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void Test1()
        {
            Assert.True(true);
        }

        [Test]
        public void Test2()
        {
            Assert.False(false);
        }

        [Test]
        public void Test3()
        {
            Program.Main(new string[0]);
        }
    }
}
