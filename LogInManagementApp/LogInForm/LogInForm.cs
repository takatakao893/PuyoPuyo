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
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace LogInForm
{
    public partial class LogInForm : Form
    {
        public string connectionString = "Data Source=localhost\\SQLEXPRESS;" +
            "Initial Catalog=myDB;" +
            "Integrated Security=SSPI;" +
            "TrustServerCertificate=true;";
        public LogInForm()
        {
            InitializeComponent();
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            bool flag = false;
            if (textID.Text == "" || textPass.Text == "")
            {
                MessageBox.Show("IDとパスワード両方入力してください","警告");
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
                                    flag = true;
                                }
                            }
                        }
                    }
                }
                if (!flag)
                {
                    MessageBox.Show("ユーザー登録をしてください", "警告");
                }
                else
                {
                    bool flagPass = false;
                    string checkPass = "select Pass from UserMaster where ID = " + Convert.ToInt32(textID.Text);
                    byte[] beforeByteArray = Encoding.UTF8.GetBytes(textPass.Text);

                    // SHA1 ハッシュ値を計算する
                    SHA1 sha1 = SHA1.Create();
                    byte[] afterByteArray = sha1.ComputeHash(beforeByteArray);
                    sha1.Clear();

                    // バイト配列を16進数文字列に変換
                    StringBuilder sb1 = new StringBuilder();
                    foreach(byte b in afterByteArray)
                    {
                        sb1.Append(b.ToString("x2"));
                    }
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using (var command = new SqlCommand(checkPass, conn))
                        {
                            conn.Open();    //myDBからPassを持ってきくる

                            using (SqlDataReader dr = command.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    if (Convert.ToString(dr["Pass"]) == Convert.ToString(sb1))
                                    {
                                        flagPass = true;
                                    }
                                }
                            }
                        }
                    }
                    //IDとPassどちらも正しい場合
                    if (flagPass)
                    {
                        string sqlStr = "insert into ログイン履歴([User], ログイン日時, ログイン成功) values"+
                            "(@user, CURRENT_TIMESTAMP, @login)";
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            using (var command = new SqlCommand(sqlStr, conn))
                            {
                                conn.Open();
                                // パラメータの追加
                                command.Parameters.AddWithValue("@user", textID.Text);
                                command.Parameters.AddWithValue("@login", "  〇");
                                command.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show("ユーザー情報管理画面へ遷移","画面遷移");
                        this.Close();
                        // UserInfoFormのインスタンスを生成
                        UserInfoForm form = new UserInfoForm(textID.Text);
                        // formを表示
                        form.ShowDialog();

                    }
                    else
                    {
                        string sqlStr = "insert into ログイン履歴([User], ログイン日時, ログイン成功) values(" +
                            "@user, CURRENT_TIMESTAMP, @login)";
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            using (var command = new SqlCommand(sqlStr, conn))
                            {
                                conn.Open();
                                // パラメータの追加 UserInfoTableに追加されていない情報を持ってくるから空
                                command.Parameters.AddWithValue("@user", textID.Text);
                                command.Parameters.AddWithValue("@login", "  ×");
                                command.ExecuteNonQuery();
                            }
                        }
                        //IDは正しいが、Passは間違っている場合
                        MessageBox.Show("正しいパスワードを入力してください","警告");
                    }
                }
            }
        }
    }
}
