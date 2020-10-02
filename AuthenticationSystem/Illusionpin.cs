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
    public partial class Illusionpin : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\USER\Desktop\project\AuthenticationSystem\AuthenticationSystem\authedb.mdf;Integrated Security=True;User Instance=True");
        SqlCommand cmd;


        public string id, pass;

        public Illusionpin()
        {
            InitializeComponent();
        }


        public static Random r = new Random();
        public static int number;
        string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString();


        string  s1, s2, s3, s4, s5, s6, s7, s8, s9,s10;


        private void Illusionpin_Load(object sender, EventArgs e)
        {
             string ss = "9";

            number = Convert.ToInt32(ss);
            List<int> available = new List<int>(number);
            for (int i = 0; i <= number; i++)
                available.Add(i);
            List<int> result = new List<int>(number);
            while (available.Count > 0)
            {
                int index = r.Next(available.Count);
                result.Add(available[index]);
                available.RemoveAt(index);
            }


          

            for (int i = 0; i < result.Count; i++)
            {

              

                if (i == 0)
                {
                    pictureBox1.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");

                    s1 = result[i].ToString();

                }
                else if (i == 1)
                {
                    pictureBox2.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");

                    s2 = result[i].ToString();


                }
                else if (i == 2)
                {
                    pictureBox3.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");

                    s3 = result[i].ToString();
                }
                else if (i == 3)
                {
                    pictureBox4.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");

                    s4 = result[i].ToString();
                }
                else if (i == 4)
                {
                    pictureBox5.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");
                    s5 = result[i].ToString();
                }
                else if (i == 5)
                {
                    pictureBox6.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");
                    s6 = result[i].ToString();
                }
                else if (i == 6)
                {
                    pictureBox7.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");
                    s7 = result[i].ToString();
                }
                else if (i == 7)
                {
                    pictureBox8.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");

                    s8 = result[i].ToString();
                }
                else if (i == 8)
                {
                    pictureBox9.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");

                    s9 = result[i].ToString();
                }
                else if (i == 9)
                {
                    pictureBox10.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");
                    s10 = result[i].ToString();
                }


            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

          


            if (textBox1.Text.Length < 4)
            {
                textBox1.Text = textBox1.Text + s1;
            }
            else
            {
                MessageBox.Show("Four Digit Number Only");

            }

            
        }

      
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            

            if (textBox1.Text.Length < 4)
            {
                textBox1.Text = textBox1.Text + s2;

            }
            else
            {
                MessageBox.Show("Four Digit Number Only");

            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
           

            if (textBox1.Text.Length < 4)
            {
                textBox1.Text = textBox1.Text + s3;
            }
            else
            {
                MessageBox.Show("Four Digit Number Only");

            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
           

            if (textBox1.Text.Length < 4)
            {
                textBox1.Text = textBox1.Text + s4;
            }
            else
            {
                MessageBox.Show("Four Digit Number Only");

            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            

            if (textBox1.Text.Length < 4)
            {
                textBox1.Text = textBox1.Text + s5;
            }
            else
            {
                MessageBox.Show("Four Digit Number Only");

            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
           
            if (textBox1.Text.Length < 4)
            {
                textBox1.Text = textBox1.Text + s6;
            }
            else
            {
                MessageBox.Show("Four Digit Number Only");

            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
           
            if (textBox1.Text.Length < 4)
            {
                textBox1.Text = textBox1.Text + s7;
            }
            else
            {
                MessageBox.Show("Four Digit Number Only");

            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
           

            if (textBox1.Text.Length < 4)
            {
                textBox1.Text = textBox1.Text + s8;
            }
            else
            {
                MessageBox.Show("Four Digit Number Only");

            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
           

            if (textBox1.Text.Length < 4)
            {
                textBox1.Text = textBox1.Text + s9;
            }
            else
            {
                MessageBox.Show("Four Digit Number Only");

            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            

            if (textBox1.Text.Length < 4)
            {
                textBox1.Text = textBox1.Text + s10;
            }
            else
            {
                MessageBox.Show("Four Digit Number Only");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {


            textBox1.Text = "";
            string ss = "9";

            number = Convert.ToInt32(ss);
            List<int> available = new List<int>(number);
            for (int i = 0; i <= number; i++)
                available.Add(i);
            List<int> result = new List<int>(number);
            while (available.Count > 0)
            {
                int index = r.Next(available.Count);
                result.Add(available[index]);
                available.RemoveAt(index);
            }




            for (int i = 0; i < result.Count; i++)
            {

                Console.Write(result[i]);


                if (i == 0)
                {
                    pictureBox1.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");

                    s1 = result[i].ToString();

                }
                else if (i == 1)
                {
                    pictureBox2.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");

                    s2 = result[i].ToString();


                }
                else if (i == 2)
                {
                    pictureBox3.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");

                    s3 = result[i].ToString();
                }
                else if (i == 3)
                {
                    pictureBox4.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");

                    s4 = result[i].ToString();
                }
                else if (i == 4)
                {
                    pictureBox5.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");
                    s5 = result[i].ToString();
                }
                else if (i == 5)
                {
                    pictureBox6.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");
                    s6 = result[i].ToString();
                }
                else if (i == 6)
                {
                    pictureBox7.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");
                    s7 = result[i].ToString();
                }
                else if (i == 7)
                {
                    pictureBox8.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");

                    s8 = result[i].ToString();
                }
                else if (i == 8)
                {
                    pictureBox9.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");

                    s9 = result[i].ToString();
                }
                else if (i == 9)
                {
                    pictureBox10.Image = new Bitmap(path + "\\Pin\\" + result[i] + ".png");
                    s10 = result[i].ToString();
                }


            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            con.Open();
            cmd = new SqlCommand("select * from regtb where UserId='" + id + "' and pin='" + textBox1.Text + "' ", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {


                MessageBox.Show("Login Successfully!");
              

            }
            else
            {
                MessageBox.Show("Pin Incorrect!");

            }
            con.Close();



           
        }


       
    }
}
