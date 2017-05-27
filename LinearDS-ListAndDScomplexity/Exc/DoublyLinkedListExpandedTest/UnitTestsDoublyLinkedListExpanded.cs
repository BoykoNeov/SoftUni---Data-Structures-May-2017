using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoublyLinkedListExpanded.Tests
{
    using System.Collections;
    using System.Collections.Generic;

    [TestClass]
    public class UnitTestsDoublyLinkedListExpanded
    {
        [TestMethod]
        public void DLLe_Access_And_Change_DLLe_elemenets_like_array()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<string>();

            // Act
            list.AddFirst("10");
            list.AddFirst("20");
            list.AddFirst("30");

            // Assert
            Assert.AreEqual("10", list[2]);
            Assert.AreEqual("30", list[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void DLLe_Access_Like_Array_Beyond_Count_ShouldThrowException()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<int>();

            // Act
            var element = list[0];
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void DLLe_Access_Like_Array_Before_Zero_ShouldThrowException()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<int>();

            // Act
            list.AddFirst(5);
            var element = list[-1];
        }

        [TestMethod]
        public void DLLe_Insert_Element_Inside()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<int>();

            // Act
            list.AddFirst(0);
            list.InsertAt(1, 0);
            list.InsertAt(2, 1);

            // Assert
            var items = new List<int>();
            list.ForEach(items.Add);
            CollectionAssert.AreEqual(items, new List<int>() { 1, 2, 0 });
        }

        [TestMethod]
        public void DLLe_AddFirst_EmptyList_ShouldAddElement()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<int>();

            // Act
            list.AddFirst(5);

            // Assert
            Assert.AreEqual(1, list.Count);

            var items = new List<int>();
            list.ForEach(items.Add);
            CollectionAssert.AreEqual(items, new List<int>() { 5 });
        }

        [TestMethod]
        public void DLLe_AddFirst_SeveralElements_ShouldAddElementsCorrectly()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<int>();

            // Act
            list.AddFirst(10);
            list.AddFirst(5);
            list.AddFirst(3);

            // Assert
            Assert.AreEqual(3, list.Count);

            var items = new List<int>();
            list.ForEach(items.Add);
            CollectionAssert.AreEqual(items, new List<int>() { 3, 5, 10 });
        }

        [TestMethod]
        public void DLLe_AddLast_EmptyList_ShouldAddElement()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<int>();

            // Act
            list.AddLast(5);

            // Assert
            Assert.AreEqual(1, list.Count);

            var items = new List<int>();
            list.ForEach(items.Add);
            CollectionAssert.AreEqual(items, new List<int>() { 5 });
        }

        [TestMethod]
        public void DLLe_AddLast_SeveralElements_ShouldAddElementsCorrectly()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<int>();

            // Act
            list.AddLast(5);
            list.AddLast(10);
            list.AddLast(15);

            // Assert
            Assert.AreEqual(3, list.Count);

            var items = new List<int>();
            list.ForEach(items.Add);
            CollectionAssert.AreEqual(items, new List<int>() { 5, 10, 15 });
        }

        [TestMethod]
        public void DLLe_RemoveFirst_OneElement_ShouldMakeListEmpty()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<int>();
            list.AddLast(5);

            // Act
            var element = list.RemoveFirst();

            // Assert
            Assert.AreEqual(5, element);
            Assert.AreEqual(0, list.Count);

            var items = new List<int>();
            list.ForEach(items.Add);
            CollectionAssert.AreEqual(items, new List<int>() { });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DLLe_RemoveFirst_EmptyList_ShouldThrowException()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<int>();

            // Act
            var element = list.RemoveFirst();
        }

        [TestMethod]
        public void DLLe_RemoveFirst_SeveralElements_ShouldRemoveElementsCorrectly()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<int>();
            list.AddLast(5);
            list.AddLast(6);
            list.AddLast(7);

            // Act
            var element = list.RemoveFirst();

            // Assert
            Assert.AreEqual(5, element);
            Assert.AreEqual(2, list.Count);

            var items = new List<int>();
            list.ForEach(items.Add);
            CollectionAssert.AreEqual(items, new List<int>() { 6, 7 });
        }

        [TestMethod]
        public void DLLe_RemoveLast_OneElement_ShouldMakeListEmpty()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<int>();
            list.AddFirst(5);

            // Act
            var element = list.RemoveLast();

            // Assert
            Assert.AreEqual(5, element);
            Assert.AreEqual(0, list.Count);

            var items = new List<int>();
            list.ForEach(items.Add);
            CollectionAssert.AreEqual(items, new List<int>() { });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DLLe_RemoveLast_EmptyList_ShouldThrowException()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<int>();

            // Act
            var element = list.RemoveLast();
        }

        [TestMethod]
        public void DLLe_RemoveLast_SeveralElements_ShouldRemoveElementsCorrectly()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<int>();
            list.AddFirst(10);
            list.AddFirst(9);
            list.AddFirst(8);

            // Act
            var element = list.RemoveLast();

            // Assert
            Assert.AreEqual(10, element);
            Assert.AreEqual(2, list.Count);

            var items = new List<int>();
            list.ForEach(items.Add);
            CollectionAssert.AreEqual(items, new List<int>() { 8, 9 });
        }

        [TestMethod]
        public void DLLe_ForEach_EmptyList_ShouldEnumerateElementsCorrectly()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<int>();

            // Act
            var items = new List<int>();
            list.ForEach(items.Add);

            // Assert
            CollectionAssert.AreEqual(items, new List<int>() { });
        }

        [TestMethod]
        public void DLLe_ForEach_SingleElement_ShouldEnumerateElementsCorrectly()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<int>();
            list.AddLast(5);

            // Act
            var items = new List<int>();
            list.ForEach(items.Add);

            // Assert
            CollectionAssert.AreEqual(items, new List<int>() { 5 });
        }

        [TestMethod]
        public void DLLe_ForEach_MultipleElements_ShouldEnumerateElementsCorrectly()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<string>();
            list.AddLast("Five");
            list.AddLast("Six");
            list.AddLast("Seven");

            // Act
            var items = new List<string>();
            list.ForEach(items.Add);

            // Assert
            CollectionAssert.AreEqual(items,
                new List<string>() { "Five", "Six", "Seven" });
        }

        [TestMethod]
        public void DLLe_IEnumerable_Foreach_MultipleElements()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<string>();
            list.AddLast("Five");
            list.AddLast("Six");
            list.AddLast("Seven");

            // Act
            var items = new List<string>();
            foreach (var element in list)
            {
                items.Add(element);
            }

            // Assert
            CollectionAssert.AreEqual(items,
                new List<string>() { "Five", "Six", "Seven" });
        }

        [TestMethod]
        public void DLLe_IEnumerable_NonGeneric_MultipleElements()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<object>();
            list.AddLast("Five");
            list.AddLast(6);
            list.AddLast(7.77);

            // Act
            var enumerator = ((IEnumerable)list).GetEnumerator();
            var items = new List<object>();
            while (enumerator.MoveNext())
            {
                items.Add(enumerator.Current);
            }

            // Assert
            CollectionAssert.AreEqual(items, new List<object>() { "Five", 6, 7.77 });
        }

        [TestMethod]
        public void DLLe_ToArray_EmptyList_ShouldReturnEmptyArray()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<string>();

            // Act
            var arr = list.ToArray();

            // Assert
            CollectionAssert.AreEqual(arr, new List<string>() { });
        }

        [TestMethod]
        public void DLLe_ToArray_NonEmptyList_ShouldReturnArray()
        {
            // Arrange
            var list = new DoublyLinkedListExpanded<string>();
            list.AddLast("Five");
            list.AddLast("Six");
            list.AddLast("Seven");

            // Act
            var arr = list.ToArray();

            // Assert
            CollectionAssert.AreEqual(arr,
                new string[] { "Five", "Six", "Seven" });
        }
    }
}