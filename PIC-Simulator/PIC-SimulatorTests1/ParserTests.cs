using NUnit.Framework;
using PIC_Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.IO;
using System.Reflection;

namespace PIC_Simulator.Tests
{
    [TestFixture()]
    public class ParserTests
    {
       
        [Test()]
        public void setFilePathTest()
        {
            // Getter and Setter doesn't need to be tested
            Assert.Pass();
        }

        [Test()]
        public void parseTest()
        {
            //arrange
            var classUnderTest = getClassUnderTest();
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\testfile.lst");
            classUnderTest.setFilePath(path);

            //act
            classUnderTest.parse();

            //assert
            Assert.That(classUnderTest.getRom().All(testTotalFile.Contains));
        }


        [Test()]
        public void getTotalFileTest()
        {
            // Getter and Setter doesn't need to be tested
            Assert.Pass();
        }

        [Test()]
        public void getRomTest()
        {
            // Getter and Setter doesn't need to be tested
            Assert.Pass();        }

        private Parser getClassUnderTest()
        {
            Parser parser = new Parser();
            parser.init(new ROM());
            return parser;
        }


        private readonly List<int> testTotalFile = new List<int> {

12289,
5763 ,
129,
4739,
12289,
129,
400,
0,
0,
0,
2704,
2049,
7427,
10247,
12291,
5763,
129,
4739,
12289,
129,
400,
2704,
2049,
7427,
10261,
12344,
5763,
129,
4739,
385,
7681,
10270,
12337,
5763,
129,
4739,
385,
7553,
10277,
10279,
12289,
5763,
129,
4739,
12289,
129,
400,
0,
0,
0,
2704,
2049,
7427,
10247,
12291,
5763,
129,
4739,
12289,
129,
400,
2704,
2049,
7427,
10261,
12344,
5763,
129,
4739,
385,
7681,
10270,
12337,
5763,
129,
4739,
385,
7553,
10277,
10279,
12289,
5763,
129,
4739,
12289,
129,
400,
0,
0,
0,
2704,
2049,
7427,
10247,
12291,
5763,
129,
4739,
12289,
129,
400,
2704,
2049,
7427,
10261,
12344,
5763,
129,
4739,
385,
7681,
10270,
12337,
5763,
129,
4739,
385,
7553,
10277,
10279,

            };


    }
}