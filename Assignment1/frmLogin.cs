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
    public partial class frmLogin : Form
    {
        SqlConnection conn;
        SqlCommand command;

        SqlDataReader dataReader;



        // public connection
        public string connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gerhard\projects\Assignment1_Group\Assignment1V1\Assignment1\Database1.mdf;Integrated Security=True";

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            tbxPassword.UseSystemPasswordChar = true;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            bool displayFrm = false;
            // checks to see if the username and password entered match the username and password int the database
            string Un, Pw;

            Un = tbxUsername.Text;
            Pw = tbxPassword.Text;

            conn = new SqlConnection(connectionstring);
            string sql = "SELECT * FROM Login WHERE  Username = '" + Un + " ' " + "AND Password = '" + Pw + "'";

            try
            {
                conn.Open();

                command = new SqlCommand(sql, conn);
                dataReader = command.ExecuteReader();



                while (dataReader.Read())
                {
                    if (dataReader.GetValue(1).ToString() == "")
                    {
                        MessageBox.Show("invalid username or password");
                        displayFrm = false;
                    }
                    else
                    {
                        displayFrm = true;
                    }
                }

                this.Close();
                frmStaff S = new frmStaff();
                S.MdiParent = Main.ActiveForm;
                S.Show();

                conn.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
                conn.Close();
            }
        }
    }
}
