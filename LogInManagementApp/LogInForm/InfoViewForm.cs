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
    public partial class InfoViewForm : Form
    {
        public static string _connectionString;

        public InfoViewForm()
        {
            InitializeComponent();
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost\\SQLEXPRESS";
            builder.InitialCatalog = "myDB";
            builder.IntegratedSecurity = true;
            _connectionString = builder.ToString();
            dataGridView1.DataSource = GetDataTable();
        }

        public static DataTable GetDataTable()
        {
            var sql = @"select [Name], [From],PostCode, [Address], PhoneNo, BirthDay from UserInfoTable" +
                 " where ISNUMERIC(Name) = 0";
            DataTable dt = new DataTable();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var adapter = new SqlDataAdapter(sql, connection))
                {
                    connection.Open();
                    adapter.Fill(dt);
                }
                return dt;
            }
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetDataTable();
        }
    }
}
