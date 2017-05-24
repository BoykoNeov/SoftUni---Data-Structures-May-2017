using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LongestSubsequence.Tests
{
    [TestClass()]
    public class LongestSubsequenceTests
    {

        [TestMethod()]
        public void SingleInputShouldReturnSingleOutput()
        {
            List<int> list = new List<int>
            {
                0
            };

            list = LongestSubsequence.CalculateLongestSubsequence(list);
            Assert.AreEqual(0, list[0]);
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod()]
        public void RepeatedLongestSequenceInput()
        {
            List<int> list = new List<int>
            {
                0 , 2 , 2 , 2 , 5, 5, 5, 2, 2, 2, 12
            };

            list = LongestSubsequence.CalculateLongestSubsequence(list);

            List<int> test = new List<int>
            {
                2, 2, 2
            };

            Assert.IsTrue(list.SequenceEqual(test));
        }
    }
}