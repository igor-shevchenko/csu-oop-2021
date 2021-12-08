using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Set;

namespace SetTests
{
    [TestClass]
    public class BinaryTreeTest
    {
        [TestMethod]
        public void TestAddThenFind()
        {
            var tree = new BinaryTree<int>();
            tree.Insert(0);

            Assert.IsTrue(tree.Find(0));
        }

        [TestMethod]
        public void TestAddMultipleThenFind()
        {
            var tree = new BinaryTree<int>();
            tree.Insert(0);
            tree.Insert(-5);
            tree.Insert(5);

            Assert.IsTrue(tree.Find(5));
            Assert.IsTrue(tree.Find(-5));
        }

        [TestMethod]
        public void TestDontAddThenDontFind()
        {
            var tree = new BinaryTree<int>();
            tree.Insert(0);

            Assert.IsFalse(tree.Find(1));
        }


        [TestMethod]
        public void TestTraverse()
        {
            var tree = new BinaryTree<int>();
            tree.Insert(0);
            tree.Insert(-5);
            tree.Insert(-10);
            tree.Insert(6);
            tree.Insert(9);

            CollectionAssert.AreEqual(tree.Traverse().ToList(), new List<int> {-10, -5, 0, 6, 9});
        }


        [TestMethod]
        public void TestAddThenRemove()
        {
            var tree = new BinaryTree<int>();
            tree.Insert(0);

            Assert.IsTrue(tree.Remove(0));
        }

        [TestMethod]
        public void TestDontAddThenRemove()
        {
            var tree = new BinaryTree<int>();
            tree.Insert(0);

            Assert.IsFalse(tree.Remove(1));
        }

        [TestMethod]
        public void TestAddMultipleThenRemoveFromLeft()
        {
            var tree = new BinaryTree<int>();
            tree.Insert(0);
            tree.Insert(-5);
            tree.Insert(-3);
            tree.Insert(-1);

            Assert.IsTrue(tree.Remove(-5));
            CollectionAssert.AreEqual(tree.Traverse().ToList(), new List<int> {-3, -1, 0});
        }

        [TestMethod]
        public void TestAddMultipleThenRemoveFromRight()
        {
            var tree = new BinaryTree<int>();
            tree.Insert(0);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(1);

            Assert.IsTrue(tree.Remove(5));
            CollectionAssert.AreEqual(tree.Traverse().ToList(), new List<int> { 0, 1, 3 });
        }
    }

    [TestClass]
    public class BinaryTreeSetSerializerTest
    {
        public class MemoryStreamService : IFileService
        {
            public MemoryStream stream = new MemoryStream();

            public Stream GetReadStream(string name)
            {
                stream.Position = 0;
                return stream;
            }

            public Stream GetWriteStream(string name)
            {
                return stream;
            }
        }

        [TestMethod]
        public void TestSerializeAndDeserialize()
        {
            var tree = new BinaryTreeSet<int> {0, 1, 2};

            var serializer = new BinaryTreeSetSerializer(new MemoryStreamService());
            serializer.Serialize(tree, "123", closeStream: false);
            var newTree = serializer.Deserialize<int>("123", closeStream:false);

            CollectionAssert.AreEqual(tree.ToList(), newTree.ToList());
        }
    }
}
