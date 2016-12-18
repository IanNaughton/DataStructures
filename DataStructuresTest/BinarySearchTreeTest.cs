using BinarySearchTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresTest
{
    [TestClass]
    public class BinarySearchTreeTest
    {
        List<int> _testNumbers;
        List<int> _inOrderTestNumbers;
        List<int> _preOrderTestNumbers;
        List<int> _postOrderTestNumbers;

        /// <summary>
        /// An argument can be made that these are bad tests, and 
        /// that I should be passing in a mock root to check what 
        /// is actually inserted. I decided not to allow this because
        /// I believe that this creates an inconsistent level of 
        /// abstraction. I don't feel that consumers of this class should
        /// be aware of the node class--even if only for injecting
        /// dependencies into the constructor.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            // We are building this tree!
            //
            //           40
            //        /      \
            //       30      60
            //      /  \    /  \
            //     25  35  45   75
            //    /            /  \  
            //   20           70   80
            //  /               \
            // 15               73
            //                 /
            //                72  

            _testNumbers = new List<int> { 40, 30, 60, 25, 35, 45, 75, 20, 70, 80, 15, 73, 72 };
            _inOrderTestNumbers = new List<int> { 15, 20, 25, 30, 35, 40, 45, 60, 70, 72, 73, 75, 80 };
            _preOrderTestNumbers = new List<int> { 40, 30, 25, 20, 15, 35, 60, 45, 75, 70, 73, 72, 80 };
            _postOrderTestNumbers = new List<int> { 15, 20, 25, 35, 30, 45, 72, 73, 70, 80, 75, 60, 40 };
        }

       
        [TestMethod]
        public void Tree_NodesAreRetrievedInOrder()
        {
            // Arrange
           
            Tree<int> testTree = new Tree<int>();
            _testNumbers.ForEach(number => testTree.AddNode(number));

            // Act

            List<int> inorderList = testTree.TraverseInOrder();

            // Assert
          
            Assert.IsNotNull(inorderList);
            Assert.IsTrue(inorderList.Count > 0);
            
            for (int index = 0; index < _inOrderTestNumbers.Count -1; index++)
            {
                Assert.AreEqual<int>(_inOrderTestNumbers[index], inorderList[index], $"Expected numbers: {string.Join(", ", _inOrderTestNumbers)} Actual numbers: {string.Join(", ", inorderList)})"); 
            }
        }

        [TestMethod]
        public void Tree_NodesAreRetrievedPreOrder()
        {
            // Arrange

            Tree<int> testTree = new Tree<int>();
            _testNumbers.ForEach(number => testTree.AddNode(number));

            // Act

            List<int> preOrderList = testTree.TraversePreOrder();

            // Assert

            Assert.IsNotNull(preOrderList);
            Assert.IsTrue(preOrderList.Count > 0);

            for (int index = 0; index < _preOrderTestNumbers.Count - 1; index++)
            {
                Assert.AreEqual<int>(_preOrderTestNumbers[index], preOrderList[index], $"Expected numbers: {string.Join(", ", _preOrderTestNumbers)} Actual numbers: {string.Join(", ", preOrderList)})");
            }
        }

        [TestMethod]
        public void Tree_NodesAreRetrievedPostOrder()
        {
            // Arrange

            Tree<int> testTree = new Tree<int>();
            _testNumbers.ForEach(number => testTree.AddNode(number));

            // Act

            List<int> postOrderList = testTree.TraversePostOrder();

            // Assert

            Assert.IsNotNull(postOrderList);
            Assert.IsTrue(postOrderList.Count > 0);

            for (int index = 0; index < _postOrderTestNumbers.Count - 1; index++)
            {
                Assert.AreEqual<int>(_postOrderTestNumbers[index], postOrderList[index], $"Expected numbers: {string.Join(", ", _postOrderTestNumbers)} Actual numbers: {string.Join(", ", postOrderList)})");
            }
        }

        [TestMethod]
        public void Tree_GetNodeFindsCorrectNode()
        {
            // Arrange

            int testValue = 35;

            Tree<int> testTree = new Tree<int>();
            _testNumbers.ForEach(number => testTree.AddNode(number));

            // Act

            int foundNode = testTree.GetNode(testValue);

            // Assert

            Assert.AreEqual<int>(testValue, foundNode);

        }

        [TestMethod]
        public void Tree_FailingToFindNodeCausesException()
        {
            // Arrange

            int testValue = -1;

            Tree<int> testTree = new Tree<int>();
            _testNumbers.ForEach(number => testTree.AddNode(number));

            // Act

            try
            {
                int foundNode = testTree.GetNode(testValue);
            }
            catch (KeyNotFoundException ex)
            {
                // Assert

                Assert.AreEqual<string>("Node does not exists in the current tree.", ex.Message);
            }
        }

        [TestMethod]
        public void Tree_DeleteCorrectlyRemovesNodeWithTwoChildren()
        {

            // Arrange

            int testValue = 60;
            var sortedResult = new List<int> { 15, 20, 25, 30, 35, 40, 45, 70, 72, 73, 75, 80 };

            Tree<int> testTree = new Tree<int>();
            _testNumbers.ForEach(number => testTree.AddNode(number));

            // Act

            testTree.DeleteNode(testValue);
            var result = testTree.TraverseInOrder();

            // Assert

            Assert.AreEqual(string.Join(", ", result), string.Join(", ", sortedResult));
        }

        [TestMethod]
        public void Tree_GetMaxDepthReturnsMaxDepth()
        {
            // Arrange

            Tree<int> testTree = new Tree<int>();
            _testNumbers.ForEach(number => testTree.AddNode(number));

            // Act

            int depth = testTree.GetDepth();

            // Assert

            Assert.AreEqual(6, depth);

        }
    }
}
