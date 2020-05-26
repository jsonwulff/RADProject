using NUnit.Framework;
using RADProject;

namespace RADProjectTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.IsTrue(Stream.HelloWorld() == "Hello World!");
        }
    }
}