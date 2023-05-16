using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// 33706956
// Stefan Janse van Rensburg

namespace Assignment1
{
    public partial class frmAdd : Form
    {
        public string fType { get; set; }
        public string fName { get; set; }
        public string fStock { get; set; }
        public string fPrice { get; set; }

        public frmAdd()
        {
            InitializeComponent();
        }

        private void frmAdd_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // data is saved into public variables
            fType = tbxType.Text;
            fName = tbxName.Text;
            fStock = tbxStock.Text;
            fPrice = tbxPrice.Text;


            this.Close();

        }
    }
}
