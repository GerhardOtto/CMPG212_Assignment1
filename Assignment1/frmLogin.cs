using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment1
{
    public partial class frmLogin : Form
    {
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
            this.Close();
            frmStaff S = new frmStaff();
            S.MdiParent = Main.ActiveForm;
            S.Show();
        }
    }
}
