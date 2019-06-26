using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    /// <summary>
    /// Class combines Cell Content with the location of the cell om the 9x9 grid. This class is generally used to load fixed content, or to read back the grid state
    /// of each cell.
    /// </summary>
    public class GridContent : CellContent
    {
        public GridContent(CellContent cell) : base(cell)
        { }

        public GridPosition Position { get; set; }

        public CellContent ToCellContent()
        {
            CellContent baseContent = new CellContent(this);
            return baseContent;
        }

        public override string ToString()
        {
            return $"{{rank:{Rank},Content:{ContentStr},IsFixed:{IsFixed},X:{Position.X},Y:{Position.Y}}}";
        }

    }  // class
}
