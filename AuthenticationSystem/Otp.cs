using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AuthenticationSystem
{
    public partial class Otp : Form
    {

        public string otp;

        public Otp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (otp  == textBox1 .Text )
            {
                MessageBox.Show("Login");

            }
            else
            {

               
                MessageBox.Show("Otp Incorrect!");

                Login ll = new Login();
                ll.Show();
                this.Close();





            }
        }
    }
}
