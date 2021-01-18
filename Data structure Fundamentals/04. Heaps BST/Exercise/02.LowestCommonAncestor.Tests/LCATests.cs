namespace _02.LowestCommonAncestor.Tests
{
    using NUnit.Framework;

    public class LCATests
    {
        private IAbstractBinaryTree<int> _binaryTree;

        [SetUp]
        public void Setup()
        {
            this._binaryTree = 
            new BinaryTree<int>(7,
                new BinaryTree<int>(21, null, null), 
                new BinaryTree<int>(14, 
                    new BinaryTree<int>(23, new BinaryTree<int>(5, null, null), null), 
                    new BinaryTree<int>(6, null, 
                        new BinaryTree<int>(13, null, null))));
        }

        [Test]
        public void TestCommonAncestorWorksCorrectly()
        {
            Assert.AreEqual(14, this._binaryTree.FindLowestCommonAncestor(23, 13));
        }

        [Test]
        public void TestCommonAncestorWorksCorrectly2()
        {
            Assert.AreEqual(14, this._binaryTree.FindLowestCommonAncestor(6, 13));
        }

        [Test]
        public void TestCommonAncestorWorksCorrectly3()
        {
            Assert.AreEqual(14, this._binaryTree.FindLowestCommonAncestor(6, 23));
        }

        [Test]
        public void TestCommonAncestorWorksCorrectly4()
        {
            Assert.AreEqual(7, this._binaryTree.FindLowestCommonAncestor(21, 23));
        }

        [Test]
        public void TestCommonAncestorWorksCorrectly5()
        {
            Assert.AreEqual(14, this._binaryTree.FindLowestCommonAncestor(5, 13));
        }
    }
}