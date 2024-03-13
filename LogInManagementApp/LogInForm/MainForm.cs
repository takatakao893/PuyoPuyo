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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonRegist_Click(object sender, EventArgs e)
        {
            // RegistMainFormのインスタンスを生成
            RegistMainForm form = new RegistMainForm();
            // formを表示
            form.ShowDialog();  
            // RegistFormのインスタンスを生成
            //RegistForm form = new RegistForm();
            // formを表示
            //form.ShowDialog();
        }

        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            // LogInFormのインスタンスを生成
            LogInForm form = new LogInForm();
            // formを表示
            form.ShowDialog();

        }

        private void buttonRefer_Click(object sender, EventArgs e)
        {
            // ReferFormのインスタンスを生成
            ReferForm form = new ReferForm();
            // formを表示
            form.ShowDialog();
        }
    }
}
