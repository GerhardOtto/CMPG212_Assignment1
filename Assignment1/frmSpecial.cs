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
    public partial class frmSpecial : Form
    {
        public frmSpecial()
        {
            InitializeComponent();
        }

        public object fPrice { get; set; }
        public object fSpecial { get; set; }

        private void frmSpecial_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // checks if the checkboxes are ticked and save data into public variables
            if (rdoYes.Checked)
            {

                fSpecial = rdoYes.Text;
                fPrice = textBox1.Text;
            }
            else if (rdoNo.Checked)
            {

                fSpecial = rdoNo.Text;
                fPrice = textBox1.Text;
            }
            else
            {
                MessageBox.Show("Please select if the Item is on special or not");
            }

            this.Close();
        }
    }
}
