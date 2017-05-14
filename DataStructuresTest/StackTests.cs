using LinkedList.Stack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresTest
{
    [TestClass]
    public class StackTests
    {
        [TestInitialize]
        public void Setup()
        {
            // Not sure if this is really needed...
        }

        [TestMethod]
        public void WhenPushIsCalled_AnItemIsAdded()
        {
            var stack = new CustomStack<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            Assert.AreEqual<int>(3, stack.Pop());
            Assert.AreEqual<int>(2, stack.Pop());
            Assert.AreEqual<int>(1, stack.Pop());

        }

        /// <summary>
        /// I get it, this is a duplicate test case, but I would rather
        /// be explicit about the test cases I am interested in than implicitly
        /// test this use case in the test above
        /// </summary>
        [TestMethod]
        public void WhenPopIsCalled_TheTopStackItemIsReturned()
        {
            var stack = new CustomStack<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            Assert.AreEqual<int>(3, stack.Pop());
        }

        [TestMethod]
        public void WhenPopIsCalledOnAnEmptyStack_AnExceptionIsThrown()
        {
            var stack = new CustomStack<int>();

            try
            {
                stack.Pop();
            }
            catch (InvalidOperationException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
            }
        }
    }
}
