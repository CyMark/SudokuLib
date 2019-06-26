using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    public class GridPosition
    {
        public GridPosition()
        { X = Y = 0; }

        public GridPosition(int x, int y)
        { X = x; Y = y; }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
