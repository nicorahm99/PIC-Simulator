using NUnit.Framework;
using PIC_Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIC_Simulator.Tests
{
    [TestFixture]
    public class StackTests
    {
        int VALID_INTEGER = 12345;

        [Test]
        public void singlePushPopTest()
        {
            var classUnderTest = getClassUnderTest();
    
            classUnderTest.push(VALID_INTEGER);

            Assert.That(classUnderTest.pop().Equals(VALID_INTEGER));
        }

        [Test]
        public void multiplePushPopTest()
        {
            var classUnderTest = getClassUnderTest();

            fillStackUntilOverflowOccurs(classUnderTest);

            Assert.That(lastElementIn(classUnderTest).Equals(8));
        }

        private object lastElementIn(Stack classUnderTest)
        {
            for (int i = 0; i <= 6; i++)
            {
                classUnderTest.pop();
            }
            return classUnderTest.pop();
        }

        private void fillStackUntilOverflowOccurs(Stack classUnderTest)
        {
            for(int i = 0; i <= 8; i++)
            {
                classUnderTest.push(i);
            }
        }

        private Stack getClassUnderTest()
        {
            return new Stack();
        }
    }
}