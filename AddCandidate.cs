using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace voting_system
{
    public partial class AddCandidate : Form
    {
        readonly string cs = @"server=localhost;userid=root;password=;database=voting_system";
        MySqlConnection conn = null;
        MySqlDataReader read = null;
        public AddCandidate()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.Show();
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string enteredID = textBox2.Text;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(cs))
                {
                    conn.Open();

                    string query = "SELECT picture FROM student WHERE studentID = @studentID";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("@studentID", enteredID);

                    string imagePath = command.ExecuteScalar() as string;

                    if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                    {
                        // Load and display the image in pictureBox1
                        pictureBox1.Image = Image.FromFile(imagePath);
                    }
                    else
                    {
                        // Image not found or path is empty
                        string defaultPic = "C:\\Users\\johnv\\source\\repos\\voting_system\\bin\\Debug\\Images\\fp.jpg";
                        pictureBox1.Image = Image.FromFile(defaultPic);
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Handle exceptions
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddCandidate_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
                conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBoxButtons b = MessageBoxButtons.OK;
                MessageBox.Show($"Connection error: {ex}", "Error", b, MessageBoxIcon.Error);
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(cs))
                {
                    conn.Open();

                    string query = "SELECT `studentID`, `studentName`, `sex`, `course` FROM student";
                    MySqlCommand command = new MySqlCommand(query, conn);

                    // Fetch the data into a DataTable
                    DataTable dataTable = new DataTable();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }

                    // Bind the DataTable to the DataGridView
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (MySqlException ex)
            {
                // Handle exceptions
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string studentID = textBox2.Text;
            string partyName = comboBox1.Text;
            string position = comboBox2.Text;

            // check if the inputs is complete
            if (textBox2.Text == string.Empty || 
                comboBox1.Text == string.Empty || 
                comboBox2.Text == string.Empty
               )
            {
                MessageBox.Show("Please input all the required input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the student ID exists
            string CheckEnrolled = $"SELECT COUNT(*) FROM student WHERE studentID = '{studentID}'";
            conn.Open();
            MySqlCommand commandCheckEnrolled = new MySqlCommand(CheckEnrolled, conn);
            int enrolledCount = Convert.ToInt32(commandCheckEnrolled.ExecuteScalar());
            conn.Close();
            if (enrolledCount == 0)
            {
                MessageBox.Show("This Student does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // Check if the student ID already registered as candidate
            string CheckStudent = $"SELECT COUNT(*) FROM candidate WHERE studentID = '{studentID}'";
            conn.Open();
            MySqlCommand commandCheckStudent = new MySqlCommand(CheckStudent, conn);
            int studentCount = Convert.ToInt32(commandCheckStudent.ExecuteScalar());
            conn.Close();
            if (studentCount > 0)
            {
                MessageBox.Show("Student already registered as a Candidate", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // Check if the position for the party already has a candidate
            string CheckPosition = $"SELECT COUNT(*) FROM candidate WHERE position = '{position}' AND partyName = '{partyName}'";
            conn.Open();
            MySqlCommand commandCheckPosition = new MySqlCommand(CheckPosition, conn);
            int positionCount = Convert.ToInt32(commandCheckPosition.ExecuteScalar());
            conn.Close();
            if (positionCount > 0)
            {
                MessageBox.Show("There is already a candidate for this position in the selected party.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // Insert into candidate table
            string qCandidate = $"INSERT INTO candidate (studentID, partyName, position) VALUES ('{studentID}', '{partyName}', '{position}')";
            conn.Open();
            MySqlCommand commandStudent = new MySqlCommand(qCandidate, conn);
            commandStudent.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("The record has been added", "Add Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Clear the textboxes, combo box, radio buttons, and reset the image path
            textBox2.Clear();
            comboBox2.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
        }
    }
}
