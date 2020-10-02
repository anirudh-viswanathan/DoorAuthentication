using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Luxand;
using System.Data.SqlClient;

using System.IO;
using System.Net;
using System.Net.Mail;
using System.Drawing.Imaging;



namespace AuthenticationSystem
{
    public partial class Login : Form
    {
        public string uname;

        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\USER\Desktop\project\AuthenticationSystem\AuthenticationSystem\authedb.mdf;Integrated Security=True;User Instance=True");
        SqlCommand cmd;

        // program states: whether we recognize faces, or user has clicked a face
        enum ProgramState { psNormal, psRemember, psRecognize }
        ProgramState programState = ProgramState.psRecognize;


        struct FaceTemplate
        { // single template
            public byte[] templateData;
        }
        List<FaceTemplate> faceTemplates;

        string path = Path.GetDirectoryName(Application.ExecutablePath).ToString();
        String cameraName;
        bool needClose = false;
        string userName;
        String TrackerMemoryFile;
        int mouseX = 0;
        int mouseY = 0;

        // WinAPI procedure to release HBITMAP handles returned by FSDKCam.GrabFrame
        [DllImport("gdi32.dll")]
        static extern bool DeleteObject(IntPtr hObject);



        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {


            if (FSDK.FSDKE_OK != FSDK.ActivateLibrary("gyYgVWQTSzjiuGB/hH8dKgg0QrrIuhoHdfUCzD9rY+vru3WRZsaezTX6YWj9osdI/cmxY1NSdLkyWuugMPCxUG7/xNLegHLeaUpzVyKpDkaWL8tJIUsIL7xv9bhmgifPbAyTDuxF3VGxXmHkv/L/MStf9kdXV/A1vVvT93QC4vQ="))
            {
                MessageBox.Show("Please run the License Key Wizard (Start - Luxand - FaceSDK - License Key Wizard)", "Error activating FaceSDK", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            FSDK.InitializeLibrary();
            FSDKCam.InitializeCapturing();

            string[] cameraList;
            int count;
            FSDKCam.GetCameraList(out cameraList, out count);

            if (0 == count)
            {
                MessageBox.Show("Please attach a camera", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            cameraName = cameraList[0];

            FSDKCam.VideoFormatInfo[] formatList;
            FSDKCam.GetVideoFormatList(ref cameraName, out formatList, out count);




            timer1.Start();

        }

       
        IList<string> strList = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            int cameraHandle = 0;

            int r = FSDKCam.OpenVideoCamera(ref cameraName, ref cameraHandle);
            if (r != FSDK.FSDKE_OK)
            {
                MessageBox.Show("Error opening the first camera", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
           

            // set realtime face detection parameters
            FSDK.SetFaceDetectionParameters(false, false, 100);
            FSDK.SetFaceDetectionThreshold(3);

            // list where we store face templates
            // faceTemplates = new List();
            faceTemplates = new List<FaceTemplate>();


            while (!needClose)
            {
                // faceTemplates.Clear();

                Int32 imageHandle = 0;
                if (FSDK.FSDKE_OK != FSDKCam.GrabFrame(cameraHandle, ref imageHandle)) // grab the current frame from the camera
                {
                    Application.DoEvents();
                    continue;
                }

                FSDK.CImage image = new FSDK.CImage(imageHandle);

                Image frameImage = image.ToCLRImage();
                Graphics gr = Graphics.FromImage(frameImage);

                FSDK.TFacePosition facePosition = image.DetectFace();
                // if a face is detected, we can recognize it
                if (facePosition.w != 0)
                {
                    gr.DrawRectangle(Pens.LightGreen, facePosition.xc - facePosition.w / 2, facePosition.yc - facePosition.w / 2,
                        facePosition.w, facePosition.w);

                    // create a new face template
                    FaceTemplate template = new FaceTemplate();

                    template.templateData = new byte[FSDK.TemplateSize];
                    FaceTemplate template1 = new FaceTemplate();
                    if (programState == ProgramState.psRemember || programState == ProgramState.psRecognize)
                        template.templateData = image.GetFaceTemplateInRegion(ref facePosition);


                    switch (programState)
                    {
                        case ProgramState.psNormal: // normal state - do nothing
                            break;

                        case ProgramState.psRemember: // Remember Me state - store facial templates

                            label1.Text = "Templates stored: " + faceTemplates.Count.ToString();
                            faceTemplates.Add(template);
                            if (faceTemplates.Count > 9)
                            {
                                // get the user name
                                InputName inputName = new InputName();
                                inputName.ShowDialog();
                                userName = inputName.userName;






                                cmd = new SqlCommand("insert into facetb values(@Name,@face)", con);
                                cmd.Parameters.AddWithValue("@Name", userName);

                                cmd.Parameters.AddWithValue("@face", template.templateData);

                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                                MessageBox.Show("Record Save!");

                                programState = ProgramState.psRecognize;



                            }
                            break;

                        case ProgramState.psRecognize: // recognize the user
                            bool match = false;


                            con.Open();
                            cmd = new SqlCommand("select * from facetb ORDER BY id ASC ", con);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {

                                template1.templateData = (byte[])dr["face"];
                                faceTemplates.Add(template1);


                                strList.Add(dr["Name"].ToString());


                            }
                            con.Close();





                            int ii = 0;

                            foreach (FaceTemplate t in faceTemplates)
                            {

                                float similarity = 0.0f;
                                FaceTemplate t1 = t;
                                FSDK.MatchFaces(ref template.templateData, ref t1.templateData, ref similarity);
                                float threshold = 0.0f;
                                FSDK.GetMatchingThresholdAtFAR(0.01f, ref threshold); // set FAR to 1%
                                if (similarity > threshold)
                                {


                                    userName = strList[ii].ToString();

                                    label3.Text = strList[ii].ToString();
                                    match = true;
                                    break;
                                }

                                ii++;




                            }

                            con.Close();



                            if (match)
                            {
                                StringFormat format = new StringFormat();
                                format.Alignment = StringAlignment.Center;

                                gr.DrawString(userName, new System.Drawing.Font("Arial", 16),
                                    new System.Drawing.SolidBrush(System.Drawing.Color.LightGreen),
                                    facePosition.xc, facePosition.yc + facePosition.w * 0.55f, format);
                               // abc = 0;
                                send();
                            }

                            else
                            {
                                abc = 0;
                                label3.Text = "UnKnow FACE";

                               
                            }
                            break;
                    }
                }

                // display current frame
                pictureBox1.Image = frameImage;

                GC.Collect(); // collect the garbage after the deletion

                // make UI controls accessible
                Application.DoEvents();
            }

            FSDKCam.CloseVideoCamera(cameraHandle);
            FSDKCam.FinalizeCapturing();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            needClose = true;
        }

        int abc;

        private void send()
        {

            abc++;

            //label4.Text = abc.ToString();

            if (abc == 20)
            {


               // Random aa = new Random();
               // int aaa = aa.Next(1111, 9999);




               //// sendmessage("7010063677", " Try UnKnow User Access your Account! " + " Login OTP :" + aaa);

               // string to = "sangeeth5535@gmail.com";
               // string from = "sampletest685@gmail.com";

               // string password = "mailtest2";
               // using (MailMessage mm = new MailMessage(from, to))
               // {
               //     mm.Subject = "Alert";
               //     mm.Body = "UnKnow User Access your Account" + " Login OTP :" + aaa;

               //     Image image = pictureBox1.Image;
               //     System.IO.MemoryStream stream = new System.IO.MemoryStream();
               //     image.Save(stream, ImageFormat.Jpeg);
               //     stream.Position = 0;

               //     mm.Attachments.Add(new Attachment(stream, "Screenshot.jpg"));

               //     mm.IsBodyHtml = false;
               //     SmtpClient smtp = new SmtpClient();
               //     smtp.Host = "smtp.gmail.com";
               //     smtp.EnableSsl = true;
               //     NetworkCredential NetworkCred = new NetworkCredential(from, password);
               //     smtp.UseDefaultCredentials = true;
               //     smtp.Credentials = NetworkCred;
               //     smtp.Port = 587;
               //     smtp.Send(mm);
               //     MessageBox.Show("Mail Send!");

               // }

               // needClose = true;


                con.Open();
                cmd = new SqlCommand("select * from regtb where UserId='" + label3.Text  + "' ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {


                    Illusionpin op = new Illusionpin();
                    op.id = label3.Text;
                    op.Show();

                    abc = 0;
                }
                else
                {
                    //MessageBox.Show("Pin Incorrect!");

                }
                con.Close();


                needClose = true;
                this.Close();

                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }
       
        public void sendmessage(string targetno, string message)
        {

            String query = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=fantasy5535&password=1163974702&sendername=Sample&mobileno=" + targetno + "&message=" + message;
            WebClient client = new WebClient();
            Stream sin = client.OpenRead(query);
            // Response.Write("<script> alert('Message Send') </script>");
            MessageBox.Show("Message Send");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        int start=0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            start++;
            if (start == 50)
            {

                button1.PerformClick();

                start = 0;

                timer1.Stop();

            }
        }
    }
}
