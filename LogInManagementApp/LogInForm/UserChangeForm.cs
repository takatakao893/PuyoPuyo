using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace LogInForm
{
    public partial class UserChangeForm : Form
    {
        // LogInFormからの値を格納するための変数
        private string ID_str;
        public string connectionString = "Data Source=localhost\\SQLEXPRESS;" +
                "Initial Catalog=myDB;" +
                "Integrated Security=SSPI;" +
                "TrustServerCertificate=true;";
        public UserChangeForm(string str)
        {
            ID_str = str;
            InitializeComponent();
            string sqlStr = "select [Name], [From], PostCode, [Address], PhoneNo, BirthDay" +
                " from UserInfoTable where ID = @id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sqlStr, conn))
                {
                    conn.Open();    // UserInfoTableとの接続
                    command.Parameters.AddWithValue("@id", Convert.ToInt32(ID_str));

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            textName.Text += Convert.ToString(dr["Name"]);
                            textFrom.Text += Convert.ToString(dr["From"]);
                            textPostCo.Text += Convert.ToString(dr["PostCode"]);
                            textAddress.Text += Convert.ToString(dr["Address"]);
                            textPhone.Text += Convert.ToString(dr["PhoneNo"]);
                            BirthDay.Text += Convert.ToString(dr["BirthDay"]);
                        }
                    }
                }
            }
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("登録情報を破棄しますか？", "警告", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void send_info(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@id", Convert.ToInt32(ID_str));
                    command.Parameters.AddWithValue("@name", textName.Text);
                    command.Parameters.AddWithValue("@from", textFrom.Text);
                    command.Parameters.AddWithValue("@pc", textPostCo.Text);
                    command.Parameters.AddWithValue("@address", textAddress.Text);
                    command.Parameters.AddWithValue("@pn", textPhone.Text);
                    command.Parameters.AddWithValue("@bd", Convert.ToDateTime(BirthDay.Text));

                    command.ExecuteNonQuery();
                }
            }
            MessageBox.Show("ユーザー情報を登録しました", "成功");
            this.Close();
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            string sqlStr = "update UserInfoTable set [Name] = @name, [From] = @from, " +
                "PostCode = @pc, [Address] = @address, PhoneNo = @pn, BirthDay = @bd " +
                "where ID = @id";
            if (textName.Text == "" || textPostCo.Text == "" || textAddress.Text == "" || textPhone.Text == "" || BirthDay.Text == "")
            {
                DialogResult result = MessageBox.Show("空白の項目がありますが良いですか？", "警告", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    send_info(sqlStr);
                }
            }
            else
            {
                send_info(sqlStr);
            }
        }
    }
}
