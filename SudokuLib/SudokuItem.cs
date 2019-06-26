using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    /// <summary>
    /// This class contains any Sodoku 9 items, wether it is a 3x3 square or row or column.
    /// </summary>
    public class SodokuItem : IEnumerable<CellContent>
    {
        protected List<CellContent> cells;
        public SodokuItem()
        {
            Init();
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="another"></param>
        public SodokuItem(SodokuItem another)
        {
            Init();
            for (int i = 0; i < 9; i++)
            {
                InsertItem(new CellContent(another[i]), i);
            }
        } // ctor


        public SodokuItem(List<CellContent> contentList)
        {
            Init();
            for (int i = 0; i < 9; i++)
            {
                InsertItem(new CellContent(contentList[i]), i);
            }
        } // ctor

        private void Init()
        {
            cells = new List<CellContent>();
            for (int i = 0; i < 9; i++) { cells.Add(new CellContent(0)); }
        }

        /// <summary>
        /// Check if all cells are empty (Rank = 0)
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                foreach(var cell in cells)
                { if (cell.Rank != 0) return false; }
                return true;
            }
        }

        /// <summary>
        /// How many cells have content
        /// </summary>
        public int ContentCount
        {
            get
            {
                int count = 0;
                foreach(var cell in cells)
                { if (cell.Rank > 0) { count++; } }

                return count;
            }
        }

        /// <summary>
        /// How many cells don't have content
        /// </summary>
        public int EmptyCount
        {
            get
            {
                return 9 - ContentCount;
            }
        }

        /// <summary>
        /// Check if all cells have content
        /// </summary>
        public bool IsFull
        {
            get
            {
                return ContentCount == 9;
            }
        }

        /// <summary>
        /// Check if content already loaded
        /// </summary>
        /// <param name="contentToCheck"></param>
        /// <returns></returns>
        public bool HasContent(CellContent contentToCheck)
        {
            foreach(var cell in cells)
            { if (cell == contentToCheck) { return true; } }

            return false;
        }


        /// <summary>
        /// Validates the item.  It must be empty or not have dupliate values
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            if(this.IsEmpty) { return true; }

            List<CellContent> check = new List<CellContent>();
            foreach(var cell in cells)
            {
                if (cell.IsEmpty) { continue; }
                foreach(var ccell in check)
                {
                    if(ccell == cell) { return false; }
                }
                check.Add(cell);
            }

            return true;
        }

        public int ValidateIndex(int index)
        {
            if(index < 0 || index > 8) { throw new IndexOutOfRangeException();  }

            return index;
        }


        public void InsertItem(CellContent content, int index)
        {
            if (!content.IsEmpty)
            {
                if (HasContent(content)) { throw new ArgumentException($"*Error: Cannot insert {content.ContentStr}, it already exists in the set!"); }
            }
                
            if(!cells[index].IsEmpty) { throw new InvalidOperationException($"*Error: Cannot insert {content.ContentStr}, into an occupied cell at index={index}!"); }
            cells[index] = content;
        }


        /// <summary>
        /// Replaces non-fixed items with a new value.  Throws exception if the replacement value already appears in the set.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="index"></param>
        public void ReplaceItem(CellContent content, int index)
        {
            index = ValidateIndex(index);
            if (content == cells[index]) { return; } // trivial - replace like with like

            if(!content.IsEmpty)
            {
                if (HasContent(content)) { throw new ArgumentException($"*Error: Cannot insert {content.ContentStr}, it already exists in the set!"); }
            }
            if(cells[index].IsFixed && !cells[index].IsEmpty) { return; } // ignore replacement of a fixed item
            cells[index] = content;
        }


        public void ClearCell(int index) => ReplaceItem(new CellContent(0), index);


        /// <summary>
        /// List of ranks that have not been placed yet
        /// </summary>
        /// <returns></returns>
        public List<int> AvailableRanks()
        {
            List<int> ranks = new List<int>();
            //if(IsEmpty)
            //{
                for(int r = 1; r < 10; r++)
                {
                    if(cells.Where(k => k.Rank == r).Count() > 0) { continue; }
                    ranks.Add(r);
                }
            //}

            return ranks;
        }


        public CellContent this[int index]
        {
            get
            {
                return cells[index];
            }
            set
            {
                ReplaceItem(value, index);
            }
        }


        public IEnumerator<CellContent> GetEnumerator()
        {
            foreach (CellContent cell in cells)
                yield return cell;
        }

        
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
