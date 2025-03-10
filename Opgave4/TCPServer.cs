using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Opgave4
{
    class TCPServer
    {
    public static void HandleClient(TcpClient client)
        {
            using (client)
            {
                NetworkStream ns = client.GetStream();
                StreamReader reader = new StreamReader(ns);
                StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };
                Random rnd = new Random();

                while (true)
                {
                    // 1) Læs kommando
                    string command = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(command)) break;

                    // 2) Svar med "Input numbers"
                    writer.WriteLine("Input numbers");

                    // 3) Læs 2 tal adskilt af mellemrum
                    string numbersLine = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(numbersLine)) break;
                    string[] numbers = numbersLine.Split();

                    int n1 = int.Parse(numbers[0]);
                    int n2 = int.Parse(numbers[1]);

                    // 4) Udfør handling baseret på kommandoen, og skriv resultat
                    switch (command)
                    {
                        case "Random":
                            writer.WriteLine(rnd.Next(n1, n2 + 1));
                            break;
                        case "Add":
                            writer.WriteLine(n1 + n2);
                            break;
                        case "Subtract":
                            writer.WriteLine(n1 - n2);
                            break;
                        default:
                            // Ukendt kommando, luk forbindelsen
                            return;
                    }
                }
            }
        }
    }
}
