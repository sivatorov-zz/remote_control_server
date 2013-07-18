using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Management;
using System.Drawing.Imaging;

namespace remote_control_server
{
    public partial class form_remote_control_server : Form
    {
        public form_remote_control_server()
        {
            InitializeComponent();
        }

        TcpListener mytcpl;
        Socket mysocket;
        NetworkStream myns;

        Thread myth;

        string host;
        System.Net.IPAddress ip;
        DateTime dt;

        private static Bitmap bmpScreenshot;

        private static Graphics gfxScreenshot;

        FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(Application.StartupPath + @"\remote_control_server.exe");


        private void form_remote_control_server_Load(object sender, EventArgs e)
        {
            ni_remote_control_server.BalloonTipTitle = "Remote Control Server";
            ni_remote_control_server.BalloonTipText = "Version " + fvi.FileVersion;
            ni_remote_control_server.Text = "Remote Control Server\nVersion " + fvi.FileVersion;

            Control.CheckForIllegalCrossThreadCalls = false;

            myth = new Thread(new System.Threading.ThreadStart(our_Server));
            myth.Start();

            host = System.Net.Dns.GetHostName();
            ip = System.Net.Dns.GetHostByName(host).AddressList[0];
            dt = DateTime.Now;

            string str = dt + " " + host + " (" + ip.ToString() + ") version " + fvi.FileVersion + " on-line";
            sent_report(str);
        }

        void our_Server()
        {
            try
            {
                mytcpl = new TcpListener(23);
                mytcpl.Start();
                mysocket = mytcpl.AcceptSocket();
                myns = new NetworkStream(mysocket);
                byte[] buffer = new byte[1024];
                myns.Read(buffer, 0, 1024);
                MemoryStream ms = new MemoryStream(buffer);


                DateTime dt = DateTime.Now;

                string s = new StreamReader(ms).ReadToEnd();

                

                if (s.IndexOf("<Message>") == 0)
                {
                    s = s.Replace("<Message>","");
                    Match m_caption = Regex.Match(s, "<StartCaption>.*<EndCaption>", RegexOptions.Singleline);
                    Match m_text = Regex.Match(s, "<StartText>.*<EndText>", RegexOptions.Singleline);
                    string s_caption = m_caption.ToString().Replace("<StartCaption>", "").Replace("<EndCaption>", "");
                    string s_text = m_text.ToString().Replace("<StartText>", "").Replace("<EndText>", "");



                    string str = dt + " " + host + " (" + ip.ToString() + ") message (" + s_caption + ", " + s_text + ") sent ok";

                    sent_report(str);

                    MessageBox.Show(s_text, s_caption);

                }

                if (s.IndexOf("<Update>") == 0)
                {
                    string str = dt + " " + host + " (" + ip.ToString() + ") update ok";
                    sent_report(str);
                    Process process_run_program = new Process();
                    process_run_program.StartInfo.FileName = Application.StartupPath + @"\update.exe";
                    process_run_program.Start();
                    this.Close();
                }

                if (s.IndexOf("<PrimaryInternet>") == 0)
                {
                    SetGateway("192.168.8.234");
                    string str = dt + " " + host + " (" + ip.ToString() + ") Gateway Primary Internet Update";
                    sent_report(str);
                }

                if (s.IndexOf("<ReserveInternet>") == 0)
                {
                    SetGateway("192.168.8.230");
                    string str = dt + " " + host + " (" + ip.ToString() + ") Gateway Reserve Internet Update";
                    sent_report(str);
                }

                if (s.IndexOf("<Check>") == 0)
                {
                    string str = dt + " " + host + " (" + ip.ToString() + ") version " + fvi.FileVersion + " on-line";
                    sent_report(str);
                }

                if (s.IndexOf("<Screen>") == 0)
                {
                    bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                    // Create a graphics object from the bitmap

                    gfxScreenshot = Graphics.FromImage(bmpScreenshot);

                    // Take the screenshot from the upper left corner to the right bottom corner

                    gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

                    // Save the screenshot to the specified path that the user has chosen

                    bmpScreenshot.Save("screen\\" + dt.ToString("HHmmss_ddMMyyyy") + ".png", ImageFormat.Png);

                    // Show the form again

                    string str = dt + " " + host + " (" + ip.ToString() + ") screenshot ok";
                    sent_report(str);
                }

                mytcpl.Stop();

                if (mysocket.Connected == true)
                {
                    while (true)
                    {
                        our_Server();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public void sent_report(string s)
        {
            try
            {
                TcpClient myclient = new TcpClient("192.168.8.103", 32);
                NetworkStream myns_sent = myclient.GetStream();
                MemoryStream ms_sent = new MemoryStream();

                byte[] buffer_sent = System.Text.Encoding.UTF8.GetBytes(s);
                ms_sent.Write(buffer_sent, 0, buffer_sent.Length);

                myns_sent.Write(ms_sent.ToArray(), 0, ms_sent.ToArray().Length);
                myns_sent.Close();
                myclient.Close();

            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
            }
        }

        private void form_remote_control_server_FormClosing(object sender, FormClosingEventArgs e)
        {
            string str = dt + " " + host + " (" + ip.ToString() + ") off-line";
            sent_report(str);
            try
            {
                mytcpl.Stop();
                myth.Abort();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); 
            }
        }

        private void tsmi_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static void SetGateway(string gateway)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    try
                    {
                        ManagementBaseObject setGateway;
                        ManagementBaseObject newGateway =
                            objMO.GetMethodParameters("SetGateways");

                        newGateway["DefaultIPGateway"] = new string[] { gateway };
                        newGateway["GatewayCostMetric"] = new int[] { 1 };

                        setGateway = objMO.InvokeMethod("SetGateways", newGateway, null);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }

        private void tsmi_reserve_internet_Click(object sender, EventArgs e)
        {
            SetGateway("192.168.8.230");
        }

        private void tsmi_primary_internet_Click(object sender, EventArgs e)
        {
            SetGateway("192.168.8.234");
        }
    }
}
