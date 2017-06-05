using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class UnitTests1
{
    [TestMethod]
    public void Insert_Single_TraverseInOrder()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        bst.Insert(1);

        // Act
        List<int> nodes = new List<int>();
        bst.EachInOrder(nodes.Add);

        // Assert
        int[] expectedNodes = new int[] { 1 };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }


    [TestMethod]
    public void Insert_Multiple_TraverseInOrder()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        bst.Insert(2);
        bst.Insert(1);
        bst.Insert(3);

        // Act
        List<int> nodes = new List<int>();
        bst.EachInOrder(nodes.Add);

        // Assert
        int[] expectedNodes = new int[] { 1, 2, 3 };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }

    [TestMethod]
    public void Contains_ExistingElement_ShouldReturnTrue()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        bst.Insert(2);
        bst.Insert(1);
        bst.Insert(3);

        // Act
        bool contains = bst.Contains(1);

        // Assert
        Assert.IsTrue(contains);
    }

    [TestMethod]
    public void Contains_NonExistingElement_ShouldReturnFalse()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        bst.Insert(2);
        bst.Insert(1);
        bst.Insert(3);

        // Act
        bool contains = bst.Contains(5);

        // Assert
        Assert.IsFalse(contains);
    }

    [TestMethod]
    public void Insert_Multiple_DeleteMin_Should_Work_Correctly()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        bst.Insert(2);
        bst.Insert(1);
        bst.Insert(3);

        // Act
        bst.DeleteMin();
        List<int> nodes = new List<int>();
        bst.EachInOrder(nodes.Add);

        // Assert
        int[] expectedNodes = new int[] { 2, 3 };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }

    [TestMethod]
    public void DeleteMin_One_Element_Should_Work_Correctly()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        bst.Insert(1);

        // Act
        bst.DeleteMin();
        List<int> nodes = new List<int>();
        bst.EachInOrder(nodes.Add);

        // Assert
        int[] expectedNodes = new int[] { };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }


    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void DeleteMin_EmptyTree_ShouldThrow()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        // Act
        // Assert
        bst.DeleteMin();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void DeleteMax_EmptyTree_ShouldThrow()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        // Act
        // Assert
        bst.DeleteMax();
    }

    [TestMethod]
    public void DeleteMax_OneElement_ShouldRemoveCorrectly()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        bst.Insert(1);

        // Act
        bst.DeleteMax();
        List<int> nodes = new List<int>();
        bst.EachInOrder(nodes.Add);

        // Assert
        int[] expectedNodes = new int[] { };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }

    [TestMethod]
    public void DeleteMax_FewElements_ShouldRemoveCorrectElement()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        bst.Insert(2);
        bst.Insert(1);
        bst.Insert(3);

        // Act
        bst.DeleteMax();
        List<int> nodes = new List<int>();
        bst.EachInOrder(nodes.Add);

        // Assert
        int[] expectedNodes = new int[] { 1, 2 };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }

    [TestMethod]
    public void Count_OnFewElements_ShouldReturnCorrectCount()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        // Act
        int actualCount = bst.Count();

        // Assert
        Assert.AreEqual(10, actualCount);
    }

    [TestMethod]
    public void Count_EmptyTree_ShouldReturnCorrectly()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        // Act
        int actualCount = bst.Count();

        // Assert
        Assert.AreEqual(0, actualCount);
    }

    [TestMethod]
    public void Count_AfterDeleteMin_ShouldReturnCorrectly()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        // Act
        bst.DeleteMin();
        int actualCount = bst.Count();

        // Assert
        Assert.AreEqual(9, actualCount);
    }

    [TestMethod]
    public void Count_AfterDeleteMax_ShouldReturnCorrectly()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        // Act
        bst.DeleteMax();
        int actualCount = bst.Count();

        // Assert
        Assert.AreEqual(9, actualCount);
    }

    [TestMethod]
    public void Search_NonExistingElement_ShouldReturnEmptyTree()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        // Act
        BinarySearchTree<int> result = bst.Search(5);
        List<int> nodes = new List<int>();
        result.EachInOrder(nodes.Add);

        // Assert
        int[] expectedNodes = new int[] { };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }

    [TestMethod]
    public void Search_ExistingElement_ShouldReturnSubTree()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        // Act
        BinarySearchTree<int> result = bst.Search(5);
        List<int> nodes = new List<int>();
        result.EachInOrder(nodes.Add);

        // Assert
        int[] expectedNodes = new int[] { 1, 3, 4, 5, 8, 9 };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }

    [TestMethod]
    public void Range_ExistingElements_ShouldReturnCorrectElements()
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        // Act
        IEnumerable<int> result = bst.Range(4, 37);

        // Assert
        int[] expectedNodes = new int[] { 4, 5, 8, 9, 10, 37 };
        CollectionAssert.AreEqual(expectedNodes, result.ToArray());
    }

    [TestMethod]
    public void Range_ExistingElements_ShouldReturnCorrectCount()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        // Act
        IEnumerable<int> result = bst.Range(4, 37);

        // Assert
        int[] expectedNodes = new int[] { 4, 5, 8, 9, 10, 37 };
        Assert.AreEqual(expectedNodes.Length, result.ToArray().Length);
    }

    [TestMethod]
    public void Rank_ExistingElement_ShouldReturnCorrectRank()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        // Act
        int rank = bst.Rank(9);

        // Assert
        Assert.AreEqual(5, rank);
    }

    [TestMethod]
    public void Rank_NonExistingSmallElement_ShouldReturnZero()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        // Act
        int rank = bst.Rank(-9);

        // Assert
        Assert.AreEqual(0, rank);
    }

    [TestMethod]
    public void Rank_NonExistingLargeElement_ShouldReturnTreeCount()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        // Act
        int rank = bst.Rank(5500);

        // Assert
        Assert.AreEqual(bst.Count(), rank);
    }

    [TestMethod]
    public void Rank_EmptyTree_ShouldReturnZero()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        // Act
        int rank = bst.Rank(5500);

        // Assert
        Assert.AreEqual(0, rank);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Select_EmptyTree_ShouldThrow()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        // Act
        // Assert
        bst.Select(5);
    }

    [TestMethod]
    public void Select_FewElements_ShouldReturnCorrectElement()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        // Act
        int select = bst.Select(5);

        // Assert
        Assert.AreEqual(9, select);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Select_NonExistingElement_ShouldThrow()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        // Act
        // Assert
       bst.Select(15);
    }

    [TestMethod]
    public void Ceiling_FewElements_ShouldReturnCorrectElement()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        // Act
        int ceiling = bst.Ceiling(4);

        // Assert
        Assert.AreEqual(5, ceiling);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Ceiling_NonExistingCeil_ShouldThrow()

    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(45);

        // Act
        // Assert
        bst.Ceiling(45);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Ceiling_EmptyTree_ShouldThrow()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        // Act
        // Assert
        bst.Ceiling(45);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Floor_EmptyTree_ShouldThrow()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        // Act
        // Assert
        bst.Floor(45);
    }

    [TestMethod]
    public void Floor_FewElements_ShouldReturnCorrectElement()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        // Act
        int floor = bst.Floor(5);

        // Assert
        Assert.AreEqual(4, floor);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Delete_EmptyTree_ShouldThrow()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        // Act
        // Assert
        bst.Delete(45);
    }

    [TestMethod]
    public void Delete_OneElement_ShouldRemoveCorrectly()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);

        // Act
        bst.Delete(10);
        List<int> nodes = new List<int>();
        bst.EachInOrder(nodes.Add);

        // Assert
        int[] expectedNodes = new int[] { };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }

    [TestMethod]
    public void Delete_ElementWithoutRightChild_ShouldReplaceWithLeft()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);
        bst.Insert(0);

        // Act
        bst.Delete(1);
        List<int> nodes = new List<int>();
        bst.EachInOrder(nodes.Add);

        // Assert
        int[] expectedNodes = new int[] { 0, 3, 4, 5, 8, 9, 10, 37, 39, 45 };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }

    [TestMethod]
    public void Delete_LeafElement_ShouldRemoveCorrectly()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        // Act
        bst.Delete(1);
        List<int> nodes = new List<int>();
        bst.EachInOrder(nodes.Add);

        // Assert
        int[] expectedNodes = new int[] { 3, 4, 5, 8, 9, 10, 37, 39, 45 };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }

    [TestMethod]
    public void Delete_RightChildHasNoLeftChild_ShouldPromoteRightChild()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        // Act
        bst.Delete(37);
        List<int> nodes = new List<int>();
        bst.EachInOrder(nodes.Add);

        // Assert
        int[] expectedNodes = new int[] { 1, 3, 4, 5, 8, 9, 10, 39, 45 };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }

    [TestMethod]
    public void Delete_RightChildHasLeftChild_ShouldPromoteSmallestLeft()
    {
        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);
        bst.Insert(6);

        // Act
        bst.Delete(5);
        List<int> nodes = new List<int>();
        bst.EachInOrder(nodes.Add);

        // Assert
        int[] expectedNodes = new int[] { 1, 3, 4, 6, 8, 9, 10, 39, 45 };
        CollectionAssert.AreEqual(expectedNodes, nodes);
    }
}