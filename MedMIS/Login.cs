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
    public partial class Login : Form
    {
        

        public Login()
        {
            InitializeComponent();
            Pass.PasswordChar = '*';
            cpopass.PasswordChar = '*';
            Crepass.PasswordChar='*';
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            
            

        }

        private void Submit_Click(object sender, EventArgs e)
        {
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=E:\\MMIS.sqlite;Version=3;");                   
            string str = "select * from  tbl_login";
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand(str, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if ((UserName.TextLength!=0) && (Pass.TextLength!=0))
                {
                    if (string.Equals(UserName.Text.ToString(), reader.GetValue(0).ToString(),StringComparison.InvariantCultureIgnoreCase) && (Pass.Text.ToString()==reader.GetValue(1).ToString()))
                    {

                        MedMenu mm = new MedMenu();
                        mm.Show();
                        break;

                        // rf.Show();

                    }
                    else
                    {
                        MessageBox.Show("Incorrect username/password");
                        break;

                    }

                }
                else {


                    MessageBox.Show("Please fill up all the fields before submitting");
                    break;
                
                }
            }
            m_dbConnection.Close();
            
          }

        private void OK_Click(object sender, EventArgs e)
        {
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=E:\\MMIS.sqlite;Version=3;");  
            string getquery = "select username ,Password from tbl_login";
            m_dbConnection.Open();
            SQLiteCommand getcommand = new SQLiteCommand(getquery, m_dbConnection);
            SQLiteDataReader getreader = getcommand.ExecuteReader();
            while (getreader.Read())
            {

                //MessageBox.Show(getreader.GetValue(0).ToString());

                //MessageBox.Show(getreader.GetValue(1).ToString());
                if ((CPUser.Text.ToString() == getreader.GetValue(0).ToString()) && (cpopass.Text.ToString() == getreader.GetValue(1).ToString()) && Crepass.Text.ToString() != "")
                {
                    string insertquery = "Update  tbl_login  SET Password=@pass where username=@name";
                    SQLiteCommand insertcommand = new SQLiteCommand(insertquery, m_dbConnection);

                    insertcommand.Parameters.Add("@pass", System.Data.DbType.String).Value= Crepass.Text.ToString();
                    insertcommand.Parameters.Add("@name", System.Data.DbType.String).Value = CPUser.Text.ToString();
                    var rows = insertcommand.ExecuteNonQuery();
                    MessageBox.Show(rows.ToString());





                }

            }




        }

        private void Login_Load(object sender, EventArgs e)
        {

        }






   }

  }
























            //using (SqlCommand command = new SqlCommand(str, connection))
            //{ 
            //       command.Parameters.Add("@username",SqlDbType.NVarChar).Value=UserName.Text.ToString();
            //       command.Parameters.Add("@Password",SqlDbType.NVarChar).Value=Pass.Text.ToString();
            //       var rowsaffected = command.ExecuteNonQuery();
            //       connection.Close();
            //       MessageBox.Show(rowsaffected.ToString());
            //       MessageBox.Show("Record inserted. Please check your table data. :)");
            
            
            //}
           
  
            
           
        
 

