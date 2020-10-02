using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AuthenticationSystem
{
    public partial class AdminHome : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\USER\Desktop\project\AuthenticationSystem\AuthenticationSystem\authedb.mdf;Integrated Security=True;User Instance=True");
        SqlCommand cmd;


        public AdminHome()
        {
            InitializeComponent();
        }

        private void AdminHome_Load(object sender, EventArgs e)
        {

            cmd = new SqlCommand("select * from regtb", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();

        }

      

        private void userDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 ff = new Form1();
            ff.Show();

        }

        private void statementinfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Home hh = new Home();
            hh.Show();
            this.Close();
        }
    }
}
