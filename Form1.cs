using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace voting_system
{
    public partial class Form1 : Form
    {
        readonly string cs = @"server=localhost;userid=root;password=;database=voting_system";
        MySqlConnection conn = null;
        MySqlDataReader read = null;


        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
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
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string enteredUsername = textBox1.Text;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(cs))
                {
                    conn.Open();

                    string query = "SELECT picture FROM student WHERE username = @username";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("@username", enteredUsername);

                    string imagePath = command.ExecuteScalar() as string;
                    // string imagePath = "C:\\Users\\johnv\\source\\repos\\voting_system\\bin\\Debug\\Images\\Group 32.png";

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


        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            try
            {
                MySqlCommand command;
                conn.Open();

                string query = "SELECT * FROM user WHERE username=@username AND password=@password";
                command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                read = command.ExecuteReader();

                if (read.Read())
                {
                    string role = read["role"].ToString();

                    if (role == "admin")
                    {
                        MessageBox.Show("You logged in as an admin.", "Login Successful");
                        this.Hide();
                        AdminDashboard admindash = new AdminDashboard();
                        admindash.Show();
                    }
                    else if (role == "student")
                    {
                        MessageBox.Show("You logged in as a student.", "Login Successful");
                        this.Hide();
                        StudentDashboard studentdash = new StudentDashboard();
                        studentdash.Show();

                        // Add code to open student form or perform student-specific actions
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed");
                }
            }
            catch (MySqlException ex)
            {
                MessageBoxButtons b = MessageBoxButtons.OK;
                MessageBox.Show($"Error: {ex}", "Error", b, MessageBoxIcon.Error);
            }
            finally
            {
                if (read != null) read.Close();
                if (conn != null && conn.State == ConnectionState.Open) conn.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
