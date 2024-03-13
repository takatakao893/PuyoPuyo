using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogInForm
{
    public partial class RegistMainForm : Form
    {
        public RegistMainForm()
        {
            InitializeComponent();
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonRegit_Click(object sender, EventArgs e)
        {
            // RegistFormのインスタンスを生成
            RegistForm form = new RegistForm();
            // formを表示
            form.ShowDialog();
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            // PassChangeFormのインスタンスを生成
            PassChangeForm form = new PassChangeForm();
            // formを表示
            form.ShowDialog();
        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            // ChangeViewFormのインスタンスを生成
            ChangeViewForm form = new ChangeViewForm();
            // formを表示
            form.ShowDialog();
        }
    }
}
