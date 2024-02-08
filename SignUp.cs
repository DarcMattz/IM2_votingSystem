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
using System.Xml.Linq;

namespace voting_system
{
    public partial class SignUp : Form
    {
        readonly string cs = @"server=localhost;userid=root;password=;database=voting_system";
        MySqlConnection conn = null;
        MySqlDataReader read = null;
        private string imagePath = "";

        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_Load(object sender, EventArgs e)
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
        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //checking if the input is complete
            if (string.IsNullOrWhiteSpace(textBox1.Text) || 
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) || 
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text)
               )
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            /*string defaultPic = "C:\\Users\\johnv\\source\\repos\\voting_system\\bin\\Debug\\Images\\fp.jpg";
            if (pictureBox1.Image == null && pictureBox1.Image.Tag == null && pictureBox1.Image.Tag.ToString() == defaultPic)
            {
                MessageBox.Show("Please upload an image.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/

            //decleration

            MySqlCommand command0;
            MySqlCommand command1;
            MySqlCommand command2;

            string studentID = textBox1.Text;
            string studentName = textBox2.Text;
            string course = textBox3.Text;
            string username = textBox4.Text;
            string password = textBox5.Text;
            string sex = radioButton1.Checked ? radioButton1.Text : radioButton2.Text;

            //validation for studentID
            string checkID = $"SELECT COUNT(*) FROM student WHERE studentID = '{studentID}'";
            conn.Open();
            command0 = new MySqlCommand(checkID, conn);
            int countID = Convert.ToInt32(command0.ExecuteScalar());
            conn.Close();

            if (countID > 0)
            {
                MessageBox.Show("This StudentID is already registered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //validation for username
            string checkUsername = $"SELECT COUNT(*) FROM user WHERE username = '{username}'";
            conn.Open();
            command0 = new MySqlCommand(checkUsername, conn);
            int countUsername = Convert.ToInt32(command0.ExecuteScalar());
            conn.Close();

            if (countUsername > 0)
            {
                MessageBox.Show("Username already exists. Please choose a different username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //inserting data
            string insertStudent = $"INSERT INTO student(studentID, studentName, sex, course, username, password, picture) VALUES ('{studentID}','{studentName}','{sex}','{course}','{username}','{password}','{imagePath}')";
            string insertUser = $"INSERT INTO user(username,password,role) VALUES ('{username}','{password}','student')";
            conn.Open();
            command1 = new MySqlCommand(insertStudent, conn);
            command2 = new MySqlCommand(insertUser, conn);
            command1.ExecuteNonQuery();
            command2.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Successfully Registered", "Add Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string sourceImagePath = openFileDialog.FileName;
                string imageName = Path.GetFileName(sourceImagePath);
                string destinationFolderPath = "C:\\\\\\\\Users\\\\\\\\johnv\\\\\\\\source\\\\\\\\repos\\\\\\\\voting_system\\\\\\\\bin\\\\\\\\Debug\\\\\\\\Images\\\\\\\\";
                string destinationImagePath = Path.Combine(destinationFolderPath, imageName);

                // Check if the destination folder exists, create it if not
                if (!Directory.Exists(destinationFolderPath))
                {
                    Directory.CreateDirectory(destinationFolderPath);
                }

                // Copy the selected image to the project's "Images" folder if it doesn't already exist
                if (!File.Exists(destinationImagePath))
                {
                    File.Copy(sourceImagePath, destinationImagePath);
                }

                // Display the copied image in pictureBox1
                pictureBox1.Image = Image.FromFile(destinationImagePath);

                // Store the image path for later use (e.g., when saving to the database)
                imagePath = destinationImagePath;
            }
        }

    }
}
