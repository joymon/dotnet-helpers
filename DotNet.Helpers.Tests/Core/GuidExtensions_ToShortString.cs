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
    }
}
