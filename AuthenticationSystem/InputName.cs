using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace AuthenticationSystem
{
    public partial class InputName : Form
    {

       

        public string userName;
        public Bitmap bmp;

        public InputName()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

     
       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            userName = textBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {

          

           
        }

        private void InputName_Load(object sender, EventArgs e)
        {
          

        }
        int acc;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
           

          

           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                MessageBox.Show("Please Enter Name");

            }
            else
            {
                this.Close();
            }

        }
    }
}
