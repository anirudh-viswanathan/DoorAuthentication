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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminLogin bb = new AdminLogin();
            bb.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
         
        }


       
        public static Random r = new Random();
        public static int number;
        private void button4_Click(object sender, EventArgs e)
        {
            //work
            //if (textBox1.Text == "")
            //{
            //    //Response.Write("first enter you count number");

            //    MessageBox.Show("");
            //}
            //else
            //{
            //    number = Convert.ToInt32(textBox1.Text);
            //    List<int> available = new List<int>(number);
            //    for (int i = 1; i <= number; i++)
            //        available.Add(i);
            //    List<int> result = new List<int>(number);
            //    while (available.Count > 0)
            //    {
            //        int index = r.Next(available.Count);
            //        result.Add(available[index]);
            //        available.RemoveAt(index);
            //    }

            //    listBox1.Items.Clear();
            //    for (int i = 0; i < result.Count; i++)
            //    {
            //       // Response.Write(result[i] + "-");

            //        listBox1.Items.Add(result[i].ToString());
            //    }
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login ll = new Login();
            ll.Show();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Random rr = new Random();
            int i = rr.Next(0, 9);
           // label2.Text = i.ToString();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        
    }
}
