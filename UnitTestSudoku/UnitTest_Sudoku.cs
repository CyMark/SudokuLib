using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuLib;

namespace UnitTestSudoku
{
    [TestClass]
    public class UnitTest_Sudoku
    {
        [TestMethod]
        public void Test_Sudoku_Init()
        {
            Sudoku Sudoku = new Sudoku();
            Assert.AreEqual(0, Sudoku.GetColumnAt(0).ContentCount);
            Assert.AreEqual(0, Sudoku.GetColumnAt(8).ContentCount);

            Assert.AreEqual(0, Sudoku.GetSudoku3x3At(0, 0).ContentCount);
            Assert.AreEqual(0, Sudoku.GetSudoku3x3At(1, 2).ContentCount);
            Assert.AreEqual(0, Sudoku.GetSudoku3x3At(2, 1).ContentCount);
            Assert.AreEqual(0, Sudoku.GetSudoku3x3At(2, 2).ContentCount);

        }

        [TestMethod]
        public void Test_Sudoku_Grid()
        {
            Sudoku Sudoku = new Sudoku();

            // top left
            Sudoku[2, 2] = new CellContent(9);
            int rank = Sudoku[2, 2].Rank;
            Assert.AreEqual(9, Sudoku[2, 2].Rank);
            SudokuItem row = Sudoku.GetRowAt(2);
            Assert.AreEqual(9, row[2].Rank);
            SudokuItem col = Sudoku.GetColumnAt(2);
            Assert.AreEqual(9, col[2].Rank);
            Sudoku3x3 sgrid = Sudoku.GetSudoku3x3AtGridPosition(2, 2);
            Assert.AreEqual(9, sgrid[2, 2].Rank);

            // bottom right
            Sudoku[7, 8] = new CellContent(1);
            rank = Sudoku[7, 8].Rank;
            Assert.AreEqual(rank, Sudoku[7, 8].Rank);
            row = Sudoku.GetRowAt(8);
            Assert.AreEqual(rank, row[7].Rank);
            col = Sudoku.GetColumnAt(7);
            Assert.AreEqual(rank, col[8].Rank);
            sgrid = Sudoku.GetSudoku3x3AtGridPosition(7, 8);
            Assert.AreEqual(rank, sgrid[1, 2].Rank);

            // middle
            Sudoku[4, 5] = new CellContent(5);
            rank = Sudoku[4, 5].Rank;
            Assert.AreEqual(rank, Sudoku[4, 5].Rank);
            row = Sudoku.GetRowAt(5);
            Assert.AreEqual(rank, row[4].Rank);
            col = Sudoku.GetColumnAt(4);
            Assert.AreEqual(rank, col[5].Rank);
            sgrid = Sudoku.GetSudoku3x3AtGridPosition(4, 5);
            Assert.AreEqual(rank, sgrid[1, 2].Rank);
            
            // middle bottom
            Sudoku[5, 7] = new CellContent(2);
            rank = Sudoku[5, 7].Rank;
            Assert.AreEqual(rank, Sudoku[5, 7].Rank);
            row = Sudoku.GetRowAt(7);
            Assert.AreEqual(rank, row[5].Rank);
            col = Sudoku.GetColumnAt(5);
            Assert.AreEqual(rank, col[7].Rank);
            sgrid = Sudoku.GetSudoku3x3AtGridPosition(5, 7);
            Assert.AreEqual(rank, sgrid[2, 1].Rank);

            Assert.IsTrue(Sudoku.Validate());
            //Sudoku.
            
        }


        [TestMethod]
        public void Test_Sudoku_Available()
        {
            Sudoku Sudoku = new Sudoku();

            List<int> available = Sudoku.AvailableRanks(4, 4);

            Assert.AreEqual(9, available.Count);

            Sudoku[4, 1] = new CellContent(1);

            var col = Sudoku.GetColumnAt(4);
            Assert.AreEqual(8, col.AvailableRanks().Count);

            available = Sudoku.AvailableRanks(4, 4);
            Assert.AreEqual(8, available.Count);

            Sudoku[1, 4] = new CellContent(1);
            available = Sudoku.AvailableRanks(4, 4);
            Assert.AreEqual(8, available.Count);

            Sudoku[2, 4] = new CellContent(2);
            available = Sudoku.AvailableRanks(4, 4);
            Assert.AreEqual(7, available.Count);

            Sudoku[4, 8] = new CellContent(3);
            available = Sudoku.AvailableRanks(4, 4);
            Assert.AreEqual(6, available.Count);

            Sudoku[3, 3] = new CellContent(4);
            available = Sudoku.AvailableRanks(4, 4);
            Assert.AreEqual(5, available.Count);

            Sudoku[5, 5] = new CellContent(5);
            available = Sudoku.AvailableRanks(4, 4);
            Assert.AreEqual(4, available.Count);

            Sudoku[7, 4] = new CellContent(6);
            available = Sudoku.AvailableRanks(4, 4);
            Assert.AreEqual(3, available.Count);

            Sudoku[4, 7] = new CellContent(7);
            available = Sudoku.AvailableRanks(4, 4);
            Assert.AreEqual(2, available.Count);

            Sudoku[3, 5] = new CellContent(8);
            available = Sudoku.AvailableRanks(4, 4);
            Assert.AreEqual(1, available.Count);

            Assert.AreEqual(9, available[0]);

        }



        [TestMethod]
        public void Test_Sudoku_Available_T2()
        {
            Sudoku Sudoku = new Sudoku();

            List<int> available = Sudoku.AvailableRanks(0, 6);
            Assert.AreEqual(9, available.Count);

            Sudoku[0, 6] = new CellContent(1);
            available = Sudoku.AvailableRanks(0, 6);
            Assert.AreEqual(0, available.Count);

            available = Sudoku.AvailableRanks(1, 7);
            Assert.AreEqual(8, available.Count);

            // check if rank 1 is available:
            int cnt = available.Where(q => q == 1).Count();
            Assert.AreEqual(0, cnt);

            Assert.IsFalse(Sudoku.ValidateMove(1, 7, 1));
        }


    }
}
