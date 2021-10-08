using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ESOFTA1337Project
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `logins` WHERE username = @username AND password = @password", db.getConnection());
            command.Parameters.Add("@username", MySqlDbType.VarChar).Value = userInput.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = passInput.Text;

            adapter.SelectCommand = command;

            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                this.Hide();
                StudentRegistration logged = new StudentRegistration();
                logged.Show();
            }
            else
            {
                if (passInput.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Required fields are empty!", "Warn!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Invalid login credentials,Try again!", "Invalid Login Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            db.closeConnection();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            var debug = true; //-> Enable/Disable Debug

            Database db = new Database();
            try
            {
                db.connection.Open();
                if (db.connection.State == System.Data.ConnectionState.Open)
                {
                    MessageBox.Show("Connection Established!");
                }
                else
                {
                    System.Environment.Exit(0);
                }
            }
            catch
            {
                MessageBox.Show("Please Check Your Database Connection Settings!");
                System.Environment.Exit(0);
            }

            if (debug == true)
            {
                userInput.Text = "admin";
                passInput.Text = "Skills@123";
            }

        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            StudentRegistration call = new StudentRegistration(); //need to fix
            call.clear_All_Data();
        }
    }
}
