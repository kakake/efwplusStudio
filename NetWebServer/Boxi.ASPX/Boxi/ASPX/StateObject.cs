namespace Boxi.ASPX
{
    using System;
    using System.Net.Sockets;
    using System.Text;

    public class StateObject
    {
        public byte[] buffer = new byte[0x400];
        public const int BufferSize = 0x400;
        internal Host host;
        public StringBuilder sb = new StringBuilder();
        public Socket workSocket;
    }
}

