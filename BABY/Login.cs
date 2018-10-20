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


namespace BABY
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            panalForgot.Hide();
            panalSingUp.Hide();
            panelSelect.Hide();
            panalReset.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUserNameLogin.Text == "")
            {
                MessageBox.Show("Please Enter User Name !", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else if (txtPasswordLogin.Text == "")
            {
                MessageBox.Show("Please Enter Password !", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
            {
                try
                {
                    //MySqlConnection conn = new MySqlConnection("server=127.0.0.1;database=Baby;uid=root;sslMode = none");
                    MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM login WHERE UserName ='" + txtUserNameLogin.Text + "' AND Password ='" + txtPasswordLogin.Text + "'", conn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                       // MessageBox.Show("Login Sucessfull", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        conn.Close();
                        this.Visible = false;
                        DashBoard d = new DashBoard();
                        d.Show();
                        txtUserNameLogin.Clear();
                        txtPasswordLogin.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Login please check username and password", "Invalid Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        conn.Close();
                        txtUserNameLogin.Clear();
                        txtPasswordLogin.Clear();
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
            
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSubmitForgot_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserNameForgot.Text == "")
                {
                    MessageBox.Show("Please Fill User Name Text Field !", "Important Message !", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (txtSecurityQuestionForgot.SelectedItem == null)
                {
                    MessageBox.Show("Select Security Question", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (txtAnswerForgot.Text == "")
                {
                    MessageBox.Show("Please Answer Security Question", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                    conn.Open();
                    string qry = "SELECT * FROM login WHERE UserName='" + txtUserNameForgot.Text + "' AND SecurityQuestion='" + txtSecurityQuestionForgot.Text + "' AND Answer ='" + txtAnswerForgot.Text + "'";
                    MySqlCommand cmd = new MySqlCommand(qry, conn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                       // MessageBox.Show("Now You Can Reset Password !", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        conn.Close();
                        panelSelect.Show();
                        panalSingUp.Hide();
                        panalLogin.Hide();
                        panalForgot.Hide();
                        panalReset.Hide();
                        txtUserNameForgot.Clear();
                        txtSecurityQuestionForgot.ResetText();
                        txtAnswerForgot.Clear();
                        conn.Close();
                    }
                    else
                    {
                        MessageBox.Show("You Enterd Invalid Details.Please Try Again", "Invalid Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUserNameForgot.Clear();
                        txtSecurityQuestionForgot.ResetText();
                        txtAnswerForgot.Clear();
                        conn.Close();

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnForgitPasswordLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtUserNameLogin.Clear();
            txtPasswordLogin.Clear();
            panalForgot.Show();
            panalLogin.Hide();
            panalSingUp.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txtUserNameForgot.Clear();
            txtSecurityQuestionForgot.ResetText();
            txtAnswerForgot.Clear();
            panalLogin.Show();
            panalForgot.Hide();
            panalSingUp.Hide();
            panelSelect.Hide();
            panalReset.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFirstNameSignUp.Text == "")
                {
                    MessageBox.Show("Please Fill Your First Name !", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (txtLastNameSignUp.Text == "")
                {
                    MessageBox.Show("Please Fill Your Last Name !", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (txtUserNameSignUp.Text == "")
                {
                    MessageBox.Show("Please Fill User Name Text Field !", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (txtPasswordSignUp.Text == "")
                {
                    MessageBox.Show("Please Enter Password !", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (txtReEnterPasswordSignUp.Text == "")
                {
                    MessageBox.Show("Please Re Enter Password !", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (txtPasswordSignUp.Text != txtReEnterPasswordSignUp.Text)
                {
                    MessageBox.Show("Your Password Or Re Enter Password Fields Are Not Same !", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (txtSecurityQuestionSignUp.SelectedItem == null)
                {
                    MessageBox.Show("Select Security Question", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (txtAnswerSignUp.Text == "")
                {
                    MessageBox.Show("Please Answer Security Question", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {

                    MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO `login`(`FirstName`, `LastName`, `UserName`, `Password`, `ReEnterPassword`, `SecurityQuestion`, `Answer`) VALUES('" + txtFirstNameSignUp.Text + "','" + txtLastNameSignUp.Text + "','" + txtUserNameSignUp.Text + "','" + txtPasswordSignUp.Text + "','" + txtReEnterPasswordSignUp.Text + "','" + txtSecurityQuestionSignUp.Text + "','" + txtAnswerSignUp.Text + "')", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("New User Is Added !", "Congratulations !", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    txtFirstNameSignUp.Clear();
                    txtLastNameSignUp.Clear();
                    txtUserNameSignUp.Clear();
                    txtPasswordSignUp.Clear();
                    txtReEnterPasswordSignUp.Clear();
                    txtSecurityQuestionSignUp.ResetText();
                    txtAnswerSignUp.Clear();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnResetPasswordSignUp_Click(object sender, EventArgs e)
        {
            try
            {
              
                if (txtUserNameSignUp.Text == "")
                {
                    MessageBox.Show("Please Fill User Name Text Field !", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (txtPasswordSignUp.Text == "")
                {
                    MessageBox.Show("Please Enter Password !", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (txtReEnterPasswordSignUp.Text == "")
                {
                    MessageBox.Show("Please Re Enter Password !", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (txtPasswordSignUp.Text != txtReEnterPasswordSignUp.Text)
                {
                    MessageBox.Show("Your Password Or Re Enter Password Fields Are Not Same !", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }     
                else
                {


                    MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE `login` SET `Password`='"+txtPasswordSignUp.Text+"',`ReEnterPassword`='"+txtReEnterPasswordSignUp.Text+ "' WHERE `UserName`='"+txtUserNameSignUp.Text+"'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Your Data IS Saved !");
                    txtUserNameSignUp.Clear();
                    txtPasswordSignUp.Clear();
                    txtReEnterPasswordSignUp.Clear();
                   
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void btnBackToForgotPasswordSignUp_Click(object sender, EventArgs e)
        {
            panelSelect.Show();
            panalLogin.Hide();
            panalSingUp.Hide();
            panalForgot.Hide();
            panalReset.Hide();
        }

        private void btnBackToLoginSignUp_Click(object sender, EventArgs e)
        {  
            panalLogin.Show();
            panalSingUp.Hide();
            panalForgot.Hide();
            panalReset.Hide();
            panelSelect.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            panalSingUp.Show();
            panalLogin.Hide();
            panalForgot.Hide();
            panelSelect.Hide();
            panalReset.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            panalLogin.Show();
            panalForgot.Hide();
            panalSingUp.Hide();
            panelSelect.Hide();
            panalReset.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
          /*  try
            {
                using (MySqlConnection con = new MySqlConnection(Properties.Resources.dbcon))
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM `login` WHERE `UserName` like @username");
                    cmd.Parameters.AddWithValue("@username", txtUserNameReset.Text);
                    cmd.Connection = con;
                    con.Open();

                    DataSet ds = new DataSet();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();

                    bool loginSuccessful = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));

                    if (loginSuccessful)
                    {
                        Console.WriteLine("Success!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid username");
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }}*/
            


           try
            {

                if (txtUserNameReset.Text == "")
                {
                    MessageBox.Show("Please Fill User Name Text Field !", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (txtPasswordReset.Text == "")
                {
                    MessageBox.Show("Please Enter Password !", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (txtReEnterPasswordreset.Text == "")
                {
                    MessageBox.Show("Please Re Enter Password !", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (txtPasswordReset.Text != txtReEnterPasswordreset.Text)
                {
                    MessageBox.Show("Your Password Or Re Enter Password Fields Are Not Same !", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
               // else if ("SELECT * FROM `login` WHERE `UserName`='" + txtUserNameReset.Text + "'") 

                else
                {
                    MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE `login` SET `Password`='" + txtPasswordSignUp.Text + "',`ReEnterPassword`='" + txtReEnterPasswordSignUp.Text + "' WHERE `UserName`='" + txtUserNameSignUp.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Your Data IS Saved !");
                    txtUserNameSignUp.Clear();
                    txtPasswordSignUp.Clear();
                    txtReEnterPasswordSignUp.Clear();

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            panalReset.Show();
            panalLogin.Hide();
            panalForgot.Hide();
            panalSingUp.Hide();
            panelSelect.Hide();
            
        }

        private void label25_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            panalReset.Hide();
            panalLogin.Hide();
            panalForgot.Hide();
            panalSingUp.Hide();
            panelSelect.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panalReset.Hide();
            panalLogin.Show();
            panalForgot.Hide();
            panalSingUp.Hide();
            panelSelect.Hide();
        }

        private void txtPasswordLogin_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPasswordLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtUserNameLogin.Text == "")
                {
                    MessageBox.Show("Please Enter User Name !", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (txtPasswordLogin.Text == "")
                {
                    MessageBox.Show("Please Enter Password !", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    try
                    {
                        //MySqlConnection conn = new MySqlConnection("server=127.0.0.1;database=Baby;uid=root;sslMode = none");
                        MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("SELECT * FROM login WHERE UserName ='" + txtUserNameLogin.Text + "' AND Password ='" + txtPasswordLogin.Text + "'", conn);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            // MessageBox.Show("Login Sucessfull", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            conn.Close();
                            this.Visible = false;
                            DashBoard d = new DashBoard();
                            d.Show();
                            txtUserNameLogin.Clear();
                            txtPasswordLogin.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Login please check username and password", "Invalid Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            conn.Close();
                            txtUserNameLogin.Clear();
                            txtPasswordLogin.Clear();
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
        }
    }
}

