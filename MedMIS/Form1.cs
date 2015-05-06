using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SQLite;
using CrystalDecisions.CrystalReports.Engine;

namespace MedMIS
{
    public partial class Form1 : Form
    {
       
         int avlble_stck; 
         Int64 totl_prc;
         Int32 prc, dsc,int_qty;
         float totldsc, dsc_prcnt, retailpric;
         int rem_stock;
        public Form1()
        {
           

            InitializeComponent();
            CustomControls();          
            ShowListView();
            PopulateCombobox();
            PopulatePartyIdCombobox();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          

           

        }

        private void PopulateCombobox()
        {
            using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=E:\\MMIS.sqlite;Version=3;"))
            {
                m_dbConnection.Open();
                string str = "select med_name from  tbl_mdcnentry";
                using (SQLiteCommand cmd = new SQLiteCommand(str, m_dbConnection))
                {
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (!combomedicine.Items.Contains(reader[0]))
                            combomedicine.Items.Add(reader[0]);
                        //if (!combocompany.Items.Contains(reader[1]))
                        //    combocompany.Items.Add(reader[1]);

                    }
                    reader.Close();
                }
            }

        }
        public void PopulatePartyIdCombobox()
        {
            using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=E:\\MMIS.sqlite;Version=3;"))
            {
                m_dbConnection.Open();
                string str = "select distinct CP_id from  tbl_partyinfo";
                using (SQLiteCommand cmd = new SQLiteCommand(str, m_dbConnection))
                {
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (!PartId.Items.Contains(reader[0]))
                            PartId.Items.Add(reader[0]);

                    }
                    reader.Close();
                    m_dbConnection.Close();

                }
            }
        }
         private void CustomControls()
         {
             dateTimePicker1.Format = DateTimePickerFormat.Custom;
             dateTimePicker1.CustomFormat = "";
             dateTimePicker2.Format = DateTimePickerFormat.Custom;
             dateTimePicker2.CustomFormat = "";
             dateTimePicker3.Format = DateTimePickerFormat.Custom;
             dateTimePicker3.CustomFormat = "";
             dateTimePicker1.Value = Date.Today;
             lstLocal.View = View.Details;
             lstLocal.BackColor = System.Drawing.Color.Aqua;
             lstLocal.Columns.Add("Invoice No.", 80, HorizontalAlignment.Left);
             lstLocal.Columns.Add("Invoice Date", 130, HorizontalAlignment.Left);
             lstLocal.Columns.Add("Party/Customer ID",130,HorizontalAlignment.Left);
             lstLocal.Columns.Add("Medicine Name", 90, HorizontalAlignment.Left);
             lstLocal.Columns.Add("Company Name", 90, HorizontalAlignment.Left);
             lstLocal.Columns.Add("Mfg.Date", 130, HorizontalAlignment.Left);
             lstLocal.Columns.Add("Exp.Date", 130, HorizontalAlignment.Left);
             lstLocal.Columns.Add("Qty.", 80, HorizontalAlignment.Left);
             lstLocal.Columns.Add("Price",80,HorizontalAlignment.Left);
             lstLocal.Columns.Add("Discount", 80, HorizontalAlignment.Left);
             lstLocal.Columns.Add("Retail Price.", 80, HorizontalAlignment.Left);
             lstLocal.GridLines = true;
             lstLocal.FullRowSelect = true;                            
         }

         private void ShowListView()
         {

             lstLocal.Items.Clear();
             using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=E:\\MMIS.sqlite;Version=3;"))
             {
                 m_dbConnection.Open();
                 lstLocal.View = View.Details;
                 lstLocal.FullRowSelect = true;
                 string str = "select * from  tbl_Mdcl";
                 using (SQLiteCommand cmd = new SQLiteCommand(str, m_dbConnection))
                 {
                     SQLiteDataReader reader = cmd.ExecuteReader();
                     while (reader.Read())
                     {
                         //MessageBox.Show(reader.GetValue(0).ToString());
                         //MessageBox.Show(reader.GetValue(1).ToString());
                         //MessageBox.Show(reader.GetValue(2).ToString());
                         ListViewItem lv = new ListViewItem(reader.GetValue(0) + " ");
                         Date list_parse_indte = Date.Parse(reader.GetValue(1) + " ");

                         lv.SubItems.Add(list_parse_indte.ToShortString() + " ");

                         lv.SubItems.Add(reader.GetValue(2) + "");

                         lv.SubItems.Add(reader.GetValue(3) + "");

                         lv.SubItems.Add(reader.GetValue(4) + "");
                         Date list_parse_mdte = Date.Parse(reader.GetValue(5) + "");

                         lv.SubItems.Add(list_parse_mdte.ToShortString() + "");
                         Date list_parse_exdte = Date.Parse(reader.GetValue(6) + "");

                         lv.SubItems.Add(list_parse_exdte.ToShortString() + "");

                         lv.SubItems.Add(reader.GetValue(7) + "");

                         lv.SubItems.Add(reader.GetValue(8) + "");

                         lv.SubItems.Add(reader.GetValue(9) + "");

                         lv.SubItems.Add(reader.GetValue(10) + "");
                         lstLocal.Items.Add(lv);


                     }

                     reader.Close();

                 }
             }
         }
        private void display_all()
        {
        
        
        
        
        }
        private void Exit_Click(object sender, EventArgs e)
        {
           

        }
        private void button1_Click(object sender, EventArgs e)
        {
          

        }

        private void InvoiceNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddNew_Click(object sender, EventArgs e)
        {
            InvoiceNo.Clear();
            qty.Clear();
            dateTimePicker1.Value=Date.Today ;
            dateTimePicker2.Value = Date.Today;
            dateTimePicker3.Value = Date.Today;
            combomedicine.Items.Clear();
            combocompany.Items.Clear();
            discount.Clear();
            Price.Clear();
            discount.Clear();
            totalamnt.Clear();
            totaldscnt.Clear();
            Save.Enabled = true;
            AddNew.Enabled = false;
            PopulateCombobox();
            PopulatePartyIdCombobox();
        }

        public void Save_Click(object sender, EventArgs e)
        {

            //check and validation?
            if (InvoiceNo.TextLength != 0 && PartId.SelectedItem != null && combomedicine.SelectedItem != null && combocompany.SelectedItem != null && qty.TextLength != 0 && discount.TextLength != 0 && Price.TextLength != 0 && dateTimePicker1.Value != null && dateTimePicker2.Value != null && dateTimePicker3.Value != null)
            {

                if (!ValidateInputboxes(InvoiceNo))
                {
                    MessageBox.Show("Please enter only numeric in Invoice No.field");
                }

                if (!ValidateInputboxes(qty))
                {
                    MessageBox.Show("Please enter only numeric in Qty field");
                }
                if (!ValidateInputboxes(discount))
                {
                    MessageBox.Show("Please enter only numeric in discount.field");
                }
                if (!ValidateInputboxes(Price))
                {
                    MessageBox.Show("Please enter only numeric in Price field");
                }

                if (CheckAvailableStock(avlble_stck))
                {
                   
                    CustomerReport();
                }

            }

            else
            {


                MessageBox.Show("Please fill in all the fields before saving");

            }
           
        }

        //helper  function to validate input
        public bool ValidateInputboxes(TextBox tb)
         { 

              int output;
              if (!int.TryParse(tb.Text, out output))
              {

                 

                  return false;

              }
              else
              {



                  return true;
              
              }
              
        
        }

        //helper function to check available stock from database
        public bool CheckAvailableStock(int stock)
        {
            int output;
            if (int.TryParse(qty.Text, out output))
            {

                if (output > stock || output==0)
                {

                    MessageBox.Show("Please select in the available stock of" + avlble_stck);
                    return false;

                }
                else {

                        //Update stock value in tbl_mdcnentry
                        rem_stock=stock-output;
                        UpdateStock(rem_stock);
                         return true;
                
                
                     }

                
            }
            else
            {
                MessageBox.Show("Parsing failed for avaialable stock");
                return false;
                
            
            }
           

        }
        public void CustomerReport()
        {

            try
            {
                using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=E:\\MMIS.sqlite;Version=3;"))
                {
                    //string conn = "Server=localhost\\SQLEXPRESS;integrated security=SSPI;MultipleActiveResultSets=true;database=MMIS";
                    String query = "INSERT INTO  tbl_Mdcl (Invoice_No,Invoice_Date,CustomerId,Med_Name,Comp_Name,Date_Manaf,Date_Exp,Qty,Price,Discount,Retail) VALUES (@Invoice_No,@Invoice_Date,@CustomerId,@Med_Name,@Comp_Name,@Manaf_Date,@Exp_Date,@Qty,@Price,@Discount,@Retail)";
                    m_dbConnection.Open();
                    using (SQLiteCommand icommand = new SQLiteCommand(query, m_dbConnection))
                    {

                      

                        //calculating total price after discount
                        prc = Convert.ToInt32(Price.Text);
                        dsc = Convert.ToInt32(discount.Text);
                        int_qty = Convert.ToInt32(qty.Text);
                        totl_prc = prc * int_qty;
                        if (dsc != 0)
                        {
                            dsc_prcnt = (float)dsc / 100;
                            //discount amount
                            totldsc = (float)totl_prc * dsc_prcnt;
                            retailpric = totl_prc - totldsc;
                            // MessageBox.Show(totl_prc.ToString());
                        }
                        else
                        {
                            totldsc = 0;
                            retailpric = totl_prc - totldsc;



                        }



                        ////a shorter syntax to adding parameter
                        
                        icommand.Parameters.Add("@Invoice_No", System.Data.DbType.Int32).Value = InvoiceNo.Text.ToString();
                        icommand.Parameters.Add("@Invoice_Date", System.Data.DbType.Date).Value = dateTimePicker1.Value;
                        icommand.Parameters.Add("@CustomerId", System.Data.DbType.Int32).Value = PartId.Text;
                        icommand.Parameters.Add("@Med_Name", System.Data.DbType.String).Value = combomedicine.Text;
                        icommand.Parameters.Add("@Comp_Name", System.Data.DbType.String).Value = combocompany.Text;
                        icommand.Parameters.Add("@Manaf_Date", System.Data.DbType.Date).Value = dateTimePicker2.Value;
                        icommand.Parameters.Add("@Exp_Date", System.Data.DbType.Date).Value = dateTimePicker3.Value;
                        icommand.Parameters.Add("@Qty", System.Data.DbType.Int32).Value = qty.Text;
                        icommand.Parameters.Add("@Price", System.Data.DbType.Int32).Value = totl_prc;
                        icommand.Parameters.Add("@Discount", System.Data.DbType.Int32).Value = totldsc;
                        icommand.Parameters.Add("@Retail", System.Data.DbType.Int32).Value = retailpric;
                        totalamnt.Text = retailpric.ToString();
                        totaldscnt.Text = totldsc.ToString();

                        ////make sure you open and close(after executing) the connection
                        var rowsaffected = icommand.ExecuteNonQuery();
                       
                        // MessageBox.Show(rowsaffected.ToString());
                        MessageBox.Show("Record inserted. Please check your table data. :)");
                        ShowListView();

                    }
                }
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);

            }

           
        }
       

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Refreshbtn_Click(object sender, EventArgs e)
        {
            lstLocal.Items.Clear();
            
            ShowListView();
        }

        private void Printbtn_Click(object sender, EventArgs e)
        {
            
            
            
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        public void Add_party_Click(object sender, EventArgs e)
        {
            Add_Party ap = new Add_Party();
            ap.Show();
            PopulatePartyIdCombobox();
            this.Close();
           
        }

        private void combomedicine_SelectedIndexChanged(object sender, EventArgs e)
        {
            combocompany.Items.Clear();
            if (!GetAutoCombobox_filled())
            {
                Save.Enabled = false;

            }
            else
            {

                Save.Enabled = true;
            
            
            }
        }

        public bool GetAutoCombobox_filled()
        {
            using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=E:\\MMIS.sqlite;Version=3;"))
           {
              m_dbConnection.Open();
             // int select_item;
             string str = "select med_name,mnf_name,mfg_date,exp_date,stock from  tbl_mdcnentry";
             using (SQLiteCommand icommand = new SQLiteCommand(str, m_dbConnection))
             {
                 SQLiteDataReader reader = icommand.ExecuteReader();
                 while (reader.Read())
                 {

                     if (combomedicine.SelectedItem.ToString() == (string)reader["med_name"])
                     {
                         combocompany.Items.Add(reader.GetValue(1).ToString());
                         dateTimePicker2.Text = reader.GetValue(2).ToString();
                         dateTimePicker3.Text = reader.GetValue(3).ToString();
                         avlble_stck = Convert.ToInt32(reader.GetValue(4));
                         if (avlble_stck == 0)
                         {

                             MessageBox.Show("No stock of " + combomedicine.SelectedItem.ToString() + " right now.");
                             reader.Close();
                             return false;

                         }




                         break;
                     }

                 }
                 reader.Close();
             }
           }
                return true;
            
        }
        public void UpdateStock(int new_stock)
        {
            using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=E:\\MMIS.sqlite;Version=3;"))
            {
              String str =  "Update tbl_mdcnentry set stock= @stock where med_name=@med_name";
                m_dbConnection.Open();
               
                using (SQLiteCommand insertCommand = new SQLiteCommand(str, m_dbConnection))
                    {
                        try
                        {
                           insertCommand.Parameters.Add("@stock", System.Data.DbType.Int32).Value = new_stock;
                           insertCommand.Parameters.Add("@med_name",System.Data.DbType.String).Value = combomedicine.SelectedItem.ToString();
                           var rowsaffected = insertCommand.ExecuteNonQuery();
                           m_dbConnection.Close();
                          // MessageBox.Show(rowsaffected.ToString());
                        
                        
                        }
                        catch (Exception e)
                          {

                             MessageBox.Show(e.Message.ToString()); 

                          }


                     }


                }

            }

        private void prnt_rpt_Click(object sender, EventArgs e)
        {
            ReportForm rf = new ReportForm();
            rf.Show();
        }

            


        








       
    }
}
