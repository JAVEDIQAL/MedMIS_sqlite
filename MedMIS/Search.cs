using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SQLite;
namespace MedMIS
{
    public partial class Search : Form
    {
        SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=E:\\MMIS.sqlite;Version=3;");
        public Search()
        {
            InitializeComponent();
        }

        private void Search_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_mdcn_Click(object sender, EventArgs e)
        {

            try
            {
                using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=E:\\MMIS.sqlite;Version=3;"))
                {
                    m_dbConnection.Open();
                    string query="Select med_name ,stock from tbl_mdcnentry where med_name LIKE  @medname  ";
                    using (SQLiteCommand command = new SQLiteCommand(query, m_dbConnection))
                    {

                        command.Parameters.Add("@medname", System.Data.DbType.String).Value = txtsearchmdcn.Text.ToString();
                        SQLiteDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
        
                                label1.Text = "Available " + reader.GetValue(0).ToString() + " stock is " + reader.GetValue(1).ToString();
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());

            }



            //}
            //string medvalue = txtsearchmdcn.Text.ToString();
            //SQLiteDataReader reader = null;
            //try
            //{
            //    m_dbConnection.Open();

            //    // 1. declare command object with parameter
            //    SQLiteCommand cmd = new SQLiteCommand("Select med_name ,stock from tbl_mdcnentry where med_name LIKE '%'+ @medname + '%'", m_dbConnection);

            //    // 2. define parameters used in command object
            //    SQLiteParameter param = new SQLiteParameter();
                
            //    param.ParameterName = "@medname";
            //    param.Value = medvalue;

            //    // 3. add new parameter to command object
            //    cmd.Parameters.Add(param);
            //    // get data stream
            //    reader = cmd.ExecuteReader();
            //    // write each record

            //    while (reader.Read())
            //    {

            //      MessageBox.Show(reader.GetValue(0).ToString());

            //      label1.Text = "Available " + reader.GetValue(0).ToString() + " stock is " + reader.GetValue(1).ToString();
            //    }
            //}
            //finally
            //{
            //    // close reader
            //    if (reader != null)
            //    {
            //        reader.Close();
            //    }

            //    // close connection
            //    if (m_dbConnection != null)
            //    {
            //       m_dbConnection.Close();
            //    }
            //}
            




















        }
        private void txtsearchmdcn_TextChanged(object sender, EventArgs e)
        {

        }



    }
}
