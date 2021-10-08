using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace ESOFTA1337Project
{
    public partial class StudentRegistration : Form
    {
        public StudentRegistration()
        {
            InitializeComponent();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void terminateSes_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login loggedOut = new Login();
            loggedOut.Show();
        }

        private void regBtn_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            MySqlCommand command = new MySqlCommand("INSERT INTO `registration` (`regNo`, `firstName`, `lastName`, `dateOfBirth`, `gender`, `address`, `email`, `mobilePhone`, `homePhone`, `parentName`, `nic`, `contactNo`) VALUES (NULL, @FnameTxt, @LNameTxt, @BdayTxt, @GMaleTxt, @AddrsTxt, @mailTxt, @mobTxt, @lanTxt, @ownerTxt, @nicTxt, @callTxt);", db.getConnection());

            command.Parameters.Add("@FnameTxt", MySqlDbType.VarChar).Value = FnameTxt.Text;
            command.Parameters.Add("@LNameTxt", MySqlDbType.VarChar).Value = LNameTxt.Text;
            command.Parameters.Add("@BdayTxt", MySqlDbType.VarChar).Value = BdayTxt.Text;
            command.Parameters.Add("@GMaleTxt", MySqlDbType.VarChar).Value = GMaleTxt.Text;
            command.Parameters.Add("@AddrsTxt", MySqlDbType.VarChar).Value = AddrsTxt.Text;
            command.Parameters.Add("@mailTxt", MySqlDbType.VarChar).Value = mailTxt.Text;
            command.Parameters.Add("@mobTxt", MySqlDbType.VarChar).Value = mobTxt.Text;
            command.Parameters.Add("@lanTxt", MySqlDbType.VarChar).Value = lanTxt.Text;
            command.Parameters.Add("@ownerTxt", MySqlDbType.VarChar).Value = ownerTxt.Text;
            command.Parameters.Add("@nicTxt", MySqlDbType.VarChar).Value = nicTxt.Text;
            command.Parameters.Add("@callTxt", MySqlDbType.VarChar).Value = callTxt.Text;

            db.openConnection();
            if (!push_Validation()) //If Validaiton Return True
            {
                // execute the query
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Registration Successfully!", "Successfully Executed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear_All_Data();
                    LoadStudentsId();
                }
                else
                {
                    MessageBox.Show("Failed To Registration!");
                }
            }
            else
            {
                MessageBox.Show("Please Enter All Required Fields!", "~Empty Fields Detected~", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            db.closeConnection();
        }

        //Start Form Validation
        public Boolean push_Validation()
        {
            String iname = FnameTxt.Text;
            String iprice = LNameTxt.Text;
            String iqua = AddrsTxt.Text;

            if (iname.Equals("") || iprice.Equals("") ||
                iqua.Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //END Validation
        public void clear_All_Data() // Using This Func To Clear All Data After Save
        {
            Action<Control.ControlCollection> func = null;
            func = (controls) =>
            {
                foreach (Control control in controls)
                {
                    if (control is TextBox)
                    {
                        (control as TextBox).Clear();
                    }
                    else
                    {
                        func(control.Controls);
                    }
                }
            };
            func(Controls);
        }//END Clear Func

        private void clBtn_Click(object sender, EventArgs e)
        {
            clear_All_Data();
        }

        private void StudentRegistration_Load(object sender, EventArgs e)
        {
            LoadStudentsId(); //Trigger
        }

        //Load Students IDs Function
        public void LoadStudentsId()
        {
            userRegNo.Items.Clear(); //Clear Combo(Prevent reloading exist data)
            Database db = new Database();
                db.connection.Open();
                var query = "SELECT `regNo` FROM `registration`";
                using (var command = new MySqlCommand(query, db.connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                          userRegNo.Items.Add(reader.GetString("regNo"));
                        }
                    }
                }
                db.connection.Close();
        }

        //Load Student details function
        private void LoadStudentDetailsById(int id)
        {
            Database db = new Database();
            db.connection.Open();
                var query = "SELECT * FROM `registration` WHERE `regNo` = @regInput";
                using (var command = new MySqlCommand(query, db.connection))
                {
                    command.Parameters.AddWithValue("@regInput", id);
                    using (var reader = command.ExecuteReader())
                    {
                    //If Student Exist
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            FnameTxt.Text = reader.GetString("firstName");
                            LNameTxt.Text = reader.GetString("lastName");
                            BdayTxt.Text = reader.GetString("dateOfBirth").ToString();
                            //GMaleTxt.Text = reader.GetString("gender");
                            AddrsTxt.Text = reader.GetString("address");
                            mailTxt.Text = reader.GetString("email");
                            mobTxt.Text = reader.GetString("mobilePhone");
                            lanTxt.Text = reader.GetString("homePhone");
                            ownerTxt.Text = reader.GetString("parentName");
                            nicTxt.Text = reader.GetString("nic");
                            callTxt.Text = reader.GetString("contactNo");
                        }
                    }
                    else
                    {
                        return; //In No Students
                    }
                }
                }
            db.connection.Close();
        }

        private void userRegNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var studentID = Convert.ToInt32(userRegNo.Text);
            LoadStudentDetailsById(studentID);
        }

        private void upBtn_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            MySqlCommand command = new MySqlCommand("UPDATE `registration` SET `firstName` = @FnameTxt, `lastName` = @LNameTxt, `dateOfBirth` = @BdayTxt, `gender` = @GMaleTxt, `address` = @AddrsTxt, `email` = @mailTxt, `mobilePhone` = @mobTxt, `homePhone` = @lanTxt, `parentName` = @ownerTxt, `nic` = @nicTxt, `contactNo` = @callTxt WHERE `registration`.`regNo` = @callID;", db.getConnection());
            
            command.Parameters.Add("@FnameTxt", MySqlDbType.VarChar).Value = FnameTxt.Text;
            command.Parameters.Add("@LNameTxt", MySqlDbType.VarChar).Value = LNameTxt.Text;
            command.Parameters.Add("@BdayTxt", MySqlDbType.VarChar).Value = BdayTxt.Text;
            command.Parameters.Add("@GMaleTxt", MySqlDbType.VarChar).Value = GMaleTxt.Text;
            command.Parameters.Add("@AddrsTxt", MySqlDbType.VarChar).Value = AddrsTxt.Text;
            command.Parameters.Add("@mailTxt", MySqlDbType.VarChar).Value = mailTxt.Text;
            command.Parameters.Add("@mobTxt", MySqlDbType.VarChar).Value = mobTxt.Text;
            command.Parameters.Add("@lanTxt", MySqlDbType.VarChar).Value = lanTxt.Text;
            command.Parameters.Add("@ownerTxt", MySqlDbType.VarChar).Value = ownerTxt.Text;
            command.Parameters.Add("@nicTxt", MySqlDbType.VarChar).Value = nicTxt.Text;
            command.Parameters.Add("@callTxt", MySqlDbType.VarChar).Value = callTxt.Text;
            command.Parameters.Add("@callID", MySqlDbType.VarChar).Value = userRegNo.Text;

            db.openConnection();
            if (!push_Validation()) //If Validaiton Return True
            {
                // execute the query
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Updated Successfully!", "Successfully Executed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear_All_Data();
                    LoadStudentsId();
                }
                else
                {
                    MessageBox.Show("Failed To Update Student!");
                }
            }
            else
            {
                MessageBox.Show("Please Enter All Required Fields!", "~Empty Fields Detected~", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            db.closeConnection();
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            db.connection.Open();
            var query = "DELETE FROM `registration` WHERE `registration`.`regNo` = @callID";
            using (var command = new MySqlCommand(query, db.connection))
            {
                command.Parameters.Add("@callID", MySqlDbType.VarChar).Value = userRegNo.Text;
                MessageBox.Show("Are You Sure You Wants To Remove This?!", "~Removing~", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                command.ExecuteNonQuery();
                clear_All_Data();
            }
            db.connection.Close();
            LoadStudentsId();
        }
    }
}
