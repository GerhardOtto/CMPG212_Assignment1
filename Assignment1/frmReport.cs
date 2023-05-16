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
            decimal Total = 0;
            string sql = "SELECT ThisCantBeWhatItMustBe, Category, Name, Sales, Profit FROM Sales ORDER BY ThisCantBeWhatItMustBe";

            conn = new SqlConnection(connectionstring);

            try
            {
                conn.Open();
                command = new SqlCommand(sql, conn);
                dataReader = command.ExecuteReader();

                string currentDate = "";
                while (dataReader.Read())
                {
                    string ThisCantBeWhatItMustBe = Convert.ToDateTime(dataReader.GetValue(0)).ToShortDateString();

                    if (ThisCantBeWhatItMustBe != currentDate)
                    {
                        if (currentDate != "")
                        {
                            lbxReports.Items.Add("");
                            lbxReports.Items.Add("Total Profit for " + currentDate + ": " + Total.ToString());
                            Total = 0; // reset total for next date
                        }
                        currentDate = ThisCantBeWhatItMustBe;

                        lbxReports.Items.Add("");
                        lbxReports.Items.Add("Sale Date: " + currentDate);
                        lbxReports.Items.Add("Category \t\t Name \t\t Sales \t Profit");
                        lbxReports.Items.Add("=====================================================================================");
                    }

                    lbxReports.Items.Add(dataReader.GetValue(1) + "\t" + dataReader.GetValue(2) + "\t" + dataReader.GetValue(3).ToString() + "\t" + dataReader.GetValue(4).ToString());
                    Total = Total + Convert.ToDecimal(dataReader.GetValue(4));
                }

                // Add the total for the last date
                if (currentDate != "")
                {
                    lbxReports.Items.Add("");
                    lbxReports.Items.Add("Total Profit for " + currentDate + ": R" + Total.ToString());
                }
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
                conn.Close();
            }

        }
    }
}
