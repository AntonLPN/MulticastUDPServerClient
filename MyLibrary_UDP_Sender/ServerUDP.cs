using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MyLibrary_UDP_Sender
{
    public class ServerUDP
    {
        public ServerUDP()
        {

        }
        /// <summary>
        /// Метод отправки всем клиентам сообщения с сервера.Принимает аргументы адресс, порт, сообщени
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="message"></param>
        public void SendMessages(string ip,int port, string message)
        {


          

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                //Если установить её в
                //значение 1, то пакет не выйдет за пределы локальной сети. Если
                //же установить её в значение отличное от 1, то дейтаграмма
                //будет проходить через несколько роутеров.

                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 2);

                //Следующим действием мы создаём объект класса IPAddress,
                //описывающий некоторый, выбранный нами, multicast - адрес.
                //Регистрируем этот адрес, для созданного нами сокета
                //посредством вызова метода SetSocketOption, создаём на базе
                //этого адреса конечную точку соединения и соединяем наш
                //сокет с этой конечной точкой
                IPAddress multDestenation = IPAddress.Parse(ip);
                //IPAddress multDestenation = IPAddress.Parse("224.5.5.5") ;
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(multDestenation));
                IPEndPoint endPoint = new IPEndPoint(multDestenation, port);
                socket.Connect(endPoint);

                //проверка на пустоту строки
                if (message != " ")
                {
                    socket.Send(Encoding.Default.GetBytes(message));
                    socket.Close();
                }
            
         
        }

    }
}
