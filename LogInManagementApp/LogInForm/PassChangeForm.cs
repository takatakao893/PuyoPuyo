using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogInForm
{
    public partial class PassChangeForm : Form
    {
        public string connectionString = "Data Source=localhost\\SQLEXPRESS;" +
            "Initial Catalog=myDB;" +
            "Integrated Security=SSPI;" +
            "TrustServerCertificate=true;";
        public PassChangeForm()
        {
            InitializeComponent();
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            bool ExistID = false;
            bool ExistPS = false;
            if (textID.Text == "" || textPass1.Text == "" || textPass2.Text == "")
            {
                MessageBox.Show("IDと新しいパスワードと確認用パスワード3つ入力してください", "警告");
            }
            else
            {
                string checkStr = "select count(*) as cnt from UserMaster where ID = " + Convert.ToInt32(textID.Text);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand(checkStr, conn))
                    {
                        conn.Open();    //myDBのUserMaster内で該当するIDが存在するか

                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (Convert.ToInt32(dr["cnt"]) > 0)
                                {
                                    ExistID = true;
                                }
                            }
                        }
                    }
                }
                if (!ExistID)
                {
                    MessageBox.Show("ユーザー登録をしてください。", "警告");
                }
                else
                {
                    if (textPass1.Text != textPass2.Text)
                    {
                        MessageBox.Show("新しいパスワードと確認用パスワードが一致しません", "警告");
                    }
                    else
                    {
                        string sqlStr = "update UserMaster set Pass = @pass where ID = @id";
                        string cheStr = "select Pass from UserMaster where ID = @id";
                        string logStr = "insert into 変更状況([User], 変更日時) values(@user, CURRENT_TIMESTAMP)";

                        byte[] beforeByteArray = Encoding.UTF8.GetBytes(textPass1.Text);

                        // SHA1 ハッシュ値を計算する
                        SHA1 sha1 = SHA1.Create();
                        byte[] afterByteArray = sha1.ComputeHash(beforeByteArray);
                        sha1.Clear();
                        // バイト配列を16進数文字列に変換
                        StringBuilder sb1 = new StringBuilder();
                        foreach (byte b in afterByteArray)
                        {
                            sb1.Append(b.ToString("x2"));
                        }
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            using (var command = new SqlCommand(cheStr, conn))
                            {
                                conn.Open();    //myDBのUserMaster内以前使ったパスワードがあるか
                                // パラメータの追加
                                command.Parameters.AddWithValue("@id", Convert.ToInt32(textID.Text));

                                using (SqlDataReader dr = command.ExecuteReader())
                                {
                                    while (dr.Read())
                                    {
                                        if (Convert.ToString(dr["Pass"]) == Convert.ToString(sb1))
                                        {
                                            ExistPS = true;
                                        }
                                    }
                                }
                                command.ExecuteNonQuery();
                            }
                        }
                        if (!ExistPS)
                        {
                            DialogResult result = MessageBox.Show("パスワードを変更しますか？", "変更", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                using (SqlConnection conn = new SqlConnection(connectionString))
                                {
                                    using (var command = new SqlCommand(sqlStr, conn))
                                    {
                                        conn.Open();    //myDBのUseMaster内のPassを変更
                                                        // パラメータの追加
                                        command.Parameters.AddWithValue("@id", Convert.ToInt32(textID.Text));
                                        command.Parameters.AddWithValue("@pass", Convert.ToString(sb1));

                                        command.ExecuteNonQuery();
                                    }
                                }
                                using (SqlConnection conn = new SqlConnection(connectionString))
                                {
                                    using (var command = new SqlCommand(logStr, conn))
                                    {
                                        conn.Open();   //myDBの変更状況にアクセス
                                        // パラメータの追加
                                        command.Parameters.AddWithValue("@user", textID.Text);
                                        command.ExecuteNonQuery();
                                    }
                                }
                                MessageBox.Show("パスワードの変更ができました", "成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("前のパスワードは使用しないでください", "警告");
                        }
                    }
                }
            }
        }
    }
}
