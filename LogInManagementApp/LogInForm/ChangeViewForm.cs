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
    public partial class ChangeViewForm : Form
    {
        public static string _connectionString;
        public ChangeViewForm()
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
            var sql = "select Info.[Name] as ユーザー, 変更.変更日時 from 変更状況 as 変更" +
                " left outer join UserInfoTable as Info\r\n\ton 変更.[User] = Info.ID order by 変更.変更日時 desc;";
            DataTable dt = new DataTable();
            // データテーブルの型指定
            dt.Columns.Add("ユーザー", Type.GetType("System.String"));
            dt.Columns.Add("変更日時", Type.GetType("System.String"));
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
                        DateTime dm = Convert.ToDateTime(dr["変更日時"]);
                        string result = dm.ToString("yyyy/MM/dd HH時mm分ss秒");
                        row["変更日時"] = result;
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
