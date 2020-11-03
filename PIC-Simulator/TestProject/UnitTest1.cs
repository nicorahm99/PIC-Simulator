using NUnit.Framework;
using PIC_Simulator;

namespace TestProject
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
            int val = Memory.testReturnNum(1);
            Assert.AreEqual(val, 1);
        }
    }
}