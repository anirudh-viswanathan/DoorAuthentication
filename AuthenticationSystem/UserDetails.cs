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
    public partial class UserDetails : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\USER\Desktop\project\AuthenticationSystem\AuthenticationSystem\authedb.mdf;Integrated Security=True;User Instance=True");
        SqlCommand cmd;

        public UserDetails()
        {
            InitializeComponent();
        }

        private void UserDetails_Load(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select * from regtb", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
