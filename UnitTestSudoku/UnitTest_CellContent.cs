using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuLib;

namespace UnitTestSudoku
{
    [TestClass]
    public class UnitTest_CellContent
    {
        [TestMethod]
        public void TestContentDisplay()
        {
            CellContent content = new CellContent(0);
            Assert.AreEqual(0, content.Rank);
            Assert.AreEqual("", content.ContentStr);
            content = new CellContent(1);
            Assert.AreEqual(1, content.Rank);
            Assert.AreEqual("1", content.ContentStr);
            content = new CellContent(9);
            Assert.AreEqual(9, content.Rank);
            Assert.AreEqual("9", content.ContentStr);

            Assert.ThrowsException<IndexOutOfRangeException>(() => new CellContent(10));

            CellContent celln = new CellContent(content);
            Assert.AreEqual(9, celln.Rank);
            Assert.AreEqual("9", celln.ContentStr);

            content = new CellContent(4);
            Assert.AreEqual(9, celln.Rank);
            Assert.AreEqual("9", celln.ContentStr);

        }

        [TestMethod]
        public void TestOperators()
        {
            CellContent cell1 = new CellContent(0);
            CellContent cell2 = new CellContent(9);

            Assert.IsTrue(cell1 != cell2);
            Assert.IsTrue(cell1.IsEmpty);
            Assert.IsFalse(cell2.IsEmpty);

            Assert.IsTrue(cell2 > cell1);
            Assert.IsTrue(cell2 >= cell1);

            CellContent cell3 = new CellContent(9);
            Assert.IsTrue(cell3 == cell2);
        }


    }
}
