using System;
using System.Data;
using System.Windows.Forms;

namespace PBook89
{
    public partial class Form1 : Form
    {
        private int selectedID = -1;

        public Form1()
        {
            InitializeComponent();
        }

        private void clear()
        {
            textBox1.Text = ""; // PhoneNumber
            textBox2.Text = ""; // FullName
            textBox3.Text = ""; // Email
            textBox4.Text = ""; // Address
            selectedID = -1;
            textBox1.Focus();
        }

        // Display all records in grid
        private void displayAll()
        {
            DataTable dt = DbSql.DisplayAll();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["ID"].Visible = false; // hide ID column
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            displayAll();
        }

        private void button1_Click(object sender, EventArgs e) // Insert
        {
            long phone = Convert.ToInt64(textBox1.Text);

            // Check if phone already exists
            DataTable dtCheck = DbSql.SearchByPhone(phone);
            if (dtCheck.Rows.Count > 0)
            {
                MessageBox.Show("Phone number already exists!", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            DbSql.Insert(
                phone,
                textBox2.Text,
                textBox3.Text,
                textBox4.Text
            );
            MessageBox.Show("Contact inserted");
            displayAll();
            clear();
        }

        private void button2_Click(object sender, EventArgs e) // Delete
        {
            if (selectedID != -1)
            {
                DbSql.Delete(selectedID);
                MessageBox.Show("Contact deleted");
                displayAll();
                clear();
            }
            else
                MessageBox.Show("Select a contact first");
        }

        private void button3_Click(object sender, EventArgs e) // Update
        {
            if (selectedID != -1)
            {
                long phone = Convert.ToInt64(textBox1.Text);

                // Check if phone already exists in another record
                DataTable dtCheck = DbSql.SearchByPhone(phone);
                if (dtCheck.Rows.Count > 0 && Convert.ToInt32(dtCheck.Rows[0]["ID"]) != selectedID)
                {
                    MessageBox.Show("Phone number already exists in another contact!", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Focus();
                    return;
                }

                DbSql.UpdateByID(selectedID,
                    phone,
                    textBox2.Text,
                    textBox3.Text,
                    textBox4.Text
                );
                MessageBox.Show("Contact updated");
                displayAll();
                clear();
            }
            else
                MessageBox.Show("Select a contact first");
        }

        private void button4_Click(object sender, EventArgs e) // Search
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                DataTable dt = DbSql.SearchByPhone(Convert.ToInt64(textBox1.Text));
                dataGridView1.DataSource = dt;
                if (dt.Rows.Count == 0)
                    MessageBox.Show("No record found");
            }
            else
                MessageBox.Show("Enter Phone Number to search");
        }

        private void button6_Click(object sender, EventArgs e) // Clear
        {
            clear();
        }

        private void button5_Click(object sender, EventArgs e) // Exit
        {
            Application.Exit();
        }

        // When clicking a row: only show PhoneNumber in textbox1
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selectedID = Convert.ToInt32(row.Cells["ID"].Value);
                textBox1.Text = row.Cells["PhoneNumber"].Value.ToString();

                // Optional: clear other textboxes since only PhoneNumber should appear
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }
    }
}
