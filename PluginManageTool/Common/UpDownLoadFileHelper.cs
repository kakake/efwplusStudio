using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.ComponentModel;

namespace PluginManageTool.Common
{
    public class UpDownLoadFileHelper
    {
        public delegate void controldelegate(string name, params object[] val);

        public enum updown
        {
            上传, 下载
        }

        //public Button btncancel;
        //public ProgressBar progressBar;
        //public Label lblTime;//已用时间
        //public Label lblSpeed;//速度
        //public Label lblState;//百分比
        //public Label lblSize;//完成进度

        DateTime startTime = DateTime.Now; //开始时间
        WebClient webClient = new WebClient();


        //private bool iscancel = false;//取消上传


        public updown UpDown { get; set; }
        public controldelegate Cdelegate { get; set; }
        public Action Completed{get;set;}
        public Action Cancelled { get; set; }

        public void Start(string serverurl, string localfile)
        {
            if (UpDown == updown.上传)
                Upload_webClient(serverurl, localfile);
            else if (UpDown == updown.下载)
                Download_webClient(serverurl, localfile);
        }

        public void Stop()
        {
            if (UpDown == updown.上传)
                CancelUpload_webClient();
            else if (UpDown == updown.下载)
                CancelDownload_webClient();
        }


        private void Download_webClient(string serverfileurl, string localfile)
        {
            //btncancel.Enabled = true;
            if (webClient.IsBusy)//是否存在正在进行中的Web请求 
            {
                webClient.CancelAsync();
            }
            if (Cdelegate != null)
            {
                Cdelegate("progressBar_1");
                //为webClient添加事件 
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);
            }
            //开始下载 
            webClient.DownloadFileAsync(new Uri(serverfileurl), localfile);

        }

        private void CancelDownload_webClient()
        {
            webClient.CancelAsync();
            webClient.Dispose();
        }

        private void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Cdelegate("progressBar_2", e.ProgressPercentage);
            TimeSpan span = DateTime.Now - startTime;
            double second = span.TotalSeconds;
            Cdelegate("lblTime", "已用时：" + second.ToString("F2") + "秒");
            if (second > 0.001)
            {
                Cdelegate("lblSpeed"," 平均速度：" + (e.BytesReceived / 1024 / second).ToString("0.00") + "KB/秒");
            }
            else
            {
                Cdelegate("lblSpeed", " 正在连接…");
            }
           // Cdelegate("lblSpeed", "");
            Cdelegate("lblState", e.ProgressPercentage.ToString() + "%");
            Cdelegate("lblSize", string.Format("正在下载文件，完成进度{0}/{1}(字节)"
                                , e.BytesReceived
                                , e.TotalBytesToReceive));
        }

        private void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error!=null)
            {
                Cancelled();
                throw e.Error;
            }

            if (e.Cancelled)
            {
                Cdelegate("lblTime", "下载被取消！");
                Cancelled();
            }
            else
            {
                Cdelegate("lblTime", "下载完成！");
                Completed();
            }
            Cdelegate("btncancel");
        }

        private void Upload_webClient(string serverurl, string localfile)
        {
            try
            {
                if (webClient.IsBusy)
                {
                    webClient.CancelAsync();
                }
                if (Cdelegate != null)
                {
                    Cdelegate("progressBar_1");
                    webClient.UploadProgressChanged += new UploadProgressChangedEventHandler(webClient_UploadProgressChanged);
                    webClient.UploadFileCompleted += new UploadFileCompletedEventHandler(webClient_UploadFileCompleted);
                }
                webClient.UploadFileAsync(new Uri(serverurl), localfile);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private void CancelUpload_webClient()
        {
            webClient.CancelAsync();
            webClient.Dispose();
        }

        void webClient_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Cancelled();
                throw e.Error;
            }

            if (e.Cancelled)
            {
                Cdelegate("lblTime", "上传被取消！");
                Cancelled();
            }
            else
            {
                Cdelegate("lblTime", "上传完成！");
                Completed();
            }
            Cdelegate("btncancel");
        }

        void webClient_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            try
            {
                Cdelegate("progressBar_2", e.ProgressPercentage);
                TimeSpan span = DateTime.Now - startTime;
                double second = span.TotalSeconds;
                Cdelegate("lblTime", "已用时：" + second.ToString("F2") + "秒");
                if (second > 0.001)
                {
                    Cdelegate("lblSpeed", " 平均速度：" + (e.BytesSent / 1024 / second).ToString("0.00") + "KB/秒");
                }
                else
                {
                    Cdelegate("lblSpeed", " 正在连接…");
                }
                //Cdelegate("lblSpeed", "");
                Cdelegate("lblState", e.ProgressPercentage.ToString() + "%");
                Cdelegate("lblSize", string.Format("正在上传文件，完成进度{0}/{1}(字节)"
                                    , e.BytesSent
                                    , e.TotalBytesToSend));
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }


}
