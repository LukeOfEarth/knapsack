using com.mobiquity.packer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packer.Tests
{
    [TestClass]
    public class PackerTests
    {
        private readonly com.mobiquity.packer.Packer packer;

        public PackerTests()
        {
            packer = new com.mobiquity.packer.Packer();
        }

        [TestMethod]
        public void Packer_PacksCorrectlyTest()
        {
            string correctResult = "4\r\n-\r\n2,7\r\n8,9";

            using (FileStream fs = File.Create("example_input"))
            {
                Byte[] content = new UTF8Encoding(true).GetBytes("81 : (1,53.38,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)\r\n8 : (1,15.3,€34)\r\n75 : (1,85.31,€29) (2,14.55,€74) (3,3.98,€16) (4,26.24,€55) (5,63.69,€52) (6,76.25,€75) (7,60.02,€74) (8,93.18,€35) (9,89.95,€78)\r\n56 : (1,90.72,€13) (2,33.80,€40) (3,43.15,€10) (4,37.97,€16) (5,46.81,€36) (6,48.77,€79) (7,81.80,€45) (8,19.36,€79) (9,6.76,€64)");
                fs.Write(content, 0, content.Length);
            }

            string output = packer.Pack("example_input");

            Assert.AreEqual(correctResult, output);
        }
    }
}
