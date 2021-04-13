using NUnit.Framework;
using PIC_Simulator;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Tests
{
    [TestFixture()]
    public class ROMTests
    {

        [Test()]
        public void fetchCommandTest()
        {
            //Arrange
            ROM testRom = new ROM();
            int VALIDADRESS = 0x83;
            //Act
            int methodResult = testRom.fetchCommand(VALIDADRESS);
            //Assert
            Assert.That(0.Equals(methodResult));
        }

        [Test()]
        public void setRomTest()
        {
            //Arrange
            ROM testRom = new ROM();
            int VALIDADRESS = 0x83;
            List<int> testROMList = new List<int>();
            for (int i = 0; i<1024; i++)
            {
                testROMList.Add(i);
            }
            //Act
            testRom.setRom(testROMList);
            //Assert
            Assert.That(VALIDADRESS.Equals(testRom.fetchCommand(VALIDADRESS)));
        }
    }
}