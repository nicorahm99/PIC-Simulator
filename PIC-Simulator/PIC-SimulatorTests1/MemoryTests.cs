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
    public class MemoryTests
    {
        const int PORT_A = 0x5;
        const int STATUS = 0x3;
        const int VALID_8_BIT_NUMBER = 0xA9;
        const int TMR_0 = 0x1;

        [Test()]
        public void setFileTest()
        {
            //arrange
            var classUnderTest = getClassUnderTest();

            //act
            classUnderTest.setFile(PORT_A, 15);

            //assert
            Assert.That(classUnderTest.getFile(PORT_A).Equals(15));
        }

        [Test()]
        public void getFileTest()
        {
            //arrange
            var classUnderTest = getClassUnderTest();

            //act 
            classUnderTest.init();

            //assert
            Assert.That(classUnderTest.getFile(0x3).Equals(0x18));
        }

        [Test()]
        public void getBitTest()
        {
            //arrange
            var classUnderTest = getClassUnderTest();

            //act 
            classUnderTest.init();

            //assert
            Assert.That(classUnderTest.getBit(0x3, 3).Equals(1));
        }

        [Test()]
        public void setBitTest()
        {
            //arrange
            var classUnderTest = getClassUnderTest();

            //act
            classUnderTest.setBit(PORT_A, 5);

            //assert
            Assert.That(classUnderTest.getBit(PORT_A, 5).Equals(1));
        }

        [Test()]
        public void clearBitTest()
        {
            //arrange
            var classUnderTest = getClassUnderTest();

            //act
            classUnderTest.clearBit(STATUS, 3);

            //assert
            Assert.That(classUnderTest.getBit(STATUS, 3).Equals(0));
        }

        [Test()]
        public void getWRegTest()
        {
            //arrange
            var classUnderTest = getClassUnderTest();

            //act 
            classUnderTest.init();

            //assert
            Assert.That(classUnderTest.getWReg().Equals(0));
        }

        [Test()]
        public void setWRegTest()
        {
            //arrange
            var classUnderTest = getClassUnderTest();

            //act 
            classUnderTest.setWReg(VALID_8_BIT_NUMBER);

            //assert
            Assert.That(classUnderTest.getWReg().Equals(VALID_8_BIT_NUMBER));
        }

        [Test()]
        public void getStatusRP0Test()
        {
            //arrange
            var classUnderTest = getClassUnderTest();

            //act 
            classUnderTest.setBit(STATUS, 5);

            //assert
            Assert.That(classUnderTest.getStatusRP0().Equals(1));
        }

        [Test()]
        public void getFullPCTest()
        {
            //arrange
            var classUnderTest = getClassUnderTest();

            //act 
            classUnderTest.init();

            //assert
            Assert.That(classUnderTest.getFullPC().Equals(0));
        }

        [Test()]
        public void incPCTest()
        {
            //arrange
            var classUnderTest = getClassUnderTest();

            //act 
            classUnderTest.init();
            classUnderTest.incPC();
            classUnderTest.incPC();
            classUnderTest.incPC();
            classUnderTest.incPC();

            //assert
            Assert.That(classUnderTest.getFullPC().Equals(4));
        }

        [Test()]
        public void setFullPCTest()
        {
            //arrange
            var classUnderTest = getClassUnderTest();

            //act 
            classUnderTest.init();
            classUnderTest.setFullPC(VALID_8_BIT_NUMBER);

            //assert
            Assert.That(classUnderTest.getFullPC().Equals(VALID_8_BIT_NUMBER));
        }

        [Test()]
        public void setMemoryBankToTest()
        {
            //arrange
            var classUnderTest = getClassUnderTest();

            //act
            classUnderTest.setMemoryBankTo(1);

            //assert
            Assert.That(classUnderTest.getBit(STATUS, 5).Equals(1));
        }

        [Test()]
        public void getOptionRegisterTest()
        {
            //arrange
            var classUnderTest = getClassUnderTest();

            //act 
            classUnderTest.init();

            //assert
            Assert.That(classUnderTest.getOptionRegister().Equals(0));
        }

        [Test()]
        public void getCurrentMemoryBankTest()
        {
            //arrange
            var classUnderTest = getClassUnderTest();

            //act
            classUnderTest.setBit(STATUS, 5);

            //assert
            Assert.That(classUnderTest.getCurrentMemoryBank().Equals(1));
        }

        [Test()]
        public void requestAccessTest()
        {            
            //arrange
            var classUnderTest = getClassUnderTest();

            //act
            classUnderTest.setMemoryBankTo(1);
            classUnderTest.setBit(PORT_A, 0);
            classUnderTest.setMemoryBankTo(0);

            //assert
            Assert.That(classUnderTest.requestAccess(PORT_A,0).Equals(true));
        }

        [Test()]
        public void getTMR0Test()
        {
            //arrange
            var classUnderTest = getClassUnderTest();

            //act
            classUnderTest.setFile(TMR_0, VALID_8_BIT_NUMBER);

            //assert
            Assert.That(classUnderTest.getTMR0().Equals(VALID_8_BIT_NUMBER));
        }

        [Test()]
        public void setTMR0Test()
        {
            //arrange
            var classUnderTest = getClassUnderTest();

            //act
            classUnderTest.setTMR0(VALID_8_BIT_NUMBER);

            //assert
            Assert.That(classUnderTest.getTMR0().Equals(VALID_8_BIT_NUMBER));
        }


        private Memory getClassUnderTest()
        {
            return new Memory();
        }
    }
}