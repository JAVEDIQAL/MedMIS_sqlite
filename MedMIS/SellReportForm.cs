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
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SQLite;
namespace MedMIS
{
    public partial class SellReportForm : Form
    {
        public SellReportForm()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=E:\\MMIS.sqlite;Version=3;");
            InitializeComponent();
            string query = "Select * from tbl_Mdcl";          
            DataSet ds = new DataSet();
            m_dbConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(query,m_dbConnection);            
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            da.Fill(ds,"tbl_Mdcl");
            
            ReportDocument cryRpt = new ReportDocument();
            cryRpt.Load(@"D:\Javed\PROGRAMMING BOOKS\C#\MedMIS\MedMIS\Months_rpt.rpt");
            cryRpt.SetDataSource(ds);
            SellCRpt .ReportSource = cryRpt;
            SellCRpt.DisplayToolbar = true;
            SellCRpt.Refresh();

        }



















        

        private void SellCRpt_Load(object sender, EventArgs e)
        {

        }

        private void SellReportForm_Load(object sender, EventArgs e)
        {

        }
    }
}
