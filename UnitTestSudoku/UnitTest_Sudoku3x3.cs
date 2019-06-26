using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuLib;

namespace UnitTestSudoku
{
    [TestClass]
    public class UnitTest_Sudoku3x3
    {
        [TestMethod]
        public void TestDataManipulation()
        {
            List<CellContent> contentlist = new List<CellContent>
            {
                new CellContent(0), // 0, 0
                new CellContent(1), // 1, 0
                new CellContent(0), // 2, 0
                new CellContent(2), // 0, 1
                new CellContent(0), // 1, 1
                new CellContent(3), // 2, 1
                new CellContent(0), // 0, 2
                new CellContent(4), // 1, 2
                new CellContent(5), // 2, 2
            };

            Sudoku3x3 grid = new Sudoku3x3(contentlist);

            Assert.AreEqual(grid[0, 0], new CellContent(0));
            Assert.AreEqual(grid[1, 0], new CellContent(1));
            Assert.AreEqual(grid[0, 1], new CellContent(2));
            Assert.AreEqual(grid[2, 1], new CellContent(3));
            Assert.AreEqual(grid[2, 2], new CellContent(5));
            
            SudokuItem t1 = new SudokuItem(contentlist);
            SudokuItem t2 = grid.ToSudokuItem();

            for(int idx = 0; idx < 9; idx++)
            {
                Assert.AreEqual(t1[idx], t2[idx]);
            }

            List<CellContent> col= grid.GetColumn(2);
            Assert.AreEqual(col[0], grid[2, 0]);
            Assert.AreEqual(col[1], grid[2, 1]);
            Assert.AreEqual(col[2], grid[2, 2]);

            List<CellContent> row = grid.GetRow(1);
            Assert.AreEqual(row[0], grid[0, 1]);
            Assert.AreEqual(row[1], grid[1, 1]);
            Assert.AreEqual(row[2], grid[2, 1]);


        } // TestDataManipulation

    }
}
