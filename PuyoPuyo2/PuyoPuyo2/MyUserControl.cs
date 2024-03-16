using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuyoPuyo2
{
    public partial class MyUserControl : UserControl
    {
        public MyUserControl()
        {
            InitializeComponent();
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if(keyData == Keys.Right || keyData == Keys.Left ||
                keyData == Keys.Up || keyData == Keys.Down) 
            {
                return true;
            }
            return base.IsInputKey(keyData);
        }
    }
}
