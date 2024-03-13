using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LogInForm
{
    public partial class RegistForm : Form
    {
        public string connectionString = "Data Source=localhost\\SQLEXPRESS;" +
            "Initial Catalog=myDB;" +
            "Integrated Security=SSPI;" +
            "TrustServerCertificate=true;";
        public RegistForm()
        {
            InitializeComponent();
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonRegist_Click(object sender, EventArgs e)
        {
            if (textID.Text == "" || textPass.Text == "")
            {
                MessageBox.Show("ログインIDとパスワード両方入力してください","警告");
            }
            else
            {
                try
                { 
                    string sqlStr = "insert into UserMaster(ID, Pass) values(@id,@pass);";
                    string infStr = "insert into UserInfoTable(ID, [Name], [From], PostCode, [Address], PhoneNo, BirthDay)" +
                        " values(@id, @name, '', '', '', '', '')";
                    byte[] beforeByteArray = Encoding.UTF8.GetBytes(textPass.Text);

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
                        using (var command = new SqlCommand(sqlStr, conn))
                        {
                            conn.Open();    //myDBのUserMasterとの接続

                            // パラメータの追加
                            command.Parameters.AddWithValue("@id", Convert.ToInt32(textID.Text));
                            command.Parameters.AddWithValue("@pass", Convert.ToString(sb1));

                            command.ExecuteNonQuery();
                        }
                    }
                    // UserInfoTableにデータを挿入
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using (var command = new SqlCommand(infStr, conn))
                        {
                            conn.Open();

                            // パラメータの追加
                            command.Parameters.AddWithValue("@id", Convert.ToInt32(textID.Text));
                            command.Parameters.AddWithValue("@name", textID.Text);
                            command.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("ユーザーの登録が完了しました","登録完了");
                    this.Close();
                }
                catch (Exception ex) {
                    MessageBox.Show("ユーザーは既に登録されています", "登録済み");
                }
            }

        }
    }
}
