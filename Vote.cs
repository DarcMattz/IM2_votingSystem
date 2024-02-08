using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace voting_system
{
    public partial class Vote : Form
    {

        readonly string cs = @"server=localhost;userid=root;password=;database=voting_system";
        MySqlConnection conn = null;
        MySqlDataReader read = null;


        private void LoadCandidateInfo(ComboBox comboBox, Label label, PictureBox pictureBox, string position)
        {
            try
            {
                string selectedStudentName = comboBox.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(selectedStudentName))
                    return;

                string query = $"SELECT candidate.partyName, student.picture " +
                               $"FROM candidate " +
                               $"INNER JOIN student ON candidate.studentID = student.studentID " +
                               $"WHERE student.studentName = '{selectedStudentName}' " +
                               $"AND candidate.position = '{position}'";

                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string partyName = reader["partyName"].ToString();
                    string picturePath = reader["picture"].ToString();

                    label.Text = $"Party: {partyName}";
                    label.Show();

                    if (!string.IsNullOrEmpty(picturePath) && File.Exists(picturePath))
                    {
                        pictureBox.Image = Image.FromFile(picturePath);
                    }
                    else
                    {
                        MessageBox.Show("Image not found or path is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No candidate found for the selected student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string userData;

        // Constructor that accepts data from Form1
        public Vote()
        {
            InitializeComponent(); // Assign the received data to a local variable in Form2
        }

        private void Vote_Load(object sender, EventArgs e)
        {
            // hiding all party label
            label11.Hide();
            label12.Hide();
            label13.Hide();
            label14.Hide();
            label15.Hide();
            label16.Hide();
            label17.Hide();
            label18.Hide();

            // for db connection
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


            // governor dropdown value
            try
            {
                string query = "SELECT candidate.studentID, student.studentName " +
                               "FROM candidate " +
                               "INNER JOIN student ON candidate.studentID = student.studentID " +
                               "WHERE candidate.position = 'Governor'";

                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string studentID = reader["studentID"].ToString();
                    string studentName = reader["studentName"].ToString();

                    // Add studentName to comboBox1
                    comboBox1.Items.Add(studentName);
                }

                conn.Close();
            }
            catch (MySqlException ex)
            {
                // Handle exceptions
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            // vice governor dropdown value
            try
            {
                string query = "SELECT candidate.studentID, student.studentName " +
                               "FROM candidate " +
                               "INNER JOIN student ON candidate.studentID = student.studentID " +
                               "WHERE candidate.position = 'Vice-Governor'";

                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string studentID = reader["studentID"].ToString();
                    string studentName = reader["studentName"].ToString();

                    // Add studentName to comboBox1
                    comboBox2.Items.Add(studentName);
                }

                conn.Close();
            }
            catch (MySqlException ex)
            {
                // Handle exceptions
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            // secretary dropdown value
            try
            {
                string query = "SELECT candidate.studentID, student.studentName " +
                               "FROM candidate " +
                               "INNER JOIN student ON candidate.studentID = student.studentID " +
                               "WHERE candidate.position = 'Secretary'";

                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string studentID = reader["studentID"].ToString();
                    string studentName = reader["studentName"].ToString();

                    // Add studentName to comboBox1
                    comboBox3.Items.Add(studentName);
                }

                conn.Close();
            }
            catch (MySqlException ex)
            {
                // Handle exceptions
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            // Treasurer dropdown value
            try
            {
                string query = "SELECT candidate.studentID, student.studentName " +
                               "FROM candidate " +
                               "INNER JOIN student ON candidate.studentID = student.studentID " +
                               "WHERE candidate.position = 'Treasurer'";

                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string studentID = reader["studentID"].ToString();
                    string studentName = reader["studentName"].ToString();

                    // Add studentName to comboBox1
                    comboBox4.Items.Add(studentName);
                }

                conn.Close();
            }
            catch (MySqlException ex)
            {
                // Handle exceptions
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




            // Auditor dropdown value
            try
            {
                string query = "SELECT candidate.studentID, student.studentName " +
                               "FROM candidate " +
                               "INNER JOIN student ON candidate.studentID = student.studentID " +
                               "WHERE candidate.position = 'Auditor'";

                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string studentID = reader["studentID"].ToString();
                    string studentName = reader["studentName"].ToString();

                    // Add studentName to comboBox1
                    comboBox5.Items.Add(studentName);
                }

                conn.Close();
            }
            catch (MySqlException ex)
            {
                // Handle exceptions
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            // BusinessManager dropdown value
            try
            {
                string query = "SELECT candidate.studentID, student.studentName " +
                               "FROM candidate " +
                               "INNER JOIN student ON candidate.studentID = student.studentID " +
                               "WHERE candidate.position = 'Business Manager'";

                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string studentID = reader["studentID"].ToString();
                    string studentName = reader["studentName"].ToString();

                    // Add studentName to comboBox1
                    comboBox6.Items.Add(studentName);
                }

                conn.Close();
            }
            catch (MySqlException ex)
            {
                // Handle exceptions
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




            // Public Relations Officer dropdown value
            try
            {
                string query = "SELECT candidate.studentID, student.studentName " +
                               "FROM candidate " +
                               "INNER JOIN student ON candidate.studentID = student.studentID " +
                               "WHERE candidate.position = 'Public Relations Officer'";

                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string studentID = reader["studentID"].ToString();
                    string studentName = reader["studentName"].ToString();

                    // Add studentName to comboBox1
                    comboBox7.Items.Add(studentName);
                }

                conn.Close();
            }
            catch (MySqlException ex)
            {
                // Handle exceptions
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            // Technical Officer dropdown value
            try
            {
                string query = "SELECT candidate.studentID, student.studentName " +
                               "FROM candidate " +
                               "INNER JOIN student ON candidate.studentID = student.studentID " +
                               "WHERE candidate.position = 'Technical Officer'";

                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string studentID = reader["studentID"].ToString();
                    string studentName = reader["studentName"].ToString();

                    // Add studentName to comboBox1
                    comboBox8.Items.Add(studentName);
                }

                conn.Close();
            }
            catch (MySqlException ex)
            {
                // Handle exceptions
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCandidateInfo(comboBox1, label11, pictureBox1, "Governor");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCandidateInfo(comboBox2, label12, pictureBox2, "Vice-Governor");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCandidateInfo(comboBox3, label13, pictureBox3, "Secretary");
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCandidateInfo(comboBox4, label14, pictureBox4, "Treasurer");
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCandidateInfo(comboBox5, label15, pictureBox5, "Auditor");
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCandidateInfo(comboBox6, label16, pictureBox6, "Business Manager");
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCandidateInfo(comboBox7, label17, pictureBox7, "Public Relations Officer");
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCandidateInfo(comboBox8, label18, pictureBox8, "Technical Officer");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int electionID = 1; // Replace this with the actual election ID
                var studentID = "21-UR-0150";

                // Get the selected student IDs based on user selection from the ComboBoxes
                string selectedStudentID1 = GetStudentIDFromComboBox(comboBox1);
                string selectedStudentID2 = GetStudentIDFromComboBox(comboBox2);
                string selectedStudentID3 = GetStudentIDFromComboBox(comboBox3);
                string selectedStudentID4 = GetStudentIDFromComboBox(comboBox4);
                string selectedStudentID5 = GetStudentIDFromComboBox(comboBox5);
                string selectedStudentID6 = GetStudentIDFromComboBox(comboBox6);
                string selectedStudentID7 = GetStudentIDFromComboBox(comboBox7);
                string selectedStudentID8 = GetStudentIDFromComboBox(comboBox8);
                // Repeat this for all the other ComboBoxes for different positions

                string position1 = "Governor";
                string position2 = "Vice-Governor";
                string position3 = "Secretary";
                string position4 = "Treasurer";
                string position5 = "Auditor";
                string position6 = "Business Manager";
                string position7 = "Public Relations Officer";
                string position8 = "Technical Officer";


                int candidateID1 = GetCandidateID(selectedStudentID1, position1);
                int candidateID2 = GetCandidateID(selectedStudentID2, position2);
                int candidateID3 = GetCandidateID(selectedStudentID3, position3);
                int candidateID4 = GetCandidateID(selectedStudentID4, position4);
                int candidateID5 = GetCandidateID(selectedStudentID5, position5);
                int candidateID6 = GetCandidateID(selectedStudentID6, position6);
                int candidateID7 = GetCandidateID(selectedStudentID7, position7);
                int candidateID8 = GetCandidateID(selectedStudentID8, position8);


                using (MySqlConnection conn = new MySqlConnection(cs))
                {
                    conn.Open();

                    // Insert votes for different positions
                    InsertVote(conn, studentID, candidateID1, electionID);
                    InsertVote(conn, studentID, candidateID2, electionID);
                    InsertVote(conn, studentID, candidateID3, electionID);
                    InsertVote(conn, studentID, candidateID4, electionID);
                    InsertVote(conn, studentID, candidateID5, electionID);
                    InsertVote(conn, studentID, candidateID6, electionID);
                    InsertVote(conn, studentID, candidateID7, electionID);
                    InsertVote(conn, studentID, candidateID8, electionID);
                    // Repeat this for all the positions

                    conn.Close();

                    MessageBox.Show("Votes recorded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Function to get student ID from the ComboBox
        // Function to get student ID from the ComboBox
        private string GetStudentIDFromComboBox(ComboBox comboBox)
        {
            if (comboBox.SelectedItem != null)
            {
                string selectedStudentName = comboBox.SelectedItem.ToString();

                try
                {
                    conn.Open();

                    string query = $"SELECT studentID FROM student WHERE studentName = '{selectedStudentName}'";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string studentID = result.ToString();
                        conn.Close();
                        return studentID;
                    }

                    conn.Close();
                }
                catch (MySqlException ex)
                {
                    conn.Close();
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return string.Empty;
        }



        // Function to get candidate ID based on student ID and position
        private int GetCandidateID(string studentID, string position)
        {
            int candidateID = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(cs))
                {
                    conn.Open();

                    string query = $"SELECT candidateID FROM candidate WHERE studentID = '{studentID}' AND position = '{position}'";

                    MySqlCommand command = new MySqlCommand(query, conn);
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        candidateID = Convert.ToInt32(result);
                    }

                    conn.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return candidateID;
        }


        // Function to insert a single vote into the database
        private void InsertVote(MySqlConnection conn, string studentID, int candidateID, int electionID)
        {
            // SQL command to insert data into the votes table
            string insertQuery = $"INSERT INTO votes (studentID, candidateID, electionID, voteTimeStamp) " +
                     $"VALUES ('{studentID}', '{candidateID}', {electionID}, NOW())";


            MySqlCommand command = new MySqlCommand(insertQuery, conn);
            command.ExecuteNonQuery();
        }


    }
}
