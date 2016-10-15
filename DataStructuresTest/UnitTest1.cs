using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures;

namespace DataStructuresTest
{
    [TestClass]
    public class LinkedListTests
    {

        [TestMethod]
        public void AddToEnd_AddsNodeToLastPositionInList()
        {
            // Arrange
            CustomLinkedList<string> testlist = new CustomLinkedList<string>();

            // Act
            testlist.Add("1st");
            testlist.Add("2nd"); 
            testlist.Add("3rd");


            // Assert
            Assert.AreEqual("1st", testlist.Find(x => x == "1st"));
            Assert.AreEqual("2nd", testlist.Find(x => x == "2nd"));
            Assert.AreEqual("3rd", testlist.Find(x => x == "3rd"));
        } 

        [TestMethod]
        public void Remove_RemovesCorrectNodeAndConnectsPreviousToNext()
        {
            // Arrange
            CustomLinkedList<string> testlist = new CustomLinkedList<string>();
            testlist.Add("1st");
            testlist.Add("2nd");
            testlist.Add("3rd");

            // Act
            testlist.Remove(x => x == "2nd");

            // Assert
            Assert.AreEqual("1st", testlist.Find(x => x == "1st"));
            Assert.AreEqual(null, testlist.Find(x => x == "2nd"));
            Assert.AreEqual("3rd", testlist.Find(x => x == "3rd"));
        } 
    }
}
