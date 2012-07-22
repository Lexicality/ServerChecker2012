using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace ServerChecker2012
{
    public class SourceQuery
    {
        UdpClient sock;
        IPEndPoint target;
        Stopwatch timer;
        public SourceQuery(string ip, ushort port = 27015)
        {
            if (ip == null)
                throw new ArgumentNullException("ip");
            timer = new Stopwatch();
            target = new IPEndPoint(IPAddress.Parse(ip), port);
            sock = new UdpClient();
            sock.Client.ReceiveTimeout = 1000;
        }

        public SourceQuery(ServerData data) : this(data.IPAddress, data.Port)
        {}

        public void UpdateIP(IPAddress ip)
        {
            if (ip == null)
                throw new ArgumentNullException("ip");
            target.Address = ip;
        }

        public void UpdatePort(ushort port)
        {
            target.Port = port;
        }

        private static byte[] query = { 0xFF, 0xFF, 0xFF, 0xFF, 0x54, 0x53, 0x6F, 0x75, 0x72, 0x63, 0x65, 0x20, 0x45, 0x6E, 0x67, 0x69, 0x6E, 0x65, 0x20, 0x51, 0x75, 0x65, 0x72, 0x79, 0x00 };
        public long Ping()
        {
            timer.Restart();
            sock.Send(query, query.Length, target);
            byte[] rec;
            try
            {
                rec = sock.Receive(ref target);
            }
            catch (SocketException e)
            {
                if (e.SocketErrorCode == SocketError.TimedOut)
                    return -1;
                else
                    throw;
            }
            timer.Stop();
            if (rec[4] == 0x49)
            {
                return timer.ElapsedMilliseconds;
            } else {
                return -1;
            }
        }
    }
}