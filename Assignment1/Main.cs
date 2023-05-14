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
using System.Xml.Linq;

namespace Assignment1
{
    public partial class Main : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Gerhard\projects\Assignment1_Group\Assignment1V1\Assignment1\Database1.mdf;Integrated Security=True");
        SqlDataAdapter adapter;
        SqlDataReader reader;
        SqlCommand cmd;
        DataSet ds;

        private void delete(string DeleteMe, ListBox lbxName)
        {

            try
            {
                DeleteMe = lbxName.SelectedItem.ToString();

                conn.Open();

                string sqlString = "DELETE FROM Movies WHERE MovieName = @MovieName";
                cmd = new SqlCommand(sqlString, conn);
                cmd.Parameters.AddWithValue("@MovieName", DeleteMe);
                cmd.ExecuteNonQuery();

                conn.Close();
                MessageBox.Show(DeleteMe.ToString() + " has been deleted");
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
                conn.Close();
            }

        }

        public Main()
        {
            InitializeComponent();
        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.SendToBack();
            pictureBox1.SendToBack();
            frmNewOrder No = new frmNewOrder(conn);
            No.MdiParent = Main.ActiveForm;
            No.StartPosition = FormStartPosition.Manual;
            No.Location = new Point(0, 30);

            No.Show();
          
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SendToBack();
            label1.SendToBack();
            frmLogin Login = new frmLogin();
            Login.MdiParent = Main.ActiveForm;
            Login.Show();
        }
    }
}
