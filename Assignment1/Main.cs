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
        private frmNewOrder No;
        private frmViewOrder Vo;
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
            No = new frmNewOrder(conn);  
            No.MdiParent = Main.ActiveForm;
            No.StartPosition = FormStartPosition.Manual;
            No.Location = new Point(0, 30);

            No.Show();

        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SendToBack();
            label1.SendToBack();

            // close the No form if it is not null and it is open
            if (No != null && !No.IsDisposed)
            {
                No.Close();
            }

            // close the Vo form if it is not null and it is open
            if (Vo != null && !Vo.IsDisposed)
            {
                Vo.Close();
            }

            frmLogin Login = new frmLogin();
            Login.MdiParent = Main.ActiveForm;
            Login.Show();
        }
    }
}
