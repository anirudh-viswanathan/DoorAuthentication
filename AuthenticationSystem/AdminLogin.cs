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
    public partial class AdminLogin : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\USER\Desktop\project\AuthenticationSystem\AuthenticationSystem\authedb.mdf;Integrated Security=True;User Instance=True");
        SqlCommand cmd;

        public AdminLogin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            array = ShuffledArray(array);


            foreach (int s in array)
            {
                Console.WriteLine(s);
            }





        }



        protected int[] ShuffledArray(int[] myArray)
        {
            int count = myArray.Length - 1;
            int[] newArray = new int[count + 1];

            Random rnd = new Random();
            var randomNumbers = Enumerable.Range(1, count).OrderBy(i => rnd.Next()).ToArray();

            int index = 0;
            foreach (int i in randomNumbers)
            {
                newArray[index] = myArray[i];
                index++;
            }

            return newArray;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" & textBox2.Text == "admin")
            {
                AdminHome safds = new AdminHome();
                safds.Show();
            }
            else
            {
                MessageBox.Show("Username or Password Incorrect!");

            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
