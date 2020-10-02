using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Luxand;
using System.Data.SqlClient;
using System.IO;


namespace AuthenticationSystem
{
    public partial class Form1 : Form
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
       
        int mouseX = 0;
        int mouseY = 0;

        // WinAPI procedure to release HBITMAP handles returned by FSDKCam.GrabFrame
        [DllImport("gdi32.dll")]
        static extern bool DeleteObject(IntPtr hObject);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
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

            //int VideoFormat = 0; // choose a video format
            //pictureBox1.Width = formatList[VideoFormat].Width;
            //pictureBox1.Height = formatList[VideoFormat].Height;
            //this.Width = formatList[VideoFormat].Width + 48;
            //this.Height = formatList[VideoFormat].Height + 96;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           // needClose = true;
        }

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
            btnRemember.Enabled = true;

            // set realtime face detection parameters
            FSDK.SetFaceDetectionParameters(false, false, 100);
            FSDK.SetFaceDetectionThreshold(3);

            // list where we store face templates
            // faceTemplates = new List();
            faceTemplates = new List<FaceTemplate>();


            while (!needClose)
            {
               

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


                                try
                                {



                                    cmd = new SqlCommand("insert into facetb values(@Name,@face)", con);
                                    cmd.Parameters.AddWithValue("@Name", userName);

                                    cmd.Parameters.AddWithValue("@face", template.templateData);

                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                    MessageBox.Show("Record Save!");

                                    programState = ProgramState.psRecognize;
                                }

                                catch (Exception ex)
                                {

                                }


                            }
                            break;

                        case ProgramState.psRecognize: // recognize the user
                            bool match = false;


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

                                label3.Text = userName;
                            }
                            else
                            {
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

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //programState = ProgramState.psRemember;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            mouseX = 0;
            mouseY = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            needClose = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        string actype;

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (label3.Text == "UnKnow FACE")
            {
                MessageBox.Show("Please Register Your Name");

            }
            else
            {

                needClose = true;


                MessageBox.Show("Face Info Saved!");

                NewUser nn = new NewUser();
                nn.userid = label3.Text;
                nn.Show();




            }
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
        int acc;

        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void btnRemember_Click(object sender, EventArgs e)
        {
            faceTemplates.Clear();
            programState = ProgramState.psRemember;
            label1.Text = "Look at the camera";
        }
    }
}
