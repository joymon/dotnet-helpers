using DotNet.Helpers.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DotNet.Helpers.Tests.Linq
{
    [TestClass]
    public class EnumerableExtensions_Randomize
    {
        [TestMethod]
        ///<summary>
        ///Really bad test for Random. There are chances that both the times the first element will still be 1.
        ///</summary>
        public void WhenInputHas12Elements_ShouldNotReturnFirstElement2ConsecutiveTimes()
        {
            IEnumerable<int> input = new List<int>() { 1, 2, 3, 4, 5, 6,7,8,9,10,11,12 };
            int notexpected = 1;

            int actual1 = input.Randomize().FirstOrDefault();
            int actual2 = input.Randomize().FirstOrDefault();
            bool result = notexpected != actual1 || notexpected != actual2;

            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void WhenInputHas20Elements_ShouldNotReturnFirstElement2ConsecutiveTimes()
        {
            IEnumerable<int> input = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11,12,13,14,15,16,17,18,19,20 };
            int notexpected = 1;

            int actual1 = input.Randomize().FirstOrDefault();
            int actual2 = input.Randomize().FirstOrDefault();
            bool result = notexpected != actual1 || notexpected != actual2;

            Assert.AreEqual(result, true);
        }
    }
}
