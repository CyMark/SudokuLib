using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    /// <summary>
    /// Class for Sudoku cell content. Derive this class for alternative content types.  Default is ContentStr string.
    /// </summary>
    public class CellContent : IComparable
    {
        public CellContent(int rank) : this(rank, false)
        {
            ;
        }

        public CellContent(int rank, bool isFixed)
        {
            IsFixed = isFixed;
            Rank = ValidateIndex(rank);
            if (rank == 0) { ContentStr = ""; }
            else { ContentStr = rank.ToString(); }  // default
        }

        public CellContent(int rank, Dictionary<int, string> customContent)
        {
            IsFixed = false;
            Rank = ValidateIndex(rank);
            if (rank == 0)
            {
                if(customContent.ContainsKey(0)) { ContentStr = customContent[0]; }
                else { ContentStr = ""; }
            }
            else
            {
                ContentStr = customContent[rank];
            }
            
            Rank = rank;
        }  //ctor

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy"></param>
        public CellContent(CellContent copy)
        {
            Rank = copy.Rank;
            ContentStr = copy.ContentStr;
            IsFixed = copy.IsFixed;
        }

        /// <summary>
        /// Initial load attribute stating whether the item was loaded from a puzzle as a fixed entry
        /// </summary>
        public bool IsFixed { get; set; }


        private int ValidateIndex(int index)
        {
            if (index < 0 || index > 9) { throw new IndexOutOfRangeException($"*Error Index out of range: Valid range 0-9 but was {index}!"); }
            return index;
        }


        public int Rank { get; private set; }
        public string ContentStr { get; set; }

        public bool IsEmpty { get { return Rank == 0; } }
        

        public int CompareTo(object anotherCellContent)
        {
            if(anotherCellContent == null) { return 1; }
            CellContent content = anotherCellContent as CellContent;
            return Rank.CompareTo(content.Rank);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) { return false; }
            CellContent content = obj as CellContent;

            return content.Rank == Rank;
        }

        public override int GetHashCode()
        {
            return Rank.GetHashCode();
        }

        // operators

        static public bool operator ==(CellContent left, CellContent right)
        {
            return left.CompareTo(right) == 0;
        }

        static public bool operator !=(CellContent left, CellContent right)
        {
            return left.CompareTo(right) != 0;
        }

        static public bool operator >=(CellContent left, CellContent right)
        {
            return left.Rank >= right.Rank;
        }

        static public bool operator <=(CellContent left, CellContent right)
        {
            return left.Rank <= right.Rank;
        }

        static public bool operator >(CellContent left, CellContent right)
        {
            return left.Rank > right.Rank;
        }

        static public bool operator <(CellContent left, CellContent right)
        {
            return left.Rank < right.Rank;
        }

        public override string ToString()
        {
            return $"{{rank:{Rank},Content:{ContentStr},IsFixed:{IsFixed}}}";
        }


    } // class
}
