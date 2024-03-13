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
using static System.Net.Mime.MediaTypeNames;

namespace LogInForm
{
    public partial class UserRegistForm : Form
    {
        // LogInFormからの値を格納するための変数
        private string ID_str;
        public string connectionString = "Data Source=localhost\\SQLEXPRESS;" +
            "Initial Catalog=myDB;" +
            "Integrated Security=SSPI;" +
            "TrustServerCertificate=true;";

        // 値を渡せるように引数を準備
        public UserRegistForm(string str)
        {
            InitializeComponent();
            ID_str = str;
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("登録情報を破棄しますか？", "警告", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void send_info(bool flag, string sql)
        {
            if (!flag)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand(sql, conn))
                    {
                        try
                        {
                            conn.Open();    // myDBのUserInfoTableとの接続

                            // パラメータの追加
                            command.Parameters.AddWithValue("@id", Convert.ToInt32(ID_str));
                            command.Parameters.AddWithValue("@name", textName.Text);
                            command.Parameters.AddWithValue("@from", textFrom.Text);
                            command.Parameters.AddWithValue("@pc", textPostCo.Text);
                            command.Parameters.AddWithValue("@ad", textAddress.Text);
                            command.Parameters.AddWithValue("@pn", textPhone.Text);
                            command.Parameters.AddWithValue("@bd", Convert.ToDateTime(BirthDay.Text));

                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("登録失敗","失敗");
                        }
                    }
                }
                MessageBox.Show("ユーザー情報の登録が完了しました");
                this.Close();
            }
            else
            {
                MessageBox.Show("ユーザー情報は既に登録されています");
            }
        }

        private void buttonRegist_Click(object sender, EventArgs e)
        {
            string sqlStr = "update UserInfoTable [Name] = @name, [From] = @from, PostCode = @pc," +
                " [Address] = @ad, PhoneNo = @pn, BirthDay = @bd where ID = @id";
            bool flag = false;
            string checkStr = "select count(*) as cnt from UserInfoTable where ID = " + Convert.ToInt32(ID_str);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(checkStr, conn))
                {
                    conn.Open();    //myDBのUserInfoTable内で該当するIDが存在するか

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (Convert.ToInt32(dr["cnt"]) > 0)
                            {
                                flag = true;
                            }
                        }
                    }
                }
            }
            if (textName.Text == "" || textPostCo.Text == "" || textAddress.Text == "" || textPhone.Text == "" || BirthDay.Text == "")
            {
                DialogResult result = MessageBox.Show("空白の項目がありますが良いですか？", "警告", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    send_info(flag, sqlStr);
                }
            }
            else
            {
                send_info(flag, sqlStr);
            }

        }
    }
}
