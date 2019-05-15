using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using IMemory;
using StreamCatcherDemo;


using QCAP.NET;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;

namespace StreamCatcherDemo
{

    public partial class Form1 : Form

    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void OutputDebugString(string message);

        // public static uint n = 4;

        static string conn_str = Mydata.conn_str;
        public Form1()
        {
            InitializeComponent();
           
        }


        //定义摄像头个数
        public static uint n = Mydata.getcameranum();

        public uint i = 0;
        
        public bool []b_bitmap = new bool[n];

        unsafe public byte* pDstABGRBuffer;


        public bool m_bIsMaximizedForm = false;

        public bool[] m_bIsMaximizedChannelWindow = new bool[n];

        public bool[] m_bNoSignal = new bool[n];

        public string[] m_strFormatChangedOutput = new string[n];

        public bool[] m_bShareRecordCH = new bool[n];

        public bool m_bShowClone = false;

        public bool m_bIsShareRecord = false;

        public uint m_nVideoWidth = 704;

        public uint m_nVideoHeight = 480;

        public double m_dVideoFrameRate = 30.0;

        public bool m_bIsStreaming = false;

        public bool m_bSupportSoftwareEncoder = true;

        public bool m_bSupportHardwareEncoder = false;

        public bool m_bSupportIntelGPUEncoder = false;

        public uint m_hRtspCapDev = 0;                                    // RTSP STREAM CAPTURE DEVICE

        // CRITICAL SECTION OBJECT
        // 
        volatile public uint[] m_nNetworkServerState_v = new uint[n];

        public object[] b_bitmapchose = new object[n];

        //public object b_bitmapchose1 = new object();
        //public object b_bitmapchose2 = new object();
        //public object b_bitmapchose3 = new object();
        //public object b_bitmapchose4 = new object();

        // FOURCC MARCO
        //
        uint MAKEFOURCC(uint ch0, uint ch1, uint ch2, uint ch3)
        {
            return ((uint)(byte)(ch0) | ((uint)(byte)(ch1) << 8) | ((uint)(byte)(ch2) << 16) | ((uint)(byte)(ch3) << 24));
        }

        // CALLBACK FUNCTION
        //        
        public static EXPORTS.PF_FORMAT_CHANGED_CALLBACK m_pFormatChangedCB;

        public static EXPORTS.PF_NO_SIGNAL_DETECTED_CALLBACK m_pNoSignalDetectedCB;

        public static EXPORTS.PF_SIGNAL_REMOVED_CALLBACK m_pSignalRemovedCB;

        public static EXPORTS.PF_VIDEO_HARDWARE_ENCODER_CALLBACK m_pVideoHardwareEncoderCallback;

        public static EXPORTS.PF_VIDEO_PREVIEW_CALLBACK m_pPreviewVideoCB;

        public MySetupControl m_cSetupControl = new MySetupControl();

        // LIVE PREVIEW CHANNEL WINDOW
        //
        public MyChannelControl[] m_pChannelControl_LIVE = new MyChannelControl[n];

        string m_strChipName = "DC1150 USB";

        // DEVICE PROPERTY
        //
        public uint[] m_hCapDev = new uint[n];                         // STREAM CAPTURE DEVICE


        //  FORMAT CHANGED CALLBACK FUNCTION
        //
        EXPORTS.ReturnOfCallbackEnum on_process_format_changed(uint pDevice, uint nVideoInput, uint nAudioInput, uint nVideoWidth, uint nVideoHeight, uint bVideoIsInterleaved, double dVideoFrameRate, uint nAudioChannels, uint nAudioBitsPerSample, uint nAudioSampleFrequency, uint pUserData)
        {
            uint nCH = pUserData;

            // OUTPUT FORMAT CHANGED MESSAGE
            //
            string strOutput = "CH" + (nCH + 1).ToString() + " -> FORMAT CHANGED : pDevice : " + pDevice.ToString() + " , " + "nVideoInput : " + nVideoInput.ToString() + " , " +

                                        "nAudioInput : " + nAudioInput.ToString() + " , " + "nVideoWidth : " + nVideoWidth.ToString() + " , " +

                                        "nVideoHeight : " + nVideoHeight.ToString() + " , " + "bVideoIsInterleaved : " + bVideoIsInterleaved.ToString() + " , " +

                                        "dVideoFrameRate : " + dVideoFrameRate.ToString() + " , " + "nAudioChannels : " + nAudioChannels.ToString() + " , " +

                                        "nAudioBitsPerSample : " + nAudioBitsPerSample.ToString() + " , " + "nAudioSampleFrequency : " + nAudioSampleFrequency.ToString() + " , " +

                                        "pUserData : " + pUserData.ToString() + " \n";

            OutputDebugString(strOutput);

            m_nVideoWidth = nVideoWidth;

            m_nVideoHeight = nVideoHeight;

            m_dVideoFrameRate = dVideoFrameRate;

            uint nVH = 0;

            string strFrameType = " P ";

            string strVideoInput = "", strAudioInput = "";

            if (nVideoInput == 0) { strVideoInput = "COMPOSITE"; } if (nVideoInput == 1) { strVideoInput = "SVIDEO"; } if (nVideoInput == 2) { strVideoInput = "HDMI"; }

            if (nVideoInput == 3) { strVideoInput = "DVI_D"; } if (nVideoInput == 4) { strVideoInput = "COMPONENTS (YCBCR)"; } if (nVideoInput == 5) { strVideoInput = "DVI_A (RGB / VGA)"; }

            if (nVideoInput == 6) { strVideoInput = "SDI"; } if (nVideoInput == 7) { strVideoInput = "AUTO"; }

            if (nAudioInput == 0) { strAudioInput = "EMBEDDED_AUDIO"; } if (nAudioInput == 1) { strAudioInput = "LINE_IN"; }

            if (bVideoIsInterleaved == 1) { nVH = nVideoHeight / 2; } else { nVH = nVideoHeight; }

            if (bVideoIsInterleaved == 1) { strFrameType = " I "; } else { strFrameType = " P "; }

            m_strFormatChangedOutput[nCH] = @" INFO : " + nVideoWidth.ToString() + " x " + nVH.ToString() + strFrameType + " @" + dVideoFrameRate.ToString() +

                " FPS , " + nAudioChannels.ToString() + " CH x " + nAudioBitsPerSample.ToString() + " BITS x " + nAudioSampleFrequency.ToString() + " HZ , " +

                " VIDEO INPUT : " + strVideoInput + " , " + " AUDIO INPUT : " + strAudioInput + " \n";

            // NO SIGNAL
            //       
            if (nVideoWidth == 0 && nVideoHeight == 0 && dVideoFrameRate == 0.0 && nAudioChannels == 0 && nAudioBitsPerSample == 0 && nAudioSampleFrequency == 0)
            {
                m_bNoSignal[nCH] = true;
            }
            else
            {
                m_bNoSignal[nCH] = false;
            }

            return EXPORTS.ReturnOfCallbackEnum.QCAP_RT_OK;
        }

        // PREVIEW VIDEO CALLBACK FUNCTION
        //
        //回调函数，进行视频流操作
        EXPORTS.ReturnOfCallbackEnum on_process_preview_video_buffer(uint pDevice, double dSampleTime, uint pFrameBuffer, uint nFrameBufferLen, uint pUserData)
        {
            uint nCH = pUserData;

            if (b_bitmap[nCH] == true && pFrameBuffer != 0)
            {
                b_bitmap[nCH] = false;

                unsafe
                {
                    pDstABGRBuffer = (byte*)memory.Alloc((int)(m_nVideoWidth * m_nVideoHeight * 4));

                    EXPORTS.QCAP_COLORSPACE_YUY2_TO_ABGR32(pFrameBuffer, m_nVideoWidth, m_nVideoHeight, m_nVideoWidth * 2, (uint)pDstABGRBuffer, m_nVideoWidth, m_nVideoHeight, m_nVideoWidth * 4);

                    Bitmap bmp = new Bitmap((int)m_nVideoWidth, (int)m_nVideoHeight, PixelFormat.Format32bppRgb);

                    BitmapData bmpData = bmp.LockBits(new Rectangle(0,
                                                                    0,
                                                                    bmp.Width,
                                                                    bmp.Height),
                                                                    ImageLockMode.WriteOnly,
                                                                    bmp.PixelFormat);

                    byte[] managedArray = new byte[(m_nVideoWidth * m_nVideoHeight * 4)];

                    Marshal.Copy((IntPtr)pDstABGRBuffer, managedArray, 0, (int)(m_nVideoWidth * m_nVideoHeight * 4));

                    Marshal.Copy(managedArray, 0, bmpData.Scan0, (int)(m_nVideoWidth * m_nVideoHeight * 4));

                    bmp.UnlockBits(bmpData);

                    switch (nCH)
                    {
                        case 0:

                                {
                                    PicTosql1(bmp, nCH);

                                }
                           
                            break;
                        case 1:
                           
                                {
                                    PicTosql(bmp, nCH);

                                }
                            
                            break;
                        case 2:
                           
                                {
                                    PicTosql(bmp, nCH);

                                }
                           
                            break;
                        case 3:
                           
                                if (m_nNetworkServerState_v[nCH] > 0x00000000)
                                {
                                    PicTosql(bmp, nCH);

                                }
                          
                            break;
                        default:
                            break;
                    }

                }
                //string strOutput = "CH" + (nCH + 1).ToString() +  " : on_process_preview_video_buffer => pDevice : " + pDevice.ToString() + " , dSampleTime : " + dSampleTime.ToString() + " , pFrameBuffer : " + pFrameBuffer.ToString() + " , nFrameBufferLen : " + nFrameBufferLen.ToString() + " , pUserData : " + pUserData.ToString() + " \n";

                //OutputDebugString(strOutput);
}
                return EXPORTS.ReturnOfCallbackEnum.QCAP_RT_OK;
            
        }
        //位图转成字节流
        public static byte[] BitmapByte(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }
        //保存到本机
        private void GetPic(Bitmap bitmap)
        {

            string path = "F:\\camera\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bmp";

            bitmap.Save(path);

        }

        //将图片插入数据库
        public void PicTosql(Bitmap bitmap,uint i)
        {
            string constr = conn_str;
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();

            SqlCommand cmd = new SqlCommand("insert into Table_"+(i+1).ToString()+"(id,photo) values(@id,@photo)", conn);
            //  string s = DateTime.Now.ToString("MMddHHmmss");
            //  int i = int.Parse(s);

            String s = GetTimestamp(DateTime.Now);
            int k= int.Parse(s);
            byte[] P = BitmapByte(bitmap);
            cmd.Parameters.Add("id", SqlDbType.Int).Value = k;
            cmd.Parameters.Add("photo", SqlDbType.Image).Value = P;
            cmd.ExecuteNonQuery();

        }

        public void PicTosql1(Bitmap bitmap, uint i)
        {
            string constr = conn_str;
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();

            SqlCommand cmd = new SqlCommand("insert into Table_" + (i + 1).ToString() + "(time,photo) values(@time,@photo)", conn);
            //  string s = DateTime.Now.ToString("MMddHHmmss");
            //  int i = int.Parse(s);

            //String s = GetTimestamp(DateTime.Now);
            //int k = int.Parse(s);
            String nowTime = DateTime.Now.ToString();
            byte[] P = BitmapByte(bitmap);
            cmd.Parameters.Add("time", SqlDbType.DateTime).Value = nowTime;
            cmd.Parameters.Add("photo", SqlDbType.Image).Value = P;
            cmd.ExecuteNonQuery();

        }



        public static string GetTimestamp(DateTime d)
        {
            TimeSpan ts = d - new DateTime(2019, 4, 25, 8, 0, 0);
            return Convert.ToInt32(ts.TotalSeconds).ToString();     //精确到秒
        }

        public void createSql(uint cameranum)
        {
            string constr = conn_str;
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();

            for (int i = 1; i <= cameranum; i++)
            {
                //注意单引号与双引号
                string sqlstr = "if not exists (select * from sysobjects where id = OBJECT_ID(N'Table_" +  i.ToString() + "')) CREATE TABLE Table_" + i.ToString() + "(time datetime not null,photo image)";

                SqlCommand cmd = new SqlCommand(sqlstr, conn);
                //  string s = DateTime.Now.ToString("MMddHHmmss");
                //  int i = int.Parse(s);

                cmd.ExecuteNonQuery();

            }
        }


        

        // NO SIGNAL DETEACTED CALLBACK FUNCTION
        //
        EXPORTS.ReturnOfCallbackEnum on_process_no_signal_detected(uint pDevice, uint nVideoInput, uint nAudioInput, uint pUserData)
        {
            uint nCH = pUserData;

            OutputDebugString("CH" + (nCH + 1).ToString() + " No Signal Detected  \n");

            m_bNoSignal[nCH] = true;

            return EXPORTS.ReturnOfCallbackEnum.QCAP_RT_OK;
        }

        // SIGNAL REMOVED CALLBACK FUNCTION
        //
        EXPORTS.ReturnOfCallbackEnum on_process_signal_removed(uint pDevice, uint nVideoInput, uint nAudioInput, uint pUserData)
        {
            uint nCH = pUserData;

            OutputDebugString("CH" + (nCH + 1).ToString() + " Signal Removed \n");

            m_bNoSignal[nCH] = true;

            return EXPORTS.ReturnOfCallbackEnum.QCAP_RT_OK;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // CREATE CHANNEL WINDOW
            //
            for (i = 0; i < n; i++)
            {
                m_pChannelControl_LIVE[i] = new MyChannelControl();

                m_pChannelControl_LIVE[i].Parent = this;

                // LEFT POSITION
                //
                if (i == 0) { m_pChannelControl_LIVE[i].Left = 0; }

                if (i == 1) { m_pChannelControl_LIVE[i].Left = this.Width / 2; }

                if (i == 2) { m_pChannelControl_LIVE[i].Left = 0; }

                if (i == 3) { m_pChannelControl_LIVE[i].Left = this.Width / 2; }

                // TOP POSITION
                //
                if (i == 0) { m_pChannelControl_LIVE[i].Top = 0; }

                if (i == 1) { m_pChannelControl_LIVE[i].Top = 0; }

                if (i == 2) { m_pChannelControl_LIVE[i].Top = this.Height / 2; }

                if (i == 3) { m_pChannelControl_LIVE[i].Top = this.Height / 2; }

                // WIDTH & HEIGHT
                //
                m_pChannelControl_LIVE[i].Size = new System.Drawing.Size(this.Width / 2, this.Height / 2);

                m_pChannelControl_LIVE[i].Visible = true;

                m_pChannelControl_LIVE[i].m_nChannelNumber = i + 1;

                m_bIsMaximizedChannelWindow[i] = false;

                m_nNetworkServerState_v[i] = 0x00000000;

               
            }

           
            HwInitialize();

            // USER INTERFACE PROGRAMMING (SETUP CONTROL)
            //
            m_cSetupControl = new MySetupControl();

            m_cSetupControl.m_pMainForm = this;

            m_cSetupControl.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SetupControlClosed);

            m_cSetupControl.Left = this.Left;

            m_cSetupControl.Top = this.Bottom - 20;

            m_cSetupControl.Visible = true;

            m_cSetupControl.Show();

            for (i = 0; i < n; i++)
            {
                m_bShareRecordCH[i] = true;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            timerCheckSignal.Enabled = false;            
            
            EXPORTS.QCAP_STOP_SHARE_RECORD(0);
            
            HwUnInitialize();
        }

        private void SetupControlClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        //初始化
        public bool HwInitialize()
        {
            
            for (i = 0; i < n; i++) { m_hCapDev[i] = 0x00000000; }

            for (i = 0; i < n; i++) { m_bNoSignal[i] = true; }

            for (i = 0; i < n; i++) { m_strFormatChangedOutput[i] = ""; }

            // CREATE CAPTURE DEVICE            
            //
            for (i = 0; i < n; i++ )
            {
                string str_chip_name = m_strChipName;

                EXPORTS.QCAP_CREATE(ref str_chip_name, i, (uint)m_pChannelControl_LIVE[i].Handle.ToInt32(), ref m_hCapDev[i], 1);
            }

            // REGISTER FORMAT CHANGED CALLBACK FUNCTION
            // 
            m_pFormatChangedCB = new EXPORTS.PF_FORMAT_CHANGED_CALLBACK(on_process_format_changed);
            //修改后
            for (i = 0; i < n; i++) 
            {
                EXPORTS.QCAP_REGISTER_FORMAT_CHANGED_CALLBACK(m_hCapDev[i], m_pFormatChangedCB, i);
            }

            // REGISTER PREVIEW VIDEO CALLBACK FUNCTION
            // 
            m_pPreviewVideoCB = new EXPORTS.PF_VIDEO_PREVIEW_CALLBACK(on_process_preview_video_buffer);

            for (i = 0; i < n; i++)
            {

                EXPORTS.QCAP_REGISTER_VIDEO_PREVIEW_CALLBACK(m_hCapDev[i], m_pPreviewVideoCB, i);

            }


            // REGISTER NO SIGNAL DETECTED CALLBACK FUNCTION
            //
            m_pNoSignalDetectedCB = new EXPORTS.PF_NO_SIGNAL_DETECTED_CALLBACK(on_process_no_signal_detected);

            for (i = 0; i < n; i++)
            {

                EXPORTS.QCAP_REGISTER_NO_SIGNAL_DETECTED_CALLBACK(m_hCapDev[i], m_pNoSignalDetectedCB, i);

            }

            // REGISTER SIGNAL REMOVED CALLBACK FUNCTION
            //
            m_pSignalRemovedCB = new EXPORTS.PF_SIGNAL_REMOVED_CALLBACK(on_process_signal_removed);

            for (i = 0; i < n; i++)
            {
                EXPORTS.QCAP_REGISTER_SIGNAL_REMOVED_CALLBACK(m_hCapDev[i], m_pSignalRemovedCB, i);

            }

            // SET INPUT
            //
            for (i = 0; i < n; i++)
            {
                EXPORTS.QCAP_SET_VIDEO_INPUT(m_hCapDev[i], 0x00);
            }          

            // RUN DEVICE
            //
            for (i = 0; i < n; i++)
            {
                EXPORTS.QCAP_SET_VIDEO_DEINTERLACE(m_hCapDev[i], 0);
            }

            for (i = 0; i < n; i++)
            {

                EXPORTS.QCAP_SET_VIDEO_STANDARD(m_hCapDev[i], 0x10);
            }

            for (i = 0; i < n; i++)
            {
                EXPORTS.QCAP_RUN(m_hCapDev[i]);

            }


            for (i = 0; i < n; i++)
            {
                b_bitmap[i] = false;
            }

            for (i = 0; i < n; i++)
            {
                b_bitmapchose[i] = new object();
            }
           
            createSql(n);

            timerCheckSignal.Enabled = true;

            return true;



        }

        public bool HwUnInitialize()
        {
            m_bIsStreaming = false;

            for (i = 0; i < n; i++)
            {

                if (m_hCapDev[0] != 0) { EXPORTS.QCAP_STOP(m_hCapDev[i]); EXPORTS.QCAP_DESTROY(m_hCapDev[i]); }

            }

            for (i = 0; i < n; i++)
            {
                b_bitmap[i] = false;
            }
            unsafe
            {
                memory.free(pDstABGRBuffer);
            }

            

            EXPORTS.QCAP_DESTROY_BROADCAST_SERVER(m_hRtspCapDev);

            return true;
        }

        
        public void OnLButtonDown_ChannelControl(uint nChannelNumber)
        {
            if (m_bIsMaximizedChannelWindow[nChannelNumber - 1])
            {
                m_bIsMaximizedChannelWindow[nChannelNumber - 1] = false;

                for (i = 0; i < n; i++)
                {
                    // LEFT POSITION
                    //
                    if (i == 0) { m_pChannelControl_LIVE[i].Left = 0; }

                    if (i == 1) { m_pChannelControl_LIVE[i].Left = this.Width / 2; }

                    if (i == 2) { m_pChannelControl_LIVE[i].Left = 0; }

                    if (i == 3) { m_pChannelControl_LIVE[i].Left = this.Width / 2; }

                    // TOP POSITION
                    //
                    if (i == 0) { m_pChannelControl_LIVE[i].Top = 0; }

                    if (i == 1) { m_pChannelControl_LIVE[i].Top = 0; }

                    if (i == 2) { m_pChannelControl_LIVE[i].Top = this.Height / 2; }

                    if (i == 3) { m_pChannelControl_LIVE[i].Top = this.Height / 2; }

                    m_pChannelControl_LIVE[i].Size = new System.Drawing.Size(this.Width / 2, this.Height / 2);

                    m_pChannelControl_LIVE[i].Visible = true;
                }

               
            }
            else
            {
                m_bIsMaximizedChannelWindow[nChannelNumber - 1] = true;

                for (i = 0; i < n; i++) { m_pChannelControl_LIVE[i].Visible = false; }

                CloneChannelPanel1.Visible = false; CloneChannelPanel2.Visible = false; CloneChannelPanel3.Visible = false; CloneChannelPanel4.Visible = false;

                m_pChannelControl_LIVE[nChannelNumber - 1].Left = 0;

                m_pChannelControl_LIVE[nChannelNumber - 1].Top = 0;

                m_pChannelControl_LIVE[nChannelNumber - 1].Size = new System.Drawing.Size(this.Width, this.Height);

                m_pChannelControl_LIVE[nChannelNumber - 1].Visible = true;

               
            }
        }

        public void OnRButtonDown_ChannelControl(uint nChannelNumber)
        {
            // CHANGE CHANNEL WINDOWS SIZE AND POSITION
            //
            if (!m_bIsMaximizedForm)
            {
                this.WindowState = FormWindowState.Maximized;

                m_bIsMaximizedForm = true;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;

                m_bIsMaximizedForm = false;
            }

            if (m_bIsMaximizedChannelWindow[nChannelNumber - 1])
            {
                m_pChannelControl_LIVE[nChannelNumber - 1].Left = 0;

                m_pChannelControl_LIVE[nChannelNumber - 1].Top = 0;

                m_pChannelControl_LIVE[nChannelNumber - 1].Size = new System.Drawing.Size(this.Width, this.Height);

                m_pChannelControl_LIVE[nChannelNumber - 1].Visible = true;

                
            }
            else
            {
                for (i = 0; i < n; i++)
                {
                    // LEFT POSITION
                    //
                    if (i == 0) { m_pChannelControl_LIVE[i].Left = 0; }

                    if (i == 1) { m_pChannelControl_LIVE[i].Left = this.Width / 2; }

                    if (i == 2) { m_pChannelControl_LIVE[i].Left = 0; }

                    if (i == 3) { m_pChannelControl_LIVE[i].Left = this.Width / 2; }

                    // TOP POSITION
                    //
                    if (i == 0) { m_pChannelControl_LIVE[i].Top = 0; }

                    if (i == 1) { m_pChannelControl_LIVE[i].Top = 0; }

                    if (i == 2) { m_pChannelControl_LIVE[i].Top = this.Height / 2; }

                    if (i == 3) { m_pChannelControl_LIVE[i].Top = this.Height / 2; }

                    m_pChannelControl_LIVE[i].Size = new System.Drawing.Size(this.Width / 2, this.Height / 2);

                    m_pChannelControl_LIVE[i].Visible = true;
                }

                }
        }

        private void timerCheckSignal_Tick(object sender, EventArgs e)
        {
            // DISPLAY FORMAT CHANGED MESSAGE
            //
            m_cSetupControl.m_bNoSignal1 = m_bNoSignal[0];

            m_cSetupControl.m_bNoSignal2 = m_bNoSignal[1];

            m_cSetupControl.m_bNoSignal3 = m_bNoSignal[2];

            m_cSetupControl.m_bNoSignal4 = m_bNoSignal[3];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (i = 0; i < n; i++)
            {
                if (m_bNoSignal[i] == false)
                {
                    lock (b_bitmapchose[i]) { b_bitmap[i] = true; }
                }

            }

            //if (m_bNoSignal[0] == false)
            //{
            //    lock (b_bitmapchose[0]) { b_bitmap[0] = true; }
            //}
            //if (m_bNoSignal[1] == false)
            //{
            //    lock (b_bitmapchose[1]) { b_bitmap[1] = true; }
            //}
               
            }

        private void timer2_Tick(object sender, EventArgs e)
        {
            string constr = @"Data Source=USER-PC;Initial Catalog=mytest;User ID=sa;Pwd=1234";
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();

            SqlCommand cmd = new SqlCommand("insert into Table_2(time,distance) values(@time,@distance)", conn);
            //  string s = DateTime.Now.ToString("MMddHHmmss");
            //  int i = int.Parse(s);

            //String s = GetTimestamp(DateTime.Now);
            //int k = int.Parse(s);
            String nowTime = DateTime.Now.ToString();

            int p = new Random().Next(5, 10);
            cmd.Parameters.Add("time", SqlDbType.DateTime).Value = nowTime;
            cmd.Parameters.Add("distance", SqlDbType.Int).Value = p;
            cmd.ExecuteNonQuery();
        }
    }
    }

    

