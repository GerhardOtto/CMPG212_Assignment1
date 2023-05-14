using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment1
{
    public partial class frmViewOrder : Form
    {
        
        private SqlConnection conn;
        SqlDataAdapter adapter;
        SqlDataReader reader;
        SqlCommand cmd;
        DataSet ds;
        public frmViewOrder(SqlConnection connection)
        {
            conn = connection;
            InitializeComponent();
        }

        public int getStock(string tableName, string name)
        {
            int oldStock = 0;
            try
            {
                conn.Open();

                string sql = $"SELECT Stock FROM {tableName} WHERE Name = '{name}'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    oldStock = Convert.ToInt32(reader.GetValue(0));
                }
                conn.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
            }

            return oldStock;
        }

        public void update(string tableName, int stock, string name)
        {
            try
            {
                int currentStock = getStock(tableName, name);
                int newStock = currentStock - stock;
                conn.Open();

                string updateQuery = $"UPDATE {tableName} SET Stock = @stock WHERE Name = @name";
                cmd = new SqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@stock", newStock);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
                conn.Close();
            }
        }

        public void DisplayOrder(string order)
        {
            listBox1.Items.Add(order);
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {

            foreach (var item in listBox1.Items)
            {
                string listItem = item.ToString();

                // Split the string into parts
                string[] parts = listItem.Split(':');

                // Get the category
                string category = parts[0].Trim();

                // Now split the second part by space
                string[] subParts = parts[1].Trim().Split(' ');

                // Parse the first part to an integer
                int amount = Int32.Parse(subParts[0]);

                // Get the string part
                string name = subParts[1].Split('/')[0]; // split on "/" and take the first part


                if (category == "Drinks")
                {
                    update(category,amount, name);
                }
                else
                {
                    update("Food",amount, name);
                }
            }

            MessageBox.Show("Order placed!");
            listBox1.Items.Clear();
            this.Hide();
        }

        private void frmViewOrder_Load(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }
    }
}
