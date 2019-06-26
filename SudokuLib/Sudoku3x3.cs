using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    public class Sudoku3x3 : SudokuItem
    {

        public Sudoku3x3() : base() {; }

        public Sudoku3x3(SudokuItem another) : base(another) { ; }

        public Sudoku3x3(List<CellContent> contentList) : base(contentList) { ; }

        public CellContent this[int x, int y]
        {
            get
            {
                return cells[GetIndexFromGridPosition(x, y)];
            }
            set
            {
                cells[GetIndexFromGridPosition(x, y)] = value;
            }
        }


        public List<CellContent> GetRow(int index_Y)
        {
            index_Y = Validate3x3Index(index_Y);

            List<CellContent> row = new List<CellContent>();
            for(int x = 0; x < 3; x++)
            { row.Add(this[x, index_Y]); }

            return row;
        }


        public List<CellContent> GetColumn(int index_X)
        {
            index_X = Validate3x3Index(index_X);

            List<CellContent> col = new List<CellContent>();
            for (int y = 0; y < 3; y++)
            { col.Add(this[index_X, y]); }

            return col;
        }


        private int Validate3x3Index(int index)
        {
            if (index < 0 || index > 2) { throw new IndexOutOfRangeException(); }
            return index;
        }


        /// <summary>
        /// Convert the index value to a 3x3 x,y GridPosition
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public GridPosition GetGridPositionFromIndex(int index)
        {
            index = ValidateIndex(index);
            GridPosition position = new GridPosition();
            if(index == 0) { return position; }

            position.Y = index / 3;
            position.X = index % 3;

            return position;
        }


        /// <summary>
        /// Convert a x,y grid position to the index where the CellContent is in the list
        /// </summary>
        public int GetIndexFromGridPosition(int x, int y) => GetIndexFromGridPosition(new GridPosition(x, y));


        /// <summary>
        /// Convert a x,y grid position to the index where the CellContent is in the list
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public int GetIndexFromGridPosition(GridPosition position)
        {
            int idx = position.Y * 3 + position.X;

            return ValidateIndex(idx);
        }

        public SudokuItem ToSudokuItem()
        {
            return new SudokuItem(cells);
        }


    }
}
