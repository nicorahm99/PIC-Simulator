using NUnit.Framework;
using PIC_Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Tests
{
    [TestFixture()]
    public class ROMTests
    {
        //[SetUp]
        //public void Init()
        //{
        //    ROM testRom = new ROM();
        //}

        [Test()]
        public void fetchCommandTest()
        {
            //Arrange
            ROM testRom = new ROM();
            //Act
            int methodResult = testRom.fetchCommand(3);
            //Assert
            Assert.AreEqual(methodResult, 0);
        }

        [Test()]
        public void setRomTest()
        {
            //Arrange
            ROM testRom = new ROM();
            List<int> testROMList = new List<int>();
            for (int i = 0; i<1024; i++)
            {
                testROMList.Add(i);
            }
            //Act
            testRom.setRom(testROMList);
            //Assert
            Assert.AreEqual(10, testRom.fetchCommand(0xA));
        }
    }
}