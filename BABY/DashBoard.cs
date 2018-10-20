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
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
            loadTable();
            loadTable2();
            loadTable3();
            fillCombo();
            fillCombo2();
            fillCombo3();
            loadTable5();
            dataGridViewX4.Hide();



    }
        /*---------------Exit Button-----------------------------------------------*/
        private void picturebox1_click(object sender, EventArgs e)
        {
            if (totaltxt.Text != "Rs: 00.00" && totaltxt.Text != "Rs: 0.00")
            {
                MessageBox.Show("Please Save Data", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show("Please Save Data");
            }
            else
            {
                Application.Exit();
            }
            
        }

        /*---------------Minimized Button-----------------------------------------------*/
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /*---------------Maximized Button-----------------------------------------------*/
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (WindowState.ToString() == "Normal")
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        /*-------------------------btnSwapMenu-------------------------------------*/
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (panel1.Width == 250)
            {
                panel1.Width = 66;

                btnAddItem.Hide();
                btnAddItem2.Show();

                btnCategory.Hide();
                btnCategory2.Show();

                btnDashboard.Hide();
                btnDashboard2.Show();

                btnaddvehicle.Hide();
                btnaddvehicle2.Show();

                btnSearchVehicle.Hide();
                btnSearchVehicle2.Show();

                pictureBox5.Hide();
                pictureBox6.Show();

                btnDeveloper.Hide();
                btnDeveloper2.Show();
            }
            else
            {
                panel1.Width = 250;

                btnAddItem2.Hide();
                btnAddItem.Show();

                btnCategory2.Hide();
                btnCategory.Show();

                btnDashboard2.Hide();
                btnDashboard.Show();

                btnaddvehicle2.Hide();
                btnaddvehicle.Show();

                btnSearchVehicle2.Hide();
                btnSearchVehicle.Show();

                pictureBox5.Show();
                pictureBox6.Hide();

                btnDeveloper.Show();
                btnDeveloper2.Hide();
            }

        }

        /*-------------------------------LOAD EVENT--------------------------------------------------*/
        private void DashBoard_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label1.Text = DateTime.Now.ToLongDateString();
            label2.Text = DateTime.Now.ToLongTimeString();

            btnAddItem2.Hide();
            btnCategory2.Hide();
            btnDashboard2.Hide();
            btnaddvehicle2.Hide();
            btnSearchVehicle2.Hide();
            btnDeveloper2.Hide();

            pictureBox6.Hide();

            Autocomplete();


        }

        /*-----------------Time Run-----------------------------------------------*/
        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }


        /*---------------------------------------------------------------------------------------------------------------*/
        /*----------------------------------Panel Add Item------------------------------------------------*/
        /*-------------------------------------------------------------------------------------------------------------------*/
        /*-----------------------------btnAddItemAddItem----------------------------------------*/

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtItemCodeAddItem.Text == "")
                {
                    MessageBox.Show("Please Select Item Code ?", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }

                else if (txtItemNameAddItem.Text == "")
                {
                    MessageBox.Show("Please Fill Item Name", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (txtQtyAddItem.Text == "")
                {
                    MessageBox.Show("Please Fill Qty", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (TxtItemCatagoryAddItem.Text == "")
                {
                    MessageBox.Show("Please Select Catagory", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }

                else
                {

                    MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO `items`(`Item_Code`, `Item_Name`,`Qty`, `Price`, `Category`) VALUES('" + txtItemCodeAddItem.Text + "','" + txtItemNameAddItem.Text + "','" + txtQtyAddItem.Text + "','"+txtPriceAddItem.Text+"','" + TxtItemCatagoryAddItem.Text + "')", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Your Item Is Added to Database !", "INSERT", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    txtItemCodeAddItem.Clear();
                    txtItemNameAddItem.Clear();
                    txtQtyAddItem.Clear();
                    TxtItemCatagoryAddItem.ResetText();
                    txtPriceAddItem.Clear();
                    loadTable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    /*----------------------Real Time Data Grid View  Updateing in Add Item-----------------------------------------------------*/
        void loadTable()
        {
            MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
            MySqlCommand cmd = new MySqlCommand("SELECT * From items", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable dtnew = new DataTable();
                sda.Fill(dtnew);
                BindingSource bs = new BindingSource();
                bs.DataSource = dtnew;
                dataGridViewX1.DataSource = bs;
                sda.Update(dtnew);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /*------------------------Drop down Catogary Lisi in Add item Panel----------------------------------------*/
        void fillCombo()
        {
            try
            {
                
                MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `category`", conn);
                conn.Open();
                MySqlDataReader msr = cmd.ExecuteReader();
                while (msr.Read())
                {
                    string sname = msr.GetString("category");
                    TxtItemCatagoryAddItem.Items.Add(sname);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        /*--------------------------txtQtyAddItem Only Integer input in Add item Panel----------------------------------*/
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 46 && ch != 8)
            {
                e.Handled = true;
            }
        }

        /*--------------------------txtPriceAddItem Only Integer input in Add item Panel----------------------------------*/
        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 46 && ch != 8)
            {
                e.Handled = true;
            }
        }

        /*--------------------------Data Grid Viev Select Row to Fill Text Box Add Item-----------------------------*/
        private void dataGridViewX1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridViewX1.Rows[e.RowIndex];
                txtItemCodeAddItem.Text = row.Cells[1].Value.ToString();
                txtItemNameAddItem.Text = row.Cells[2].Value.ToString();
                txtQtyAddItem.Text = row.Cells[3].Value.ToString();
                txtPriceAddItem.Text = row.Cells[4].Value.ToString();
                TxtItemCatagoryAddItem.Text = row.Cells[5].Value.ToString();
            }
        }
       

        /*-----------------------------btnUpdateAddItem----------------------------------------*/
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtItemCodeAddItem.Text == "")
                {
                    MessageBox.Show("Please Select Rows ,What Do you want Update ?", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE `items` SET  `Item_Name`='" + txtItemNameAddItem.Text + "',`Qty`='" + txtQtyAddItem.Text + "',`Price`='" + txtPriceAddItem.Text + "',`Category`='" + TxtItemCatagoryAddItem.Text + "' WHERE `Item_Code`='" + txtItemCodeAddItem.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Your Data Is Updated", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    conn.Close();
                    txtItemCodeAddItem.Clear();
                    txtItemNameAddItem.Clear();
                    txtQtyAddItem.Clear();
                    TxtItemCatagoryAddItem.ResetText();
                    txtPriceAddItem.Clear();
                    loadTable();
                }
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        /*-----------------------------btnDeleteAddItem----------------------------------------*/
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtItemCodeAddItem.Text == "")
                {
                    MessageBox.Show("Please Select Rows ,What Do you want Delete ?", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult result= MessageBox.Show("Are You Sure Delete Selected Data ?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(result == DialogResult.Yes)
                    {
                        MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM `items` WHERE `Item_Code`='" + txtItemCodeAddItem.Text + "'", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        txtItemCodeAddItem.Clear();
                        txtItemNameAddItem.Clear();
                        txtQtyAddItem.Clear();
                        TxtItemCatagoryAddItem.ResetText();
                        txtPriceAddItem.Clear();
                        loadTable();
                    }
                    
                }
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        /*---------------------------Search Button Text Change Event in ADD Item panel------------------------------*/
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Item Name")
            {
                MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                MySqlDataAdapter sda = new MySqlDataAdapter("SELECT * FROM `items` WHERE `Item_Name` LIKE '" + txtSearch.Text + "%'", conn);
                //conn.Open();
                //MySqlCommand cmd = new MySqlCommand("SELECT * FROM `items` WHERE `Item_Name` LIKE '"+txtSearch.Text+"%'", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridViewX1.DataSource = dt;
            }
            else if (comboBox1.Text == "Catagory")
            {
                MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                MySqlDataAdapter sda = new MySqlDataAdapter("SELECT * FROM `items` WHERE `Category` LIKE '" + txtSearch.Text + "%'", conn);
                //conn.Open();
                //MySqlCommand cmd = new MySqlCommand("SELECT * FROM `items` WHERE `Item_Name` LIKE '"+txtSearch.Text+"%'", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridViewX1.DataSource = dt;
            }
            else
            {
                MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                MySqlDataAdapter sda = new MySqlDataAdapter("SELECT * FROM `items` WHERE `Item_Name` LIKE '" + txtSearch.Text + "%'", conn);
                //conn.Open();
                //MySqlCommand cmd = new MySqlCommand("SELECT * FROM `items` WHERE `Item_Name` LIKE '"+txtSearch.Text+"%'", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridViewX1.DataSource = dt;
            }


        }



        /*------------------------------------------------------------------------------------------------------*/
        /*----------------------------------Side Buttons-------------------------------------------*/
        /*------------------------------------------------------------------------------------------------------*/
        /*-----------------------------btnAddItem Click Event In Menu Items----------------------------------------*/

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (totaltxt.Text != "Rs: 00.00" && totaltxt.Text != "Rs: 0.00")
            {
                MessageBox.Show("Please Save Data", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
               // MessageBox.Show("Please Save Data");
            }
            else if (lblVehicleNumber.Text != "")
            {
                MessageBox.Show("Clear Vehicle Number", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show("Clear Vehicle Number");
            }
            else
            {
                panalAddItem.Show();
                panalItemCategory.Hide();
                panalAddVehicle.Hide();
                panalSearch.Hide();
                panalDeveloper.Hide();
                panelDashboard.Hide();
                TxtItemCatagoryAddItem.Items.Clear();
                fillCombo();
                loadTable();
                cleardata();
            }
        }

        /*-----------------------------btnAddItem2 Click Event In Menu Items----------------------------------------*/
        private void btnAddItem2_Click(object sender, EventArgs e)
        {
            if (totaltxt.Text != "Rs: 00.00" && totaltxt.Text != "Rs: 0.00")
            {
                MessageBox.Show("Please Save Data", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show("Please Save Data");
            }
            else if (lblVehicleNumber.Text != "")
            {
                MessageBox.Show("Clear Vehicle Number", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
               // MessageBox.Show("Clear Vehicle Number");
            }
            else
            {
                panalAddItem.Show();
                panalItemCategory.Hide();
                panalAddVehicle.Hide();
                panalSearch.Hide();
                panalDeveloper.Hide();
                panelDashboard.Hide();
                TxtItemCatagoryAddItem.Items.Clear();
                fillCombo();
                cleardata();
            }
        }

        /*-----------------------------btnCategory Click Event In Menu Items----------------------------------------*/
        private void btnCategory_Click(object sender, EventArgs e)
        {
            if (totaltxt.Text != "Rs: 00.00" && totaltxt.Text != "Rs: 0.00")
            {
                MessageBox.Show("Please Save Data", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show("Please Save Data");
            }
            else if (lblVehicleNumber.Text != "")
            {
                MessageBox.Show("Clear Vehicle Number", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show("Clear Vehicle Number");
            }
            else
            {
                panalItemCategory.Show();
                panalAddItem.Hide();
                panalAddVehicle.Hide();
                panalDeveloper.Hide();
                panalSearch.Hide();
                panelDashboard.Hide();
                cleardata();
            }
        }

        /*-----------------------------btnCategory2 Click Event In Menu Items----------------------------------------*/
        private void btnCategory2_Click(object sender, EventArgs e)
        {
            if (totaltxt.Text != "Rs: 00.00" && totaltxt.Text != "Rs: 0.00")
            {
                MessageBox.Show("Please Save Data", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show("Please Save Data");
            }
            else if (lblVehicleNumber.Text != "")
            {
                MessageBox.Show("Clear Vehicle Number", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show("Clear Vehicle Number");
            }
            else
            {
                panalItemCategory.Show();
                panalAddItem.Hide();
                panalAddVehicle.Hide();
                panalDeveloper.Hide();
                panalSearch.Hide();
                panelDashboard.Hide();
                cleardata();
            }
            
        }

        /*-----------------------------btnSearchVehicle Click Event In Menu Items----------------------------------------*/
        private void btnSearchVehicle_Click(object sender, EventArgs e)
        {
            if (totaltxt.Text != "Rs: 00.00" && totaltxt.Text != "Rs: 0.00")
            {
                MessageBox.Show("Please Save Data", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show("Please Save Data");
            }
            else if (lblVehicleNumber.Text != "")
            {
                MessageBox.Show("Clear Vehicle Number", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show("Clear Vehicle Number");
            }
            else
            {
                panalSearch.Show();
                panalAddItem.Hide();
                panalAddVehicle.Hide();
                panalDeveloper.Hide();
                panalItemCategory.Hide();
                loadTable3();
                panelDashboard.Hide();
                cleardata();
            }
        }


        /*-----------------------------btnSearchVehicle 2 Click Event In Menu Items----------------------------------------*/
        private void btnSearchVehicle2_Click(object sender, EventArgs e)
        {
            if (totaltxt.Text != "Rs: 00.00" && totaltxt.Text != "Rs: 0.00")
            {
                MessageBox.Show("Please Save Data", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show("Please Save Data");
            }
            else if (lblVehicleNumber.Text != "")
            {
                MessageBox.Show("Clear Vehicle Number", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
               // MessageBox.Show("Clear Vehicle Number");
            }
            else
            {
                panalSearch.Show();
                panalAddItem.Hide();
                panalAddVehicle.Hide();
                panalDeveloper.Hide();
                panalItemCategory.Hide();
                loadTable3();
                panelDashboard.Hide();
                cleardata();
            }
        }
        /*-----------------------------btnAddVehicle Click Event In Menu Items----------------------------------------*/
        private void btnaddvehicle_Click(object sender, EventArgs e)
        {
            if (totaltxt.Text != "Rs: 00.00" && totaltxt.Text != "Rs: 0.00")
            {
                MessageBox.Show("Please Save Data");
            }
            else if (lblVehicleNumber.Text != "")
            {
                MessageBox.Show("Clear Vehicle Number");
            }
            else
            {
                panalAddVehicle.Show();
                panalAddItem.Hide();
                panalItemCategory.Hide();
                panalSearch.Hide();
                panalDeveloper.Hide();
                panelDashboard.Hide();
                cleardata();
                fillCombo2();
            }
        }
        /*-----------------------------btnAddVehicle 2 Click Event In Menu Items----------------------------------------*/
        private void btnaddvehicle2_Click(object sender, EventArgs e)
        {
            if (totaltxt.Text != "Rs: 00.00" && totaltxt.Text != "Rs: 0.00")
            {
                MessageBox.Show("Please Save Data", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show("Please Save Data");
            }
            else if (lblVehicleNumber.Text != "")
            {
                MessageBox.Show("Clear Vehicle Number", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show("Clear Vehicle Number");
            }
            else
            {
                panalAddVehicle.Show();
                panalAddItem.Hide();
                panalItemCategory.Hide();
                panalSearch.Hide();
                panalDeveloper.Hide();
                panelDashboard.Hide();
                cleardata();
                fillCombo2();
            }
        }
        /*-----------------------------btnDashboard Click Event In Menu Items----------------------------------------*/
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            if (totaltxt.Text != "Rs: 00.00" && totaltxt.Text != "Rs: 0.00")
            {
                MessageBox.Show("Please Save Data", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show("Please Save Data");
            }
            else if (lblVehicleNumber.Text != "")
            {
                MessageBox.Show("Clear Vehicle Number", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show("Clear Vehicle Number");
            }
            else
            {
                panelDashboard.Show();
                panalAddVehicle.Hide();
                panalAddItem.Hide();
                panalDeveloper.Hide();
                panalItemCategory.Hide();
                panalSearch.Hide();
                cleardata();
                loadTable5();
                loadTable2();
                fillCombo3();
            }
        }

        /*-----------------------------btnDashboard 2 Click Event In Menu Items----------------------------------------*/
        private void btnDashboard2_Click(object sender, EventArgs e)
        {
            if (totaltxt.Text != "Rs: 00.00" && totaltxt.Text != "Rs: 0.00")
            {
                MessageBox.Show("Please Save Data", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
               // MessageBox.Show("Please Save Data");
            }
            else if (lblVehicleNumber.Text != "")
            {
                MessageBox.Show("Clear Vehicle Number", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
               // MessageBox.Show("Clear Vehicle Number");
            }
            else
            {
                panelDashboard.Show();
                panalAddVehicle.Hide();
                panalAddItem.Hide();
                panalDeveloper.Hide();
                panalItemCategory.Hide();
                panalSearch.Hide();
                cleardata();
                fillCombo3();
            }
        }


        

        /*---------------------------------------------------------------------------------------------------------------*/
        /*---------------------------------------------Panel Catogary---------------------------------------------------*/
        /*-------------------------------------------------------------------------------------------------------------------*/

        /*-------------------------------------------btnAddItemCategory------------------------------------------------------*/
        private void btnAddItemCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCategoryNameCategory.Text == "")
                {
                    MessageBox.Show("Please Fill Category ?", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {

                    MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO `category`(`Category`) VALUES ('"+txtCategoryNameCategory.Text+"')", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Your Item Is Added to Database !", "INSERT", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    txtCategoryNameCategory.Clear();
                    loadTable2();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /*----------------------Real Time Data Grid View  Updateing Category-----------------------------------------------------*/
        void loadTable2()
        {
            MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `category`", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable dtnew = new DataTable();
                sda.Fill(dtnew);
                BindingSource bs = new BindingSource();
                bs.DataSource = dtnew;
                dataGridViewX2.DataSource = bs;
                sda.Update(dtnew);
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        

















        /*--------------------------Data Grid Viev Select Row to Fill Text Box-----------------------------*/
        private void dataGridViewX2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridViewX2.Rows[e.RowIndex];
                txtIdCategory.Text = row.Cells[0].Value.ToString();
                txtCategoryNameCategory.Text = row.Cells[1].Value.ToString();

            }
        }

        /*-----------------------------btnUpdateCategory----------------------------------------*/
        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIdCategory.Text == "")
                {
                    MessageBox.Show("Please Select Rows ,What Do you want Update ?", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE `category` SET `Category`='"+txtCategoryNameCategory.Text+"' WHERE `ID`='"+txtIdCategory.Text+"'", conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Your Data Is Updated", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    conn.Close();
                    txtCategoryNameCategory.Clear();
                    txtIdCategory.Clear();
                    loadTable2();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /*-----------------------------btnDeleteCategory----------------------------------------*/
        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIdCategory.Text == "")
                {
                    MessageBox.Show("Please Select Rows ,What Do you want Delete ?", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are You Sure To Delete This Item ?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM `category` WHERE `ID`='" + txtIdCategory.Text + "'", conn);
                        cmd.ExecuteNonQuery();

                        conn.Close();
                        txtIdCategory.Clear();
                        txtCategoryNameCategory.Clear();
                        loadTable2();
                    }
                   
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

  /*---------------------------Search Button Text Change Event in Add Catogary panel------------------------------*/

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT * FROM `category` WHERE `Category` LIKE '" + textBox1.Text + "%'", conn);
            //conn.Open();
            //MySqlCommand cmd = new MySqlCommand("SELECT * FROM `items` WHERE `Item_Name` LIKE '"+txtSearch.Text+"%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridViewX2.DataSource = dt;
        }

        /*----------------------------------------------------------------------------------------------------------------*/
        /*--------------------------------------Panel Add Vehical ---------------------------------------*/
        /*----------------------------------------------------------------------------------------------------------------*/

        /*----------------------Vehical Number First Part Key Press Event Inly Input Number And Uppercase-------------------------------------------------------*/
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        /*----------------------Vehical Number First Part Key Press Event Inly Input Numbers------------------*/

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 46 && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void textBoxX2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 46 && ch != 8)
            {
                e.Handled = true;
            }
        }

        /*----------------------Add Vehicle Number Button in Add Vehical Panel ------------------*/
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (txtVehicleNumberFirst.Text == "") 
            {
                MessageBox.Show("Please ADD Vehicle Number", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else if (txtVehicleNumberSecond.Text == "")
            {
                MessageBox.Show("Please ADD Vehicle Number", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
            {
                lblVehicleNumber.Text = "" + txtVehicleNumberFirst.Text + "-" + txtVehicleNumberSecond.Text + "";
                txtVehicleNumberFirst.Clear();
                txtVehicleNumberSecond.Clear();
            }
            
        }

       
        /*----------------------Clear Vehicle Number in Add Vehical Panel ------------------*/
        private void button7_Click_1(object sender, EventArgs e)
        {
            lblVehicleNumber.Text = "";
        }


        /*----------------------Add Invoice Number Button in Add Vehical Panel ------------------*/
        private void buttonX7_Click(object sender, EventArgs e)
        {
            if(lblVehicleNumber.Text == "")
            {
                MessageBox.Show("Please ADD Vehicle Number", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
            {
                if (txtInvoiceNumber.TextLength == 1)
                {
                    lblInvoiceNumber.Text = "#0000" + txtInvoiceNumber.Text;
                }
                else if (txtInvoiceNumber.TextLength == 2)
                {
                    lblInvoiceNumber.Text = "#000" + txtInvoiceNumber.Text;
                }
                else if (txtInvoiceNumber.TextLength == 3)
                {
                    lblInvoiceNumber.Text = "#00" + txtInvoiceNumber.Text;
                }
                else if (txtInvoiceNumber.TextLength == 4)
                {
                    lblInvoiceNumber.Text = "#0" + txtInvoiceNumber.Text;
                }
                else 
                {
                    lblInvoiceNumber.Text = "#" + txtInvoiceNumber.Text;
                }


            }
            txtInvoiceNumber.Clear();

        }


        /*----------------------Clear Incoice Number in Add Vehical Panel ------------------*/
        private void buttonX8_Click(object sender, EventArgs e)
        {
            lblInvoiceNumber.ResetText();
        }

        /*-------------------------------Auto Complete Search Column in Add Vehical Panel ----------------------------------------*/
        private void Autocomplete()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `items`", conn);
                
                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "items");
                AutoCompleteStringCollection col = new
                AutoCompleteStringCollection();
                int i = 0;
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    col.Add(ds.Tables[0].Rows[i]["Item_Name"].ToString());

                }
                txtItemSearchAddVehicle.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtItemSearchAddVehicle.AutoCompleteCustomSource = col;
                txtItemSearchAddVehicle.AutoCompleteMode = AutoCompleteMode.Suggest;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /*--------------------------------------List View Size Change Fill--------------------------------*/
        //private bool Resizing = false;
        private void listView1_SizeChanged(object sender, EventArgs e)
        {
            listView1.Columns[0].Width = (int)(0.6 * listView1.ClientRectangle.Width);
            listView1.Columns[1].Width = (int)(0.1 * listView1.ClientRectangle.Width);
            listView1.Columns[2].Width = (int)(0.3 * listView1.ClientRectangle.Width);
            // if (!Resizing)
            // {
            //   Resizing = true;

            //  ListView listView = sender as ListView;
            //  if (listView1 != null)
            //  {
            //   float totalColumnWidth = 0;

            //  for (int i = 0; i < listView.Columns.Count; i++)
            //   totalColumnWidth += Convert.ToInt32(listView1.Columns[i].Tag);

            //   for (int i = 0; i < listView.Columns.Count; i++)
            //  {
            // //float colPercentage = (Convert.ToInt32(listView.Columns[i].Tag) / totalColumnWidth);
            //  //lblVehicleNumber.Text = colPercentage.ToString();
            //     listView.Columns[i].Width = (int)(0.8 * listView.ClientRectangle.Width);
            //  }
            // }
            // }

            // Resizing = false;
        }

        /*----------------Delete Selected Items In list View (Second Delete Button) in Add Vehical Panel-------------------------*/

        private void button3_Click_2(object sender, EventArgs e)
        {
            try
            {
                string itemName222 = listView1.SelectedItems[0].Text;
                if (itemName222 == "")
                {
                    MessageBox.Show("Select What Do You Want Delete", "DELETE", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are You Sure To Delete This Item ?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                        //MessageBox.Show(itemname);
                        listView1.Items.Remove(listView1.SelectedItems[0]);
                        itemdate = DateTime.Now.ToShortDateString();
                        MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM `vehicle` WHERE `Number`='" + lblVehicleNumber.Text + "'AND `Name`='" + itemname + "'AND `date`='" + itemdate + "' AND `Invoice` ='"+ lblInvoiceNumber .Text+ "'", conn);
                        cmd.ExecuteNonQuery();
                        total();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        /*-------------------Delete Selected Items In list View (First Delete Button) in Add Vehical Panel-------------------------*/
        private void button5_Click_1(object sender, EventArgs e)
        {

            try
            {
                string itemName222 = listView1.SelectedItems[0].Text;
                if (itemName222 == "")
                {
                    MessageBox.Show("Select What Do You Want Delete", "DELETE", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are You Sure To Delete This Item ?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                        //MessageBox.Show(itemname);
                        listView1.Items.Remove(listView1.SelectedItems[0]);
                        itemdate = DateTime.Now.ToShortDateString();
                        MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM `vehicle` WHERE `Number`='" + lblVehicleNumber.Text + "'AND `Name`='" + itemname + "'AND `date`='" + itemdate + "' AND `Invoice` ='" + lblInvoiceNumber.Text + "'", conn);
                        cmd.ExecuteNonQuery();
                        total();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        /*----------------Add Service Chargers Button  in Add Vehical Panel-------------------------*/

        public string itemName;
        public string itemPrice;
        public string itemdate;
        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (lblVehicleNumber.Text == "")
                {
                    MessageBox.Show("Fill Vehical Number", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (lblInvoiceNumber.Text == "")
                {
                    MessageBox.Show("Add Invoice Number", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtItemSearchAddVehicle.Text == "")
                {
                    MessageBox.Show("Select Service or Other Chargers", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtServiceChargePrice.Text == "")
                {
                    MessageBox.Show("Fill The Price", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    listView1.Items.Add(new ListViewItem(new[] { txtItemSearchAddVehicle.Text,"0", txtServiceChargePrice.Text }));
                    itemName = txtItemSearchAddVehicle.Text;
                    itemPrice = txtServiceChargePrice.Text;
                    itemdate = DateTime.Now.ToShortDateString();
                    txtServiceChargePrice.Clear();

                    txtItemSearchAddVehicle.Text = "";

                    MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO `Vehicle`(`Invoice`,`Number`, `Name`, `Qty`,`Price`,`date`) VALUES ('"+lblInvoiceNumber.Text+"','" + lblVehicleNumber.Text + "','" + itemName + "','"+0+"','" + itemPrice + "','" + itemdate + "')", conn);
                    cmd.ExecuteNonQuery();
                    total();

                }
                
            }catch(Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        /*----------------Add  Items  Button  in Add Vehical Panel-------------------------*/
        public string Price;
        public int totPrice;
        public string catagory;
        public string itemname;

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (lblVehicleNumber.Text == "")
                {
                    MessageBox.Show("Fill Vehical Number", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (lblInvoiceNumber.Text == "")
                {
                    MessageBox.Show("Add Invoice Number", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtitemAddVehicle.Text == "")
                {
                    MessageBox.Show("Select Item ", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (textBox2.Text=="")
                {
                    MessageBox.Show("Fill Qty", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string sql = "SELECT * FROM `items` WHERE `Item_Name`='" + txtitemAddVehicle.Text + "'";
                    MySqlConnection con = new MySqlConnection(Properties.Resources.dbcon);
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        catagory = reader.GetString("Category");
                        Price = reader.GetString("Price");
                        totPrice = Convert.ToInt32(textBox2.Text) * Convert.ToInt32(Price);
                        listView1.Items.Add(new ListViewItem(new[] { txtitemAddVehicle.Text + "(" + catagory + ")",textBox2.Text, totPrice.ToString() }));


                    }
                    itemdate = DateTime.Now.ToShortDateString();
                    MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                    conn.Open();
                    MySqlCommand cmd2 = new MySqlCommand("INSERT INTO `vehicle`(`Invoice`,`Number`, `Name`, `Catagory`,`Unit_Price`,`Qty`, `Price`, `date`) VALUES ('"+lblInvoiceNumber.Text+"','" + lblVehicleNumber.Text + "','" + txtitemAddVehicle.Text + "','" + catagory + "','"+Price+"','"+ textBox2 .Text+ "','" + totPrice + "','" + itemdate + "')", conn);
                    cmd2.ExecuteNonQuery();

                    MySqlCommand cmd3 = new MySqlCommand("UPDATE `items` SET Qty=Qty-'"+ Convert.ToInt32(textBox2 .Text)+ "' WHERE `Item_Name` = '"+ txtitemAddVehicle .Text+ "'", conn);
                    cmd3.ExecuteNonQuery();
                    itemname = txtitemAddVehicle.Text;
                    txtitemAddVehicle.Text = "";
                    textBox2.Clear();
                    total();

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        //--------------------------------------Auto Completete Search Items in Add Vehicla Panel--------------------------------------------//
        void fillCombo2()
        {
            try
            {

                MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `items`", conn);
                conn.Open();
                MySqlDataReader msr = cmd.ExecuteReader();
                while (msr.Read())
                {
                    string sname2 = msr.GetString("Item_Name");
                    string sname32 = msr.GetString("ID");
                    txtitemAddVehicle.Items.Add(sname2);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        /*--------------------Price Only Input Numbers--------------------------------*/
        private void txtServiceChargePrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 46 && ch != 8)
            {
                e.Handled = true;
            }
        }

        public int sums;
        public void total()
        {
            int sum = 0;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                sum += Convert.ToInt32(listView1.Items[i].SubItems[2].Text);
            }
            totaltxt.Text = "Rs: " + sum + ".00";
            sums = sum;

        }

        /*--------------Save Button In Add Vehicle Panel----------------------*/
        private void buttonX2_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblVehicleNumber.Text == "" )
                {
                    MessageBox.Show("(NO data to save)Please Add Vehicle Number", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else if (lblInvoiceNumber.Text == "")
                {
                    MessageBox.Show("Add Invoice Number", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (totaltxt.Text == "Rs: 00.00")
                {
                    MessageBox.Show("No Data to Save", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    itemdate = DateTime.Now.ToShortDateString();
                    MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO `total`(`Invoice`, `Number`,  `date` , `Total`) VALUES ('" + lblInvoiceNumber.Text + "','" + lblVehicleNumber.Text + "','" + itemdate + "','" + totaltxt.Text + "')", conn);
                    cmd.ExecuteNonQuery();

                    MySqlCommand cmd2 = new MySqlCommand("INSERT INTO `total2`(`Invoice`, `Number`,  `date` , `Total`) VALUES ('" + lblInvoiceNumber.Text + "','" + lblVehicleNumber.Text + "','" + itemdate + "','" + sums + "')", conn);
                    cmd2.ExecuteNonQuery();

                    lblVehicleNumber.Text = "";
                    totaltxt.Text = "Rs: 00.00";
                    listView1.Items.Clear();
                    lblInvoiceNumber.ResetText();
                    MessageBox.Show("Your Data Is ADDED", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        /*-----------------------Print Bill Button In Add Vehical Panel----------------*/
        private void buttonX3_Click(object sender, EventArgs e)
        {
           ///try
            //{
             //Form1 f1 = new Form1();
             //  CrystalReport1 c1 = new CrystalReport1();
            //   TextObject text = (TextObject)c1.ReportDefinition.Sections["Section3"].ReportObjects["Text1"];
            ////   f1.xxxxxxxxxxx.ReportSource = c1;
            //   f1.Show();
           // }catch(Exception ex)
           //   {
           //    MessageBox.Show(ex.Message);
            // }

        }


        /*----------------------------------------------------------------------------------------*/
        /*--------------------------Search veical Panel---------------------------------------*/
        /*----------------------------------------------------------------------------------------*/

        /*---------------------------Search text Box Key Pres Event in Upper Case And Numbers Only-----------------------------------------------*/
        private void textBoxX1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        /*----Not Use Now----*/
        private void textBoxX2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 46 && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            textBoxX1.Clear();
            cellClickNumber = null;
            cellClickDate = null;
            cellClickInvoice = null;
        }

        /*-----Not Use-------*/
        private void panel131_Paint(object sender, PaintEventArgs e)
        {

        }
/*----------------------Real Time Data Grid View  Updateing in Search Panel-----------------------------------------------------*/
        void loadTable3()
        {
            MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `vehicle`", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable dtnew = new DataTable();
                sda.Fill(dtnew);
                BindingSource bs = new BindingSource();
                bs.DataSource = dtnew;
                dataGridViewX3.DataSource = bs;
                sda.Update(dtnew);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /*----------------------Search In Search Item Panel In Text Bix Text Change Event------------------------*/
        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            dataGridViewX4.Hide();
            MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT * FROM `vehicle` WHERE `Number` LIKE '" + textBoxX1.Text + "%'", conn);
            //conn.Open();
            //MySqlCommand cmd = new MySqlCommand("SELECT * FROM `items` WHERE `Item_Name` LIKE '"+txtSearch.Text+"%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridViewX3.DataSource = dt;
            dateTimeInput1.ResetText();
        }

        /*---------------Search Button On Add Search Vehical Panel-----------------*/
        private void buttonX4_Click(object sender, EventArgs e)
        {
            dataGridViewX4.Show();
            loadTable4();
        }

 /*----------------------Real Time Data Grid View  Updateing in Search Panal-----------------------------------------------------*/
        void loadTable4()
        {
            MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `total` WHERE `Number` = '" + textBoxX1.Text + "' AND `date`='" + dateTimeInput1.Text + "'", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable dtnew = new DataTable();
                sda.Fill(dtnew);
                BindingSource bs = new BindingSource();
                bs.DataSource = dtnew;
                dataGridViewX4.DataSource = bs;
                sda.Update(dtnew);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /*-------------------------Data Grid View 3 Selected Row Fill Text Box----------------------*/
        private void dataGridViewX3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridViewX3.Rows[e.RowIndex];
                    textBoxX1.Text = row.Cells[2].Value.ToString();
                    dateTimeInput1.Value = Convert.ToDateTime(row.Cells[8].Value);

                }
            }
            catch(Exception ex)
            {
                
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }


/*------------Data Grid View 4 Cell Click Event For USes Delete Button In Search Vehical Panal----------------------*/
        public string cellClickNumber;
        public string cellClickDate;
        public string cellClickInvoice;
        private void dataGridViewX4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridViewX4.Rows[e.RowIndex];
                cellClickInvoice = row.Cells[1].Value.ToString();
                cellClickNumber = row.Cells[2].Value.ToString();
                cellClickDate = row.Cells[3].Value.ToString();  
            }
        }
     /*--------------------Delete Button On Search Vehical Panel-------------------*/
        private void button6_Click_2(object sender, EventArgs e)
        {
           // MessageBox.Show(cellClickNumber);
           try
            {
                if (cellClickDate==null)
                {
                    MessageBox.Show("Select what do You want Delete (Note :If You Want Delete Vehical Details Please Search By Date And After You Can Delete data))", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are You Sure Delete Selectod Date ?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(result == DialogResult.Yes)
                    {
                        MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM `vehicle` WHERE `Number`='" + cellClickNumber + "' AND `date`='" + cellClickDate + "' AND `Invoice`='" + cellClickInvoice + "'", conn);
                        MySqlCommand cmd2 = new MySqlCommand("DELETE FROM `total` WHERE `Number`='" + cellClickNumber + "' AND `date`='" + cellClickDate + "' AND `Invoice`='" + cellClickInvoice + "'", conn);
                        MySqlCommand cmd3 = new MySqlCommand("DELETE FROM `total2` WHERE `Number`='" + cellClickNumber + "' AND `date`='" + cellClickDate + "' AND `Invoice`='" + cellClickInvoice + "'", conn);
                        cmd.ExecuteNonQuery();
                        cmd2.ExecuteNonQuery();
                        cmd3.ExecuteNonQuery();
                        loadTable4();
                    }
                   
                }
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
            
        }


        /*-------------------------------------------------------------------------------------------*/
        /*-------------------------Dashboard Panel-----------------------------------------*/
        /*-------------------------------------------------------------------------------------------*/

        /*--------------------------------Get Total Earnings Of The Day---------------------------------------*/
        private void getSumEraning_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateTimeInput2.Text=="")
                {
                    MessageBox.Show("Please Select Date", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                   // MessageBox.Show("Please Select Date");
                }
                else
                {
                    MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT SUM(Total)FROM total2 WHERE `date` ='" + dateTimeInput2.Text + "'", conn);
                    Object temp = cmd.ExecuteScalar();
                    string data = temp.ToString();
                    label17.Text = "Rs:"+data+".00";
                    //label17.Text = data ;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               // MessageBox.Show(ex.Message);
            }
        }
/*-----------------------Get Total Sell Items -----------------------------------------------*/
        private void buttonX5_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT SUM(Qty)FROM vehicle WHERE `date` ='" + dateTimeInput3.Text + "' AND `Name`='"+ comboBoxEx1 .Text+ "'", conn);
            Object temp = cmd.ExecuteScalar();
            string data = temp.ToString();
            if (data == "")
            {
                label19.Text = "0";
            }
            else
            {
                label19.Text = data;
            }
            
        }

/*---------------------------Get Database Values To Combo Box Select Item Name--------------------------*/
        void fillCombo3()
        {
            try
            {

                MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `items`", conn);
                conn.Open();
                MySqlDataReader msr = cmd.ExecuteReader();
                while (msr.Read())
                {
                    string sname2 = msr.GetString("Item_Name");
                    comboBoxEx1.Items.Add(sname2);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        /*---------------------------Get Data To Data Grid View In Database------------------------------------------*/
        void loadTable5()
        {
            MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
            MySqlCommand cmd = new MySqlCommand("SELECT  `Item_Name`, `Qty`, `Price`, `Category` FROM `items`", conn);
            try
            {
                conn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable dtnew = new DataTable();
                sda.Fill(dtnew);
                BindingSource bs = new BindingSource();
                bs.DataSource = dtnew;
                dataGridViewX5.DataSource = bs;
                sda.Update(dtnew);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        /*----------------------------------Clear Data-------------------------------------*/
        void cleardata()
        {
            label17.Text = "";
            label19.Text = "";
            dateTimeInput2.ResetText();
            comboBoxEx1.Text = "";
            dateTimeInput3.ResetText();
            textBoxX1.Text = "";
            dateTimeInput1.ResetText();
            txtitemAddVehicle.Items.Clear();
            comboBoxEx1.Items.Clear();
            txtItemCodeAddItem.Clear();
            txtItemNameAddItem.Clear();
            txtQtyAddItem.Clear();
            txtPriceAddItem.Clear();
            TxtItemCatagoryAddItem.ResetText();
            txtItemSearchAddVehicle.ResetText();
            txtCategoryNameCategory.Clear();
            txtIdCategory.ResetText();
            lblInvoiceNumber.ResetText();
        }

        

        private void dataGridViewX5_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
          /* try
            {
                foreach (DataGridViewRow row in dataGridViewX5.Rows)
                {
                    int qty = Convert.ToInt32(row.Cells[1].Value);
                    if (qty < 10)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }

                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
            
            
        }

        private void dataGridViewX2_SizeChanged(object sender, EventArgs e)
        {
            
            
        }

        private void dataGridViewX2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel159_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel218_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel219_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDeveloper_Click(object sender, EventArgs e)
        {
            if (totaltxt.Text != "Rs: 00.00" && totaltxt.Text != "Rs: 0.00")
            {
                MessageBox.Show("Please Save Data", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show("Please Save Data");
            }
            else if (lblVehicleNumber.Text != "")
            {
                MessageBox.Show("Clear Vehicle Number", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show("Clear Vehicle Number");
            }
            else
            {
                panalDeveloper.Show();
                panalItemCategory.Hide();
                panalAddItem.Hide();
                panalAddVehicle.Hide();
                panalSearch.Hide();
                panelDashboard.Hide();
                cleardata();
            }
        }

        private void btnDeveloper2_Click(object sender, EventArgs e)
        {
            if (totaltxt.Text != "Rs: 00.00" && totaltxt.Text != "Rs: 0.00")
            {
                MessageBox.Show("Please Save Data", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show("Please Save Data");
            }
            else if (lblVehicleNumber.Text != "")
            {
                MessageBox.Show("Clear Vehicle Number", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //MessageBox.Show("Clear Vehicle Number");
            }
            else
            {
                panalDeveloper.Show();
                panalItemCategory.Hide();
                panalAddItem.Hide();
                panalAddVehicle.Hide();
                panalSearch.Hide();
                panelDashboard.Hide();
                cleardata();
            }
        }
    }
}
