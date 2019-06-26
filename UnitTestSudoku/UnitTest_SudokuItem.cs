using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuLib;

namespace UnitTestSudoku
{
    [TestClass]
    public class UnitTest_SudokuItem
    {
        [TestMethod]
        public void TestBasics()
        {
            SudokuItem row = new SudokuItem();
            Assert.IsTrue(row.IsEmpty);
            Assert.AreEqual(9, row.EmptyCount);
            Assert.AreEqual(0, row.ContentCount);

            Assert.IsTrue(row.HasContent(new CellContent(0)));
            Assert.IsFalse(row.HasContent(new CellContent(1)));
            Assert.IsTrue(row.Validate());
        }

        [TestMethod]
        public void TestAddReplaceContent()
        {
            SudokuItem column = new SudokuItem();
            column[0] = new CellContent(9);
            Assert.IsFalse(column.IsEmpty);
            Assert.AreEqual(1, column.ContentCount);
            column[1] = new CellContent(8);

            Assert.ThrowsException<ArgumentException>(() => column[2] = new CellContent(9));
            Assert.AreEqual(2, column.ContentCount);

            column.ClearCell(1);
            Assert.AreEqual(1, column.ContentCount);
            column[1] = new CellContent(8);
            column[2] = new CellContent(7);

            Assert.ThrowsException<InvalidOperationException>(() => column.InsertItem(new CellContent(6), 2));
            List<int> ranks = column.AvailableRanks();
            Assert.AreEqual(6, ranks.Count);
            Assert.AreEqual(1, ranks[0]);
            Assert.AreEqual(2, ranks[1]);
            Assert.AreEqual(6, ranks[5]);

            column[3] = new CellContent(6);
            column[4] = new CellContent(5);
            column[5] = new CellContent(4);
            column[6] = new CellContent(3);
            column[7] = new CellContent(2);
            Assert.AreEqual(8, column.ContentCount);
            ranks = column.AvailableRanks();
            Assert.AreEqual(1, ranks.Count);
            Assert.AreEqual(1, ranks[0]);

            column[8] = new CellContent(1);
            Assert.IsTrue(column.IsFull);
            Assert.IsTrue(column.Validate());

            //Assert.ThrowsException<InvalidOperationException>(() => column.InsertItem(new CellContent(9), 5) );
        }


        [TestMethod]
        public void TestCopyConstructor()
        {
            List<CellContent> contentlist = new List<CellContent>
            {
                new CellContent(0), // 0, 0
                new CellContent(1), // 0, 1
                new CellContent(0), // 0, 2
                new CellContent(2), // 1, 0
                new CellContent(0), // 1, 1
                new CellContent(3), // 1, 2
                new CellContent(9), // 2, 0
                new CellContent(4), // 2, 1
                new CellContent(5), // 2, 2
            };

            SudokuItem item1 = new SudokuItem(contentlist);

            Assert.IsTrue(item1[1] == new CellContent(1));
            Assert.IsTrue(item1[8] == new CellContent(5));

            SudokuItem copy = new SudokuItem(item1);

            for (int i = 0; i < 9; i++)
            {
                Assert.IsTrue(copy[i] == item1[i]);
            }
        } // 


        [TestMethod]
        public void TestFixedItems()
        {
            List<CellContent> contentlist = new List<CellContent>
            {
                new CellContent(0), // 0, 0
                new CellContent(1, true), // 0, 1
                new CellContent(0), // 0, 2
                new CellContent(2), // 1, 0
                new CellContent(0), // 1, 1
                new CellContent(3), // 1, 2
                new CellContent(9, true), // 2, 0
                new CellContent(4), // 2, 1
                new CellContent(5), // 2, 2
            };

            SudokuItem item = new SudokuItem(contentlist);
            Assert.IsTrue(item[1].IsFixed);

            item.ReplaceItem(new CellContent(1), 1);
            Assert.AreEqual(new CellContent(1), item[1]);
            Assert.IsTrue(item[1].IsFixed);
            Assert.IsFalse(item[1].IsEmpty);

            item.ReplaceItem(new CellContent(7), 1);

            Assert.AreEqual(new CellContent(1), item[1]);

        }


        [TestMethod]
        public void TestAvailbleItems()
        {
            SudokuItem item = new SudokuItem();

            Assert.AreEqual(9, item.AvailableRanks().Count);
        }

    } // class
}
