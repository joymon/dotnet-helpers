using DotNet.Helpers.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DotNet.Helpers.Tests.Linq
{
    [TestClass]
    public class EnumerableExtensions_Chunk
    {
        [TestMethod]
        public void WhenInputHas4ElementsAndChunkSizeIs2_Return2Chunks()
        {
            IEnumerable<int> input = new List<int>() { 1, 2, 3, 4 };
            int expected1 = 3;
            int expected2 = 7;

            int actual1 = input.Chunk(2).First().Sum();
            int actual2 = input.Chunk(2).Last().Sum();
            bool result = expected1 == actual1 && expected2 == actual2;

            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void WhenInputHas4ElementsAndChunkSizeIs3_Return2Chunks()
        {
            IEnumerable<int> input = new List<int>() { 1, 2, 3, 4 };
            int expected1 = 6;
            int expected2 = 4;

            int actual1 = input.Chunk(3).First().Sum();
            int actual2 = input.Chunk(3).Last().Sum();
            bool result = expected1 == actual1 && expected2 == actual2;

            Assert.AreEqual(result, true);
        }
    }
}
