using NUnit.Framework;
using creator;
using System.Text.RegularExpressions;

namespace creatorTests
{

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Valid_Lowercase_Pattern()
        {
            var c = Creator.ValidatePattern("lllllll");
            Assert.IsTrue(c);
        }

        [Test]
        public void Test_Valid_Number_Pattern()
        {
            var c = Creator.ValidatePattern("nnnnnn");
            Assert.IsTrue(c);
        }

        [Test]
        public void Test_Valid_Special_Pattern()
        {
            var c = Creator.ValidatePattern("sssss");
            Assert.IsTrue(c);
        }

        [Test]
        public void Test_Valid_Any_Pattern()
        {
            var c = Creator.ValidatePattern("****");
            Assert.IsTrue(c);
        }

        [Test]
        public void Test_Valid_Escape_Pattern()
        {
            var c = Creator.ValidatePattern(@"\d\\\y");
            Assert.IsTrue(c);
        }

        [Test]
        public void Test_Invalid_Escape_Pattern()
        {
            var c = Creator.ValidatePattern(@"\d\\\y\");
            Assert.IsFalse(c);
        }

        [Test]
        public void Test_Valid_Pattern()
        {
            var c = Creator.ValidatePattern(@"lLL\gnn**slL");
            Assert.IsTrue(c);
        }


        [Test]
        public void Test_Invalid_Pattern()
        {
            var c = Creator.ValidatePattern("xFgderggfsl");
            Assert.IsFalse(c);
        }
    }
}