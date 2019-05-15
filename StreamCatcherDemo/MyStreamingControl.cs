using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using QCAP.NET;

namespace StreamCatcherDemo
{
    public partial class MyStreamingControl : Form
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void OutputDebugString(string message);

        public MySetupControl m_pMainForm;

        public bool m_bSupportSoftwareEncoder = true;

        public bool m_bSupportHardwareEncoder = false;

        public bool m_bSupportIntelGPUEncoder = false;

        public uint[] m_hCapDev = new uint[4];

        public uint m_hRtspCapDev = 0;                                    // RTSP STREAM CAPTURE DEVICE

        public uint m_nVideoWidth = 1920;

        public uint m_nVideoHeight = 1080;

        public double m_dVideoFrameRate = 60.0;        

        // FOURCC MARCO
        //
        uint MAKEFOURCC(uint ch0, uint ch1, uint ch2, uint ch3)
        {
            return ((uint)(byte)(ch0) | ((uint)(byte)(ch1) << 8) | ((uint)(byte)(ch2) << 16) | ((uint)(byte)(ch3) << 24));
        }

        public MyStreamingControl()
        {
            InitializeComponent();
        }        

        private void m_btnStartStreaming_Click(object sender, EventArgs e)
        {
            m_bSupportSoftwareEncoder = m_checkSoftwareEncoder.Checked;

            m_bSupportHardwareEncoder = m_checkHardwareEncoder.Checked;

            m_bSupportIntelGPUEncoder = m_checkIntelGPUEncoder.Checked;            

            // CREATE RTSP SERVER
            //          
            String strAccount = "root";

            String strPassword = "root";

            EXPORTS.QCAP_CREATE_BROADCAST_RTSP_SERVER(0, 4, ref m_hRtspCapDev, ref strAccount, ref strPassword, 554);

            if ( m_bSupportSoftwareEncoder )
            {
                EXPORTS.QCAP_SET_VIDEO_BROADCAST_SERVER_PROPERTY(m_hRtspCapDev, 0, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, MAKEFOURCC('Y', 'V', '1', '2'), m_nVideoWidth / 2, m_nVideoHeight / 2, m_dVideoFrameRate, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 30, 0, 0, 0, 0, 0, (uint)(EXPORTS.BroadCastFlagEnum.QCAP_BROADCAST_FLAG_FULL));

                EXPORTS.QCAP_SET_VIDEO_BROADCAST_SERVER_PROPERTY(m_hRtspCapDev, 1, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, MAKEFOURCC('Y', 'V', '1', '2'), m_nVideoWidth / 2, m_nVideoHeight / 2, m_dVideoFrameRate, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 30, 0, 0, 0, 0, 0, (uint)(EXPORTS.BroadCastFlagEnum.QCAP_BROADCAST_FLAG_FULL));

                EXPORTS.QCAP_SET_VIDEO_BROADCAST_SERVER_PROPERTY(m_hRtspCapDev, 2, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, MAKEFOURCC('Y', 'V', '1', '2'), m_nVideoWidth / 2, m_nVideoHeight / 2, m_dVideoFrameRate, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 30, 0, 0, 0, 0, 0, (uint)(EXPORTS.BroadCastFlagEnum.QCAP_BROADCAST_FLAG_FULL));

                EXPORTS.QCAP_SET_VIDEO_BROADCAST_SERVER_PROPERTY(m_hRtspCapDev, 3, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, MAKEFOURCC('Y', 'V', '1', '2'), m_nVideoWidth / 2, m_nVideoHeight / 2, m_dVideoFrameRate, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 30, 0, 0, 0, 0, 0, (uint)(EXPORTS.BroadCastFlagEnum.QCAP_BROADCAST_FLAG_FULL));
            }

            if ( m_bSupportHardwareEncoder )
            {
                EXPORTS.QCAP_SET_VIDEO_BROADCAST_SERVER_PROPERTY(m_hRtspCapDev, 0, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_HARDWARE, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, MAKEFOURCC('Y', 'V', '1', '2'), m_nVideoWidth / 2, m_nVideoHeight / 2, m_dVideoFrameRate, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 30, 0, 0, 0, 0, 0, (uint)(EXPORTS.BroadCastFlagEnum.QCAP_BROADCAST_FLAG_FULL));

                EXPORTS.QCAP_SET_VIDEO_BROADCAST_SERVER_PROPERTY(m_hRtspCapDev, 1, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_HARDWARE, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, MAKEFOURCC('Y', 'V', '1', '2'), m_nVideoWidth / 2, m_nVideoHeight / 2, m_dVideoFrameRate, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 30, 0, 0, 0, 0, 0, (uint)(EXPORTS.BroadCastFlagEnum.QCAP_BROADCAST_FLAG_FULL));

                EXPORTS.QCAP_SET_VIDEO_BROADCAST_SERVER_PROPERTY(m_hRtspCapDev, 2, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_HARDWARE, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, MAKEFOURCC('Y', 'V', '1', '2'), m_nVideoWidth / 2, m_nVideoHeight / 2, m_dVideoFrameRate, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 30, 0, 0, 0, 0, 0, (uint)(EXPORTS.BroadCastFlagEnum.QCAP_BROADCAST_FLAG_FULL));

                EXPORTS.QCAP_SET_VIDEO_BROADCAST_SERVER_PROPERTY(m_hRtspCapDev, 3, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_HARDWARE, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, MAKEFOURCC('Y', 'V', '1', '2'), m_nVideoWidth / 2, m_nVideoHeight / 2, m_dVideoFrameRate, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 30, 0, 0, 0, 0, 0, (uint)(EXPORTS.BroadCastFlagEnum.QCAP_BROADCAST_FLAG_FULL));
            }

            if (m_bSupportIntelGPUEncoder)
            {
                EXPORTS.QCAP_SET_VIDEO_BROADCAST_SERVER_PROPERTY(m_hRtspCapDev, 0, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_INTEL_MEDIA_SDK, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, MAKEFOURCC('Y', 'V', '1', '2'), m_nVideoWidth / 2, m_nVideoHeight / 2, m_dVideoFrameRate, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 30, 0, 0, 0, 0, 0, (uint)(EXPORTS.BroadCastFlagEnum.QCAP_BROADCAST_FLAG_FULL));

                EXPORTS.QCAP_SET_VIDEO_BROADCAST_SERVER_PROPERTY(m_hRtspCapDev, 1, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_INTEL_MEDIA_SDK, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, MAKEFOURCC('Y', 'V', '1', '2'), m_nVideoWidth / 2, m_nVideoHeight / 2, m_dVideoFrameRate, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 30, 0, 0, 0, 0, 0, (uint)(EXPORTS.BroadCastFlagEnum.QCAP_BROADCAST_FLAG_FULL));

                EXPORTS.QCAP_SET_VIDEO_BROADCAST_SERVER_PROPERTY(m_hRtspCapDev, 2, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_INTEL_MEDIA_SDK, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, MAKEFOURCC('Y', 'V', '1', '2'), m_nVideoWidth / 2, m_nVideoHeight / 2, m_dVideoFrameRate, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 30, 0, 0, 0, 0, 0, (uint)(EXPORTS.BroadCastFlagEnum.QCAP_BROADCAST_FLAG_FULL));

                EXPORTS.QCAP_SET_VIDEO_BROADCAST_SERVER_PROPERTY(m_hRtspCapDev, 3, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_INTEL_MEDIA_SDK, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, MAKEFOURCC('Y', 'V', '1', '2'), m_nVideoWidth / 2, m_nVideoHeight / 2, m_dVideoFrameRate, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 30, 0, 0, 0, 0, 0, (uint)(EXPORTS.BroadCastFlagEnum.QCAP_BROADCAST_FLAG_FULL));
            }

            EXPORTS.QCAP_SET_AUDIO_BROADCAST_SERVER_PROPERTY(m_hRtspCapDev, 0, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.AudioEncoderFormatEnum.QCAP_ENCODER_FORMAT_PCM, 2, 16, 48000);

            EXPORTS.QCAP_SET_AUDIO_BROADCAST_SERVER_PROPERTY(m_hRtspCapDev, 1, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.AudioEncoderFormatEnum.QCAP_ENCODER_FORMAT_PCM, 2, 16, 48000);

            EXPORTS.QCAP_SET_AUDIO_BROADCAST_SERVER_PROPERTY(m_hRtspCapDev, 2, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.AudioEncoderFormatEnum.QCAP_ENCODER_FORMAT_PCM, 2, 16, 48000);

            EXPORTS.QCAP_SET_AUDIO_BROADCAST_SERVER_PROPERTY(m_hRtspCapDev, 3, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.AudioEncoderFormatEnum.QCAP_ENCODER_FORMAT_PCM, 2, 16, 48000);

            if (m_hRtspCapDev != 0)
            {
                m_pMainForm.m_pMainForm.m_bIsStreaming = true;

                m_pMainForm.m_pMainForm.m_hRtspCapDev = m_hRtspCapDev;
            }

            EXPORTS.QCAP_START_BROADCAST_SERVER( m_hRtspCapDev );

            //CRITICAL SECTION

          
          

            if (m_hRtspCapDev != 0)
            {
                // STOP RTSP STREAMING
                //            
                EXPORTS.QCAP_STOP_BROADCAST_SERVER(m_hRtspCapDev);

                EXPORTS.QCAP_DESTROY_BROADCAST_SERVER(m_hRtspCapDev);
            }

            m_btnStartStreaming.Enabled = true;

            m_btnStopStreaming.Enabled = false;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void MyStreamingControl_Load(object sender, EventArgs e)
        {
            m_btnStartStreaming.Enabled = true;

            m_btnStopStreaming.Enabled = false;
        }

        private void MyStreamingControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_pMainForm.m_pMainForm.m_bIsStreaming = false;

            m_pMainForm.m_pMainForm.m_hRtspCapDev = 0;

            if (m_hRtspCapDev != 0)
            {                
                // STOP RTSP STREAMING
                //            
                EXPORTS.QCAP_STOP_BROADCAST_SERVER(m_hRtspCapDev);

                EXPORTS.QCAP_DESTROY_BROADCAST_SERVER(m_hRtspCapDev);
            }
        }

        private void m_checkSoftwareEncoder_Click(object sender, EventArgs e)
        {
            m_checkSoftwareEncoder.Checked = true;

            m_checkHardwareEncoder.Checked = false;

            m_checkIntelGPUEncoder.Checked = false;
        }

        private void m_checkHardwareEncoder_Click(object sender, EventArgs e)
        {
            m_checkSoftwareEncoder.Checked = false;

            m_checkHardwareEncoder.Checked = true;

            m_checkIntelGPUEncoder.Checked = false;
        }

        private void m_checkIntelGPUEncoder_Click(object sender, EventArgs e)
        {
            m_checkSoftwareEncoder.Checked = false;

            m_checkHardwareEncoder.Checked = false;

            m_checkIntelGPUEncoder.Checked = true;
        }


    }
}
