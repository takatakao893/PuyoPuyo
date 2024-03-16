using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuyoPuyo2
{
    public class Cell
    {

        public Cell(int x, int y) 
        {
            LeftTop = new Point(x, y);
        }

        // 短形の左上の座標
        public Point LeftTop
        {
            get;
            private set;
        } = new Point(0, 0);

        // サイズはすべて同じ
        static public Size Size
        {
            get;
        } = new Size(30, 30);

        // そのセルは左から何番目、上から何番目にあるか指定
        public void SetColumRow(int colum, int row)
        {
            Colum = colum;
            Row = row;
        }

        // そのセルは左から何番目にあるか
        public int Colum
        {
            get;
            private set;
        } = 0;

        // そのセルは上から何番目にあるか
        public int Row
        {
            get;
            private set;
        } = 0;

        // ぷよは固定されているか
        public bool IsFixed
        {
            get;
            set;
        } = false;

        // ぷよのタイプ
        public Puyo Puyo
        {
            get;
            set;
        } = Puyo.None;

        // ぷよを消す処理をするときに使う
        public bool isChecked
        {
            get;
            set;
        } = false;

    }

    public enum Puyo
    {
        None = 0,
        Wall = -1,
        Puyo1 = 1,
        Puyo2 = 2,
        Puyo3 = 3,
        Puyo4 = 4,
    }
}
