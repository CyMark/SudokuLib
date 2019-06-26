using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    /// <summary>
    /// Sodoku board.  The board is a list of rows of SodokuItems (each with 9 elements).  Columns and 3x3 grids are derived from these rows.
    /// </summary>
    public class Sudoku
    {
        private List<SodokuItem> rows;

        public Sudoku()
        {
            ClearSudoku();
        }

        /// <summary>
        /// Clears the SodukuGrid regardless of fixed content
        /// </summary>
        public void ClearSudoku()
        {
            rows = new List<SodokuItem>();
            for (int n = 0; n < 9; n++)
            {
                rows.Add(new SodokuItem());
            }
        }


        /// <summary>
        /// Clears all non-fixed content
        /// </summary>
        public void ResetSudoku()
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; x < 9; y++)
                {
                    if (!this[x, y].IsFixed) { this[x, y] = new CellContent(0); }
                }
                //rows.Add(new SodokuItem());
            }
        }

        public int GameNr { get; set; }
        public string GameName { get; set; }
        public string UserName { get; set; }


        /// <summary>
        /// Validates that the sodoku has no violations in entries
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            // validate rows
            for (int n = 0; n < 9; n++)
            {
                if (!GetRowAt(n).Validate()) { return false; }
            }

            // validate columns
            for (int n = 0; n < 9; n++)
            {
                if (!GetColumnAt(n).Validate()) { return false; }
            }

            // validate grids
            for (int c = 0; c < 2; c++)
            {
                for (int r = 0; r < 2; r++)
                {
                    if (!GetSudoku3x3At(c, r).Validate()) { return false; }
                }
            }

            return true;
        }


        public bool IsEmpty
        {
            get
            {
                foreach (var row in rows)
                {
                    if (!row.IsEmpty) { return false; }
                }
                return true;
            }
        }


        private int ValidateIndex(int index)
        {
            if (index < 0 || index > 8) { throw new IndexOutOfRangeException(); }
            return index;
        }


        private int ValidateIndex3x3(int index)
        {
            if (index < 0 || index > 2) { throw new IndexOutOfRangeException(); }
            return index;
        }


        public CellContent this[int x, int y]
        {
            get
            {
                x = ValidateIndex(x);
                y = ValidateIndex(y);
                return rows[y][x];
            }
            set
            {
                x = ValidateIndex(x);
                y = ValidateIndex(y);
                rows[y][x] = value;
            }
        }

        public void ClearCell(int x, int y) => this[x, y] = new CellContent(0);
        public void ClearCell(GridPosition g) => this[g.X, g.Y] = new CellContent(0);

        public SodokuItem GetColumnAt(int x)
        {
            x = ValidateIndex(x);
            SodokuItem column = new SodokuItem();
            for (int n = 0; n < 9; n++)
            {
                column.ReplaceItem(rows[n][x], n);
            }
            return column;
        }


        public SodokuItem GetRowAt(int y)
        {
            y = ValidateIndex(y);
            return rows[y];
        }


        /// <summary>
        /// Returns the 3x3 grid at xGrids (0-2) and yGrids (0-2).  Eg. xGrids=2 and yGrids=2 will return the bottom righthand 3x3 grid.
        /// </summary>
        /// <param name="xGrids"></param>
        /// <param name="yGrids"></param>
        /// <returns></returns>
        public Sodoku3x3 GetSudoku3x3At(int xGrids, int yGrids)
        {
            xGrids = ValidateIndex3x3(xGrids);
            yGrids = ValidateIndex3x3(yGrids);
            Sodoku3x3 grid = new Sodoku3x3();
            int xOffset = xGrids * 3;
            int yOffset = yGrids * 3;

            for (int nx = xOffset; nx < xOffset + 3; nx++)
            {
                for (int ny = yOffset; ny < yOffset + 3; ny++)
                {
                    grid[nx - xOffset, ny - yOffset] = rows[ny][nx];
                }
            }
            return grid;
        }

        /// <summary>
        /// Get the 3x3 grid that is in the range of the 9x9 coordinates x, y.  E.g. x values of 6-8 and y values of 6-8 will get the lower right-hand 3x3 grid (at xGrids,yGrids = 2, 2).
        /// x,y index values in the range 0-2 will get the top left-hand 3x3 grid.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Sodoku3x3 GetSudoku3x3AtGridPosition(int x, int y) => GetSudoku3x3At(GridIndexFromItemIndex(x), GridIndexFromItemIndex(y));


        public SodokuItem Sudoku3x3ToItemAt(int xGrids, int yGrids) => GetSudoku3x3At(xGrids, yGrids).ToSodokuItem();


        public List<GridContent> ReadGridContent()
        {
            List<GridContent> content = new List<GridContent>();
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    GridContent gc = new GridContent(this[x, y]);
                    gc.Position = new GridPosition(x, y);
                    content.Add(gc);
                }
            }

            return content;
        }

        public void SetGridContent(List<GridContent> contents)
        {
            ClearSudoku();
            foreach (var item in contents)
            {
                this[ValidateIndex(item.Position.X), ValidateIndex(item.Position.Y)] = item.ToCellContent();
            }
        }


        public int GridIndexFromItemIndex(int itemIndex)
        {
            itemIndex = ValidateIndex(itemIndex);

            return itemIndex / 3;
        }


        public List<int> AvailableRanks(int x, int y)
        {
            List<int> ranks = new List<int>();

            if (!this[x, y].IsEmpty) { return ranks; }

            var rowRanks = GetRowAt(y).AvailableRanks();
            var columnRanks = GetColumnAt(x).AvailableRanks();
            var gridRanks = GetSudoku3x3AtGridPosition(x, y).AvailableRanks();

            for (int r = 1; r < 10; r++)
            {
                if ((rowRanks.Where(q => q == r).Count() > 0) && (columnRanks.Where(q => q == r).Count() > 0) && (gridRanks.Where(q => q == r).Count() > 0))
                { ranks.Add(r); }
            }

            return ranks;
        }


        public bool ValidateMove(int x, int y, int rank)
        {
            if (rank == 0) { return true; } // clearing a cell always valid
            var ranks = AvailableRanks(x, y);
            if (ranks.Where(q => q == rank).Count() > 0) { return true; }

            return false;
        }


        public override string ToString()
        {
            string str = $"{{GameNr:{GameNr},GameName:{GameName},UserName:{UserName}";
            if (!IsEmpty)
            {
                str += ",gridcontent:[";
                for (int x = 0; x < 9; x++)
                {
                    for (int y = 0; y < 9; y++)
                    {
                        str += new GridContent(this[x, y]).ToString() + ",";
                    }
                }
                str = str.Substring(0, str.Length - 2); // truncate last comma
                str += "]";
            }
            return str + "}}";
        }

    } // class
}
