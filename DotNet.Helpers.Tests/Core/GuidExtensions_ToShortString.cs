using DotNet.Helpers.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DotNet.Helpers.Tests.Core
{
    [TestClass]
    public class GuidExtensions_ToShortString
    {
        [TestMethod]
        public void WhenGuidToShortStringIsCalled_ReturnShortId()
        {
            Guid id = Guid.NewGuid();
            string shortId= id.ToShortString();
            Assert.IsNotNull(shortId, $"Received value {shortId}is null");
        }
        [TestMethod]
        public void WhenGuidToShortStringIsCalled_ReturnShortIdWithLengthOf22()
        {
            Guid id = Guid.NewGuid();
            string shortId = id.ToShortString();
            Assert.AreEqual(22,shortId.Length, $"Received value {shortId} is not in correct length");
        }
    }
}
