using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Sumo.Ui.Helper
{
    public static class TcpServer

    {

        public static void StartServer()

        {
            Int32 port = 2002;

            IPAddress ip = IPAddress.Parse("127.0.0.1");

            TcpListener server = new TcpListener(ip, port);

            TcpClient clientSocket;
            int counter = 0;

            server.Start();

            counter = 0;

            var listeningThread = new Thread(start: () =>
            {
                while (true)
                {
                    counter += 1;
                    clientSocket = server.AcceptTcpClient();
                    HandleClient client = new HandleClient();
                    client.StartClient(clientSocket, Convert.ToString(counter));
                }
            })
            { IsBackground = true };

            listeningThread.Start();

        }

        

    }
}