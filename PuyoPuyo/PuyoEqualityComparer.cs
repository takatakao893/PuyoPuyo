using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuyoPuyo2
{
    class PuyoEqualityComparer : IEqualityComparer<Cell>
    {
        public bool Equals(Cell cell1, Cell cell2)
        {
            if (cell2 == null && cell1 == null)
                return true;
            else if (cell1 == null || cell2 == null)
                return false;
            else if (cell1.Puyo == cell2.Puyo)
                return true;
            else
                return false;

        }

        public int GetHashCode(Cell cell)
        {
            int hCode = cell.Puyo.GetHashCode();
            return hCode.GetHashCode();
        }
    }
}
