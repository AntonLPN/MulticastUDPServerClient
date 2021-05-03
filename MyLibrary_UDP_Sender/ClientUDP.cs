using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;




namespace MyLibrary_UDP_Sender
{
    public class ClientUDP
    {


      

        public string Listener(string ip, int port)
        {

            while (true)
            {


                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 2);
                IPAddress multicastDest = IPAddress.Parse(ip);

                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(multicastDest, IPAddress.Any));
                IPEndPoint EP = new IPEndPoint(IPAddress.Any, port);
                socket.Bind(EP);
                byte[] buff = new byte[1024];
                while (true)
                {
                    int len = socket.Receive(buff);
                    string result = Encoding.Default.GetString(buff, 0, len);
                    socket.Close();
                    
                    return result;
                }
            }
        }

        

    }
}
