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

// 33706956
// Stefan Janse van Rensburg

namespace Assignment1
{
    public partial class frmStaff : Form
    {
        SqlConnection conn;
        SqlCommand command;
        SqlDataAdapter dataAdapter;
        SqlDataReader dataReader;
        DataSet ds;

        // connection to the database
        public string connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\OneDrive\OneDrive - North-West University\2023\CMPG212\Assignments\Assignment1\Assignment1\BeetleCaf.mdf;Integrated Security=True";
        //public string connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\stefa\OneDrive - North-West University\2023\CMPG212\Assignments\Assignment1\Assignment1\BeetleCaf.mdf;Integrated Security=True";


        private string dname;
        private int id;

        private void DisplayDrinks()
        {

            //function to display the drinks table
            string drinks = "SELECT * FROM Drinks";
            conn = new SqlConnection(connectionstring);

            try
            {
                conn.Open();

                command = new SqlCommand(drinks, conn);
                dataAdapter = new SqlDataAdapter();
                ds = new DataSet();

                dataAdapter.SelectCommand = command;
                dataAdapter.Fill(ds, "DataSource");

                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "DataSource";

                conn.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
                conn.Close();
            }
        }

        private void DisplayFood()
        {
            // function to display the food table
            string food = "SELECT * FROM Food";
            conn = new SqlConnection(connectionstring);

            try
            {
                conn.Open();

                command = new SqlCommand(food, conn);
                dataAdapter = new SqlDataAdapter();
                ds = new DataSet();

                dataAdapter.SelectCommand = command;
                dataAdapter.Fill(ds, "DataSource");

                dataGridView2.DataSource = ds;
                dataGridView2.DataMember = "DataSource";

                conn.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
                conn.Close();
            }
        }

        public frmStaff()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // The drinks table is updated when an Item is on special
            frmSpecial special = new frmSpecial();
            special.ShowDialog();

            conn = new SqlConnection(connectionstring);

            try
            {
                conn.Open();
                string sql = "UPDATE Drinks SET Special = @Special, Price = @Price WHERE Id = '" + id + "'";
                command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@Price", Convert.ToDecimal(special.fPrice));
                command.Parameters.AddWithValue("@Special", special.fSpecial);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
                conn.Close();
            }
            DisplayDrinks();
        }

        private void frmStaff_Load(object sender, EventArgs e)
        {
            // display drinks table in the datagridview


            DisplayDrinks();

            //display Food table in the datagridview

            DisplayFood();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAdd newDrink = new frmAdd();
            newDrink.ShowDialog();


            MessageBox.Show(newDrink.fName + " " + newDrink.fStock + "" + newDrink.fType + "" + newDrink.fPrice);


            conn = new SqlConnection(connectionstring);

            // random number generator to generate a unique ID based on the date and time the button was pressed
            Random random = new Random((int)DateTime.Now.Ticks);
            int randomNumber = random.Next(1, 1000000000);

            // new Item is added to the database
            // a form pops up for the user to add the new data
            try
            {
                conn.Open();

                string sql = "INSERT INTO Drinks(ID,Name, Stock, Type, Price) VALUES (@ID,@Name, @Stock, @Type, @Price)";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@ID", randomNumber);
                command.Parameters.AddWithValue("@Name", newDrink.fName);
                command.Parameters.AddWithValue("@Stock", Convert.ToInt32(newDrink.fStock));
                command.Parameters.AddWithValue("@Type", newDrink.fType);
                command.Parameters.AddWithValue("@Price", Convert.ToDecimal(newDrink.fPrice));
                command.ExecuteNonQuery();
                conn.Close();

            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
                conn.Close();
            }

            DisplayDrinks();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // the gridview selected item is deleted from the database
            conn = new SqlConnection(connectionstring);

            try
            {
                conn.Open();

                string sql = "DELETE FROM Drinks WHERE Id = " + id + "";
                command = new SqlCommand(sql, conn);
                dataAdapter = new SqlDataAdapter();

                dataAdapter.DeleteCommand = command;
                dataAdapter.DeleteCommand.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("deleted");
                DisplayDrinks();

            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
                conn.Close();
            }
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            frmReport report = new frmReport();
            report.Show();
        }


        // the item ID is captured into the id variable
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
        }

        private void btnUpFood_Click(object sender, EventArgs e)
        {
            frmSpecial special = new frmSpecial();
            special.ShowDialog();

            conn = new SqlConnection(connectionstring);

            try
            {
                conn.Open();
                string sql = "UPDATE Food SET Special = @Special, Price = @Price WHERE Id = '" + id + "'";
                command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@Price", Convert.ToDecimal(special.fPrice));
                command.Parameters.AddWithValue("@Special", special.fSpecial);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
                conn.Close();
            }
            DisplayFood();
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            // new Item is added to the database
            // a form pops up for the user to add the new data
            frmAdd New = new frmAdd();
            New.ShowDialog();

            Random random = new Random((int)DateTime.Now.Ticks);
            int randomNumber = random.Next(1, 1000000000);




            conn = new SqlConnection(connectionstring);


            string sql = "INSERT INTO Food(ID,Type, Name, Stock, Price) VALUES (@ID,@Type, @Name, @Stock, @Price)";
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@ID", randomNumber);
                command.Parameters.AddWithValue("@Name", New.fName);
                command.Parameters.AddWithValue("@Stock", Convert.ToInt32(New.fStock));
                command.Parameters.AddWithValue("@Type", New.fType);
                command.Parameters.AddWithValue("@Price", Convert.ToDecimal(New.fPrice));
                command.ExecuteNonQuery();
                conn.Close();

            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
                conn.Close();
            }
            DisplayFood();
        }

        private void btnDelFood_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(connectionstring);
            // item selected on the datagridview is deleted
            try
            {
                conn.Open();

                string sql = "DELETE FROM Food WHERE Id = '" + id + "'";
                command = new SqlCommand(sql, conn);
                dataAdapter = new SqlDataAdapter();

                dataAdapter.DeleteCommand = command;
                dataAdapter.DeleteCommand.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("deleted");
                DisplayFood();

            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
                conn.Close();
            }
        }


        // the item ID is captured into the id variable
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["Id"].Value);
        }
    }
}
