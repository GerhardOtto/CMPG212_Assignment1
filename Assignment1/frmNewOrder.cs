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
    public partial class frmNewOrder : Form
    {
        private SqlConnection conn;
        private frmViewOrder Vo;

        SqlDataAdapter adapter;
        SqlDataReader reader;
        SqlCommand cmd;
        DataSet ds;


        //Reads data from DB into a comboBox using data reader
        private void comboReader(string sql, ComboBox cbxName)
        {
            try
            {
                cbxName.Items.Clear();

                conn.Open();

                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int stockLeft = reader.GetInt32(0);
                    int maxRange = Math.Min(stockLeft, 10);

                    for (int i = 1; i <= maxRange; i++)
                    {
                        cbxName.Items.Add(i);
                    }
                }

                conn.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
                conn.Close();
            }
        }


        //Displays the data in datagridview
        private void display(string sqlString, DataGridView dataGridView)
        {
            try
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlString, conn);
                adapter = new SqlDataAdapter();
                ds = new DataSet();

                adapter.SelectCommand = sqlCommand;
                adapter.Fill(ds, "MyData");

                dataGridView.DataSource = ds;
                dataGridView.DataMember = "MyData";
                conn.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
                conn.Close();

            }
        }

        public frmNewOrder(SqlConnection connection)
        {
            conn = connection;
            InitializeComponent();
        }

        private void frmNewOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            
            
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {

        }

        private void frmNewOrder_Load(object sender, EventArgs e)
        {
            Vo = new frmViewOrder(conn);
            Vo.MdiParent = Main.ActiveForm;
            Vo.StartPosition = FormStartPosition.Manual;
            Vo.Location = new Point(394, 36);

            string sql = "SELECT Name, Price FROM Drinks";
            display(sql, dataGridView1);

            sql = "SELECT Name, Price FROM Food WHERE Type = 'Pastry'";
            display(sql, dataGridView3);

            sql = "SELECT Name, Price FROM Food WHERE Type = 'Sandwich'";
            display(sql, dataGridView2);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex <= -1)
            {
                MessageBox.Show("No amount of drinks were selected!");
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            string name = selectedRow.Cells[0].Value.ToString();
            double costPerItem = Convert.ToDouble(selectedRow.Cells[1].Value);

            int amount = Convert.ToInt32(comboBox1.SelectedItem.ToString());
            string display ="Drinks: " + amount + " " + name + "/s for R" + Math.Round(amount * costPerItem, 2);

            Vo.Show();
            Vo.DisplayOrder(display);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex <= -1)
            {
                MessageBox.Show("No amount of sandwiches were selected!");
                return;
            }

            DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];

            string name = selectedRow.Cells[0].Value.ToString();
            double costPerItem = Convert.ToDouble(selectedRow.Cells[1].Value);

            int amount = Convert.ToInt32(comboBox2.SelectedItem.ToString());
            string display ="Sandwich: " + amount + " " + name + "/s for R" + Math.Round(amount * costPerItem, 2);

            Vo.Show();
            Vo.DisplayOrder(display);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex <= -1)
            {
                MessageBox.Show("No amount of pasteries were selected!");
                return;
            }

            DataGridViewRow selectedRow = dataGridView3.SelectedRows[0];

            string name = selectedRow.Cells[0].Value.ToString();
            double costPerItem = Convert.ToDouble(selectedRow.Cells[1].Value);

            int amount = Convert.ToInt32(comboBox3.SelectedItem.ToString());
            string display = "Pastry: " + amount + " " + name + "/s for R" + Math.Round(amount * costPerItem, 2);

            Vo.Show();
            Vo.DisplayOrder(display);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            string name = selectedRow.Cells[0].Value.ToString();

            string sql = $"SELECT Stock FROM Drinks WHERE Name = '{name}'";
            comboReader(sql, comboBox1);

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];

            string name = selectedRow.Cells[0].Value.ToString();

            string sql = $"SELECT Stock FROM Food WHERE Name = '{name}' ";
            comboReader(sql, comboBox2);
            
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView3.SelectedRows[0];

            string name = selectedRow.Cells[0].Value.ToString();

            string sql = $"SELECT Stock FROM Food WHERE Name = '{name}'";
            comboReader(sql, comboBox3);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
