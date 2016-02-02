namespace Boxi.ASPX
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.Hosting;
    using System.Windows.Forms;

    internal class Request : SimpleWorkerRequest
    {
        private string _allRawHeaders;
        private Connection _conn;
        private int _contentLength;
        private int _endHeadersOffset;
        private string _filePath;
        private byte[] _headerBytes;
        private ArrayList _headerByteStrings;
        private bool _headersSent;
        private Host _host;
        private string[] _knownRequestHeaders;
        private string _path;
        private string _pathInfo;
        private string _pathTranslated;
        private byte[] _preloadedContent;
        private int _preloadedContentLength;
        private string _prot;
        private string _queryString;
        private byte[] _queryStringBytes;
        private ArrayList _responseBodyBytes;
        private StringBuilder _responseHeadersBuilder;
        private int _responseStatus;
        private bool _specialCaseStaticFileHeaders;
        private int _startHeadersOffset;
        private string[][] _unknownRequestHeaders;
        private string _url;
        private string _verb;
        private const int maxHeaderBytes = 0x8000;
        private static char[] s_badPathChars = new char[] { '%', '>', '<', '$', ':' };
        private static string[] s_defaultFilenames = new string[] { "index.aspx", "index.html", "index.htm", "default.aspx", "default.htm", "default.html" };

        public Request(Host host, Connection conn) : base(string.Empty, string.Empty, null)
        {
            this._host = host;
            this._conn = conn;
        }

        public override void CloseConnection()
        {
            this._conn.Close();
        }

        public override void EndOfRequest()
        {
        }

        public override void FlushResponse(bool finalFlush)
        {
            if (!this._headersSent)
            {
                this._conn.WriteHeaders(this._responseStatus, this._responseHeadersBuilder.ToString());
                this._headersSent = true;
            }
            for (int i = 0; i < this._responseBodyBytes.Count; i++)
            {
                byte[] data = (byte[]) this._responseBodyBytes[i];
                this._conn.WriteBody(data, 0, data.Length);
            }
            this._responseBodyBytes = new ArrayList();
            if (finalFlush)
            {
                this._conn.Close();
            }
        }

        public override string GetAppPath()
        {
            return this._host.VirtualPath;
        }

        public override string GetAppPathTranslated()
        {
            return this._host.PhysicalPath;
        }

        public override string GetFilePath()
        {
            return this._filePath;
        }

        public override string GetFilePathTranslated()
        {
            return this._pathTranslated;
        }

        public override string GetHttpVerbName()
        {
            return this._verb;
        }

        public override string GetHttpVersion()
        {
            return this._prot;
        }

        public override string GetKnownRequestHeader(int index)
        {
            return this._knownRequestHeaders[index];
        }

        public override string GetLocalAddress()
        {
            return this._conn.LocalIP;
        }

        public override int GetLocalPort()
        {
            return this._host.Port;
        }

        public override string GetPathInfo()
        {
            return this._pathInfo;
        }

        public override byte[] GetPreloadedEntityBody()
        {
            return this._preloadedContent;
        }

        public override string GetQueryString()
        {
            return this._queryString;
        }

        public override byte[] GetQueryStringRawBytes()
        {
            return this._queryStringBytes;
        }

        public override string GetRawUrl()
        {
            return this._url;
        }

        public override string GetRemoteAddress()
        {
            return this._conn.RemoteIP;
        }

        public override int GetRemotePort()
        {
            return 0;
        }

        public override string GetServerVariable(string name)
        {
            string str = string.Empty;
            string str2 = name;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "ALL_RAW"))
            {
                if (str2 != "SERVER_PROTOCOL")
                {
                    if (str2 != "SERVER_SOFTWARE")
                    {
                        return str;
                    }
                    return ("Microsoft-Cassini/" + Messages.VersionString);
                }
            }
            else
            {
                return this._allRawHeaders;
            }
            return this._prot;
        }

        public override string GetUnknownRequestHeader(string name)
        {
            int length = this._unknownRequestHeaders.Length;
            for (int i = 0; i < length; i++)
            {
                if (string.Compare(name, this._unknownRequestHeaders[i][0], true, CultureInfo.InvariantCulture) == 0)
                {
                    return this._unknownRequestHeaders[i][1];
                }
            }
            return null;
        }

        public override string[][] GetUnknownRequestHeaders()
        {
            return this._unknownRequestHeaders;
        }

        public override string GetUriPath()
        {
            return this._path;
        }

        public override bool HeadersSent()
        {
            return this._headersSent;
        }

        private bool IsBadPath()
        {
            return ((this._path == null) || ((this._path.IndexOfAny(s_badPathChars) >= 0) || (this._path.IndexOf("..") >= 0)));
        }

        public override bool IsClientConnected()
        {
            return this._conn.Connected;
        }

        public override bool IsEntireEntityBodyIsPreloaded()
        {
            return (this._contentLength == this._preloadedContentLength);
        }

        public override string MapPath(string path)
        {
            string physicalPath = string.Empty;
            if (((path == null) || (path.Length == 0)) || path.Equals("/"))
            {
                if (this._host.VirtualPath == "/")
                {
                    physicalPath = this._host.PhysicalPath;
                }
                else
                {
                    physicalPath = Environment.SystemDirectory;
                }
            }
            else if (this._host.IsVirtualPathAppPath(path))
            {
                physicalPath = this._host.PhysicalPath;
            }
            else if (this._host.IsVirtualPathInApp(path))
            {
                physicalPath = this._host.PhysicalPath + path.Substring(this._host.NormalizedVirtualPath.Length);
            }
            else if (path.StartsWith("/"))
            {
                physicalPath = this._host.PhysicalPath + path.Substring(1);
            }
            else
            {
                physicalPath = this._host.PhysicalPath + path;
            }
            physicalPath = physicalPath.Replace('/', '\\');
            if (physicalPath.EndsWith(@"\") && !physicalPath.EndsWith(@":\"))
            {
                physicalPath = physicalPath.Substring(0, physicalPath.Length - 1);
            }
            return physicalPath;
        }

        private void ParseHeaders()
        {
            this._knownRequestHeaders = new string[40];
            ArrayList list = new ArrayList();
            for (int i = 1; i < this._headerByteStrings.Count; i++)
            {
                string str = ((ByteString) this._headerByteStrings[i]).GetString();
                int index = str.IndexOf(':');
                if (index >= 0)
                {
                    string header = str.Substring(0, index).Trim();
                    string str3 = str.Substring(index + 1).Trim();
                    int knownRequestHeaderIndex = HttpWorkerRequest.GetKnownRequestHeaderIndex(header);
                    if (knownRequestHeaderIndex >= 0)
                    {
                        this._knownRequestHeaders[knownRequestHeaderIndex] = str3;
                    }
                    else
                    {
                        list.Add(header);
                        list.Add(str3);
                    }
                }
            }
            int num4 = list.Count / 2;
            this._unknownRequestHeaders = new string[num4][];
            int num5 = 0;
            for (int j = 0; j < num4; j++)
            {
                this._unknownRequestHeaders[j] = new string[] { (string) list[num5++], (string) list[num5++] };
            }
            if (this._headerByteStrings.Count > 1)
            {
                this._allRawHeaders = Encoding.UTF8.GetString(this._headerBytes, this._startHeadersOffset, this._endHeadersOffset - this._startHeadersOffset);
            }
            else
            {
                this._allRawHeaders = string.Empty;
            }
        }

        private void ParsePostedContent()
        {
            this._contentLength = 0;
            this._preloadedContentLength = 0;
            string s = this._knownRequestHeaders[11];
            if (s != null)
            {
                try
                {
                    this._contentLength = int.Parse(s);
                }
                catch
                {
                }
            }
            if (this._headerBytes.Length > this._endHeadersOffset)
            {
                this._preloadedContentLength = this._headerBytes.Length - this._endHeadersOffset;
                if ((this._preloadedContentLength > this._contentLength) && (this._contentLength > 0))
                {
                    this._preloadedContentLength = this._contentLength;
                }
                this._preloadedContent = new byte[this._preloadedContentLength];
                Buffer.BlockCopy(this._headerBytes, this._endHeadersOffset, this._preloadedContent, 0, this._preloadedContentLength);
            }
        }

        private void ParseRequestLine()
        {
            ByteString[] strArray = ((ByteString) this._headerByteStrings[0]).Split(' ');
            if (((strArray != null) && (strArray.Length >= 2)) && (strArray.Length <= 3))
            {
                this._verb = strArray[0].GetString();
                ByteString str2 = strArray[1];
                this._url = str2.GetString();
                if (strArray.Length == 3)
                {
                    this._prot = strArray[2].GetString();
                }
                else
                {
                    this._prot = "HTTP/1.0";
                }
                int index = str2.IndexOf('?');
                if (index > 0)
                {
                    this._queryStringBytes = str2.Substring(index + 1).GetBytes();
                }
                else
                {
                    this._queryStringBytes = new byte[0];
                }
                index = this._url.IndexOf('?');
                if (index > 0)
                {
                    this._path = this._url.Substring(0, index);
                    this._queryString = this._url.Substring(index + 1);
                }
                else
                {
                    this._path = this._url;
                    this._queryStringBytes = new byte[0];
                }
                if (this._path.IndexOf('%') >= 0)
                {
                    this._path = HttpUtility.UrlDecode(this._path);
                }
                int startIndex = this._path.LastIndexOf('.');
                int num3 = this._path.LastIndexOf('/');
                if (((startIndex >= 0) && (num3 >= 0)) && ((startIndex < num3) && (num3 != (this._path.Length - 1))))
                {
                    int length = this._path.IndexOf('/', startIndex);
                    this._filePath = this._path.Substring(0, length);
                    this._pathInfo = this._path.Substring(length);
                }
                else
                {
                    this._filePath = this._path;
                    this._pathInfo = string.Empty;
                }
                this._pathTranslated = this.MapPath(this._filePath);
            }
        }

        private void PrepareResponse()
        {
            this._headersSent = false;
            this._responseStatus = 200;
            this._responseHeadersBuilder = new StringBuilder();
            this._responseBodyBytes = new ArrayList();
        }

        public void Process()
        {
            this.ReadAllHeaders();
            if (((this._headerBytes == null) || (this._endHeadersOffset < 0)) || ((this._headerByteStrings == null) || (this._headerByteStrings.Count == 0)))
            {
                this._conn.WriteErrorAndClose(400);
            }
            else
            {
                this.ParseRequestLine();
                if (this.IsBadPath())
                {
                    this._conn.WriteErrorAndClose(400);
                }
                else
                {
                    bool flag = false;
                    string str = null;
                    try
                    {
                        this.ParseHeaders();
                        this.ParsePostedContent();
                        if (((this._verb == "POST") && (this._contentLength > 0)) && (this._preloadedContentLength < this._contentLength))
                        {
                            this._conn.Write100Continue();
                        }
                        if (flag)
                        {
                            this._conn.WriteEntireResponseFromFile(this._host.PhysicalClientScriptPath + str, false);
                        }
                        else if (!this.ProcessDirectoryListingRequest())
                        {
                            this.PrepareResponse();
                            HttpRuntime.ProcessRequest(this);
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
            }
        }

        private bool ProcessDirectoryListingRequest()
        {
            if (this._verb != "GET")
            {
                return false;
            }
            if (!Directory.Exists(this._pathTranslated))
            {
                return false;
            }
            if (!this._path.EndsWith("/"))
            {
                string str = this._path + "/";
                string extraHeaders = "Location: " + str + "\r\n";
                string body = "<html><head><title>Object moved</title></head><body>\r\n<h2>Object moved to <a href='" + str + "'>here</a>.</h2>\r\n</body></html>\r\n";
                this._conn.WriteEntireResponseFromString(0x12e, extraHeaders, body, false);
                return true;
            }
            foreach (string str4 in s_defaultFilenames)
            {
                string str5 = this._pathTranslated + @"\" + str4;
                if (File.Exists(str5))
                {
                    this._path = this._path + str4;
                    this._filePath = this._path;
                    this._url = (this._queryString != null) ? (this._path + "?" + this._queryString) : this._path;
                    this._pathTranslated = str5;
                    return false;
                }
            }
            FileSystemInfo[] elements = null;
            try
            {
                elements = new DirectoryInfo(this._pathTranslated).GetFileSystemInfos();
            }
            catch
            {
            }
            string path = null;
            if (this._path.Length > 1)
            {
                int length = this._path.LastIndexOf('/', this._path.Length - 2);
                path = (length > 0) ? this._path.Substring(0, length) : "/";
                if (!this._host.IsVirtualPathInApp(path))
                {
                    path = null;
                }
            }
            this._conn.WriteEntireResponseFromString(200, "Content-type: text/html; charset=utf-8\r\n", Messages.FormatDirectoryListing(this._path, path, elements), false);
            return true;
        }

        private void ReadAllHeaders()
        {
            this._headerBytes = null;
            do
            {
                if (!this.TryReadAllHeaders())
                {
                    return;
                }
            }
            while (this._endHeadersOffset < 0);
        }

        public override int ReadEntityBody(byte[] buffer, int size)
        {
            int count = 0;
            byte[] src = this._conn.ReadRequestBytes(size);
            if ((src != null) && (src.Length > 0))
            {
                count = src.Length;
                Buffer.BlockCopy(src, 0, buffer, 0, count);
            }
            return count;
        }

        public override void SendCalculatedContentLength(int contentLength)
        {
            if (!this._headersSent)
            {
                this._responseHeadersBuilder.Append("Content-Length: ");
                this._responseHeadersBuilder.Append(contentLength.ToString());
                this._responseHeadersBuilder.Append("\r\n");
            }
        }

        public override void SendKnownResponseHeader(int index, string value)
        {
            if (!this._headersSent)
            {
                switch (index)
                {
                    case 1:
                    case 2:
                    case 0x1a:
                        return;

                    case 0x12:
                    case 0x13:
                        if (!this._specialCaseStaticFileHeaders)
                        {
                            break;
                        }
                        return;

                    case 20:
                        if (!(value == "bytes"))
                        {
                            break;
                        }
                        this._specialCaseStaticFileHeaders = true;
                        return;
                }
                this._responseHeadersBuilder.Append(HttpWorkerRequest.GetKnownResponseHeaderName(index));
                this._responseHeadersBuilder.Append(": ");
                this._responseHeadersBuilder.Append(value);
                this._responseHeadersBuilder.Append("\r\n");
            }
        }

        public override void SendResponseFromFile(IntPtr handle, long offset, long length)
        {
            if (length != 0L)
            {
                FileStream f = null;
                try
                {
                    f = new FileStream(handle, FileAccess.Read, false);
                    this.SendResponseFromFileStream(f, offset, length);
                }
                finally
                {
                    if (f != null)
                    {
                        f.Close();
                    }
                }
            }
        }

        public override void SendResponseFromFile(string filename, long offset, long length)
        {
            if (length != 0L)
            {
                FileStream f = null;
                try
                {
                    f = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                    this.SendResponseFromFileStream(f, offset, length);
                }
                finally
                {
                    if (f != null)
                    {
                        f.Close();
                    }
                }
            }
        }

        private void SendResponseFromFileStream(FileStream f, long offset, long length)
        {
            long num = f.Length;
            if (length == -1L)
            {
                length = num - offset;
            }
            if (((length != 0L) && (offset >= 0L)) && (length <= (num - offset)))
            {
                if (offset > 0L)
                {
                    f.Seek(offset, SeekOrigin.Begin);
                }
                if (length <= 0x10000L)
                {
                    byte[] buffer = new byte[(int) length];
                    int num2 = f.Read(buffer, 0, (int) length);
                    this.SendResponseFromMemory(buffer, num2);
                }
                else
                {
                    byte[] buffer2 = new byte[0x10000];
                    int num3 = (int) length;
                    while (num3 > 0)
                    {
                        int count = (num3 < 0x10000) ? num3 : 0x10000;
                        int num5 = f.Read(buffer2, 0, count);
                        this.SendResponseFromMemory(buffer2, num5);
                        num3 -= num5;
                        if ((num3 > 0) && (num5 > 0))
                        {
                            this.FlushResponse(false);
                        }
                    }
                }
            }
        }

        public override void SendResponseFromMemory(byte[] data, int length)
        {
            if (length > 0)
            {
                byte[] dst = new byte[length];
                Buffer.BlockCopy(data, 0, dst, 0, length);
                this._responseBodyBytes.Add(dst);
            }
        }

        public override void SendStatus(int statusCode, string statusDescription)
        {
            this._responseStatus = statusCode;
        }

        public override void SendUnknownResponseHeader(string name, string value)
        {
            if (!this._headersSent)
            {
                this._responseHeadersBuilder.Append(name);
                this._responseHeadersBuilder.Append(": ");
                this._responseHeadersBuilder.Append(value);
                this._responseHeadersBuilder.Append("\r\n");
            }
        }

        private bool TryReadAllHeaders()
        {
            byte[] src = this._conn.ReadRequestBytes(0x8000);
            if ((src == null) || (src.Length == 0))
            {
                return false;
            }
            if (this._headerBytes != null)
            {
                int num = src.Length + this._headerBytes.Length;
                if (num > 0x8000)
                {
                    return false;
                }
                byte[] dst = new byte[num];
                Buffer.BlockCopy(this._headerBytes, 0, dst, 0, this._headerBytes.Length);
                Buffer.BlockCopy(src, 0, dst, this._headerBytes.Length, src.Length);
                this._headerBytes = dst;
            }
            else
            {
                this._headerBytes = src;
            }
            this._startHeadersOffset = -1;
            this._endHeadersOffset = -1;
            this._headerByteStrings = new ArrayList();
            ByteParser parser = new ByteParser(this._headerBytes);
            while (true)
            {
                ByteString str = parser.ReadLine();
                if (str == null)
                {
                    break;
                }
                if (this._startHeadersOffset < 0)
                {
                    this._startHeadersOffset = parser.CurrentOffset;
                }
                if (str.IsEmpty)
                {
                    this._endHeadersOffset = parser.CurrentOffset;
                    break;
                }
                this._headerByteStrings.Add(str);
            }
            return true;
        }
    }
}

