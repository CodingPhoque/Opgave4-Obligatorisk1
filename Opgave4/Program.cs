using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Opgave4
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start en TCP-lytter på port 42069
            TcpListener listener = new TcpListener(IPAddress.Any, 42069);
            listener.Start();
            Console.WriteLine("Server startet på port 42069");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Thread t = new Thread(() => TCPServer.HandleClient(client));
                t.Start();
            }
        }
    }
}

