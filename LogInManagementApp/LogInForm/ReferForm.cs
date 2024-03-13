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
    public partial class ReferForm : Form
    {
        public static string _connectionString;
        public ReferForm()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost\\SQLEXPRESS";
            builder.InitialCatalog = "myDB";
            builder.IntegratedSecurity = true;
            _connectionString = builder.ToString();
            dataGridView1.DataSource = GetDataTable();
        }

        public static DataTable GetDataTable()
        {
            var sql = "select Info.[Name] as ユーザー, ログ.ログイン日時, ログ.ログイン成功" + 
                " from ログイン履歴 as ログ left outer join UserInfoTable as Info" + 
                " on ログ.[User] = Info.ID order by ログ.ログイン日時 desc";           
            DataTable dt = new DataTable();
            // データテーブルの型指定
            dt.Columns.Add("ユーザー", Type.GetType("System.String"));
            dt.Columns.Add("ログイン日時", Type.GetType("System.String"));
            dt.Columns.Add("ログイン成功", Type.GetType("System.String"));
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DataRow row = dt.NewRow();
                        row["ユーザー"] = dr["ユーザー"];
                        // DateTime型をString型に変更してDataGridViewに表示
                        // C#側はログイン日時が何型かわからない
                        DateTime dm = Convert.ToDateTime(dr["ログイン日時"]);
                        string result = dm.ToString("yyyy/MM/dd HH時mm分ss秒");
                        row["ログイン日時"] = result;
                        row["ログイン成功"] = dr["ログイン成功"];
                        dt.Rows.Add(row);
                    }
                }
                return dt;
            }
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
