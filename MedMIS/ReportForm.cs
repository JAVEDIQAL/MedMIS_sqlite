﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Data.SQLite;
namespace MedMIS
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=E:\\MMIS.sqlite;Version=3;");           
            InitializeComponent();
            string query = "Select * from tbl_Mdcl";
            DataSet ds = new DataSet();
            m_dbConnection.Open();
            SQLiteCommand command=new SQLiteCommand(query,m_dbConnection);
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            da.Fill(ds,"tbl_Mdcl");
            
            ReportDocument cryRpt = new ReportDocument();
            cryRpt.Load(@"D:\Javed\PROGRAMMING BOOKS\C#\MedMIS\MedMIS\Selling_rpt.rpt");
            cryRpt.SetDataSource(ds);
            crystalReportViewer1.ReportSource = cryRpt;
            crystalReportViewer1.DisplayToolbar = true;
            crystalReportViewer1.Refresh();

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void ReportForm_Load(object sender, EventArgs e)
        {

        }
    }
}
