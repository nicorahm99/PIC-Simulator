using Moq;
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
    public class EEPROMTests
    {
        [Test()]
        public void writeToEEPROMTest()
        {
            int AN_ADRESS = 10;
            int AN_INTEGER = 123;

            //ARRANGE
            var memoryMock = new Mock<IMemory>();
            memoryMock.Setup(p => p.getFile(0x09)).Returns(AN_ADRESS);
            memoryMock.Setup(p => p.getFile(0x08)).Returns(AN_INTEGER);
            var classUnderTest = new EEPROM();
            classUnderTest.init(memoryMock.Object);
            classUnderTest.setStateMachineTriggered();


            //ACT
            classUnderTest.writeToEEPROM();

            //ASSERT
            Assert.AreEqual(AN_INTEGER, classUnderTest.getEeprom()[AN_ADRESS]);
        }
    }
}