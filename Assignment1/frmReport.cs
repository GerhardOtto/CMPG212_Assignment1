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

namespace Assignment1
{
    public partial class frmReport : Form
    {
        SqlConnection conn;
        SqlCommand command;
        SqlDataReader dataReader;

        public string connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gerhard\projects\Assignment1_Group\Assignment1V1\Assignment1\Database1.mdf;Integrated Security=True";

        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            lbxReports.Items.Clear();
            // data is retreived from the sales table and displayed in the listbox
            // the total profit is calculated
            decimal Total = 0;
            string sql = "SELECT Category,Name,Sales,Profit FROM Sales";

            conn = new SqlConnection(connectionstring);

            try
            {
                conn.Open();
                command = new SqlCommand(sql, conn);
                dataReader = command.ExecuteReader();

                lbxReports.Items.Add("Category \t\t Name \t\t Sales \t Profit");
                lbxReports.Items.Add("=====================================================================================");

                while (dataReader.Read())
                {
                    lbxReports.Items.Add(dataReader.GetValue(0) + "\t" + dataReader.GetValue(1) + "\t" + dataReader.GetValue(2).ToString() + "\t" + dataReader.GetValue(3).ToString());
                    Total = Total + Convert.ToDecimal(dataReader.GetValue(3));
                }
                lbxReports.Items.Add("");
                lbxReports.Items.Add("Total Profit: " + Total.ToString());

            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
                conn.Close();
            }
        }
    }
}
