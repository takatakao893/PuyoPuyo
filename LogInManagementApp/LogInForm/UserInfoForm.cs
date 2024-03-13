using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogInForm
{
    public partial class UserInfoForm : Form
    {
        // LogInFormからの値を格納するための変数
        private string ID_str;
        public string connectionString = "Data Source=localhost\\SQLEXPRESS;" +
            "Initial Catalog=myDB;" +
            "Integrated Security=SSPI;" +
            "TrustServerCertificate=true;";
        public UserInfoForm(string str)
        {
            InitializeComponent();
            ID_str = str;
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonRegit_Click(object sender, EventArgs e)
        {
            // UserRegistFormのインスタンスを生成 ＊IDの値渡し
            UserRegistForm form = new UserRegistForm(ID_str);
            // formを表示
            form.Show();
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            // UserChangeFormのインスタンスを生成 *IDの値渡し
            UserChangeForm form = new UserChangeForm(ID_str);
            // formを表示
            form.Show();
        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            // InfoViewFormのインスタンスを生成
            InfoViewForm form = new InfoViewForm();
            // formを表示
            form.Show();
        }
    }
}
