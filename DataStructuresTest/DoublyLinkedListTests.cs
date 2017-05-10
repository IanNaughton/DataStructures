using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures;

namespace DataStructuresTest
{
    [TestClass]
    public class DoublyLinkedListTests
    {

        [TestMethod]
        public void Add_AddsNodeToLastPositionInList()
        {
            // Arrange
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
            testList.Add("1st");
            testList.Add("2nd");
 
            // Act
            testList.Add("3rd");

            // Assert
            Assert.AreEqual("1st 2nd 3rd", testList.ToString());
        }

        [TestMethod]
        public void Find_ReturnsFirstOccuranceOfValueInList()
        {
            // Arrange
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
            testList.Add("1st Potato");
            testList.Add("2nd Potato");

            // Act
            testList.Add("3rd Potato");

            // Assert
            Assert.AreEqual("1st Potato", testList.Find(item => item.Contains("Potato")));
        }

        [TestMethod]
        public void ForEach_IteratesOverList()
        {
            // Arrang
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
            testList.Add("1st");
            testList.Add("2nd");
            testList.Add("3rd");

            // Act
            string actual = "";
            foreach (string item in testList)
            {
                actual = actual + " " + item;
            }

            // Assert
            Assert.AreEqual("1st 2nd 3rd", actual.TrimStart());
        }

        [TestMethod]
        public void Remove_RemovesCorrectNodeAndConnectsPreviousToNext()
        {
            // Arrange
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
            testList.Add("1st");
            testList.Add("2nd");
            testList.Add("3rd");

            // Act
            testList.Remove(x => x == "2nd");

            // Assert
            Assert.AreEqual("1st", testList.Find(x => x == "1st"));
            Assert.AreEqual(null, testList.Find(x => x == "2nd"));
            Assert.AreEqual("3rd", testList.Find(x => x == "3rd"));
        }
    }
}
