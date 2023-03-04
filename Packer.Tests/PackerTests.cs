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

            string output = packer.Pack("example_input");

            Assert.AreEqual(correctResult, output);
        }
    }
}
