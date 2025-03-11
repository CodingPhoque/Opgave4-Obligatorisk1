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
                    try
                    {
                        // 1) Læs kommando
                        string command = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(command))
                        {
                            Console.WriteLine("Forbindelsen lukkes: Ingen kommando modtaget.");
                            break;
                        }

                        Console.WriteLine($"Modtaget kommando: {command}");
                        writer.WriteLine("Input numbers");

                        // 3) Læs 2 tal
                        string numbersLine = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(numbersLine))
                        {
                            Console.WriteLine("Forbindelsen lukkes: Ingen tal modtaget.");
                            break;
                        }

                        Console.WriteLine($"Modtaget tal: {numbersLine}");
                        string[] numbers = numbersLine.Split();

                        if (numbers.Length < 2 || !int.TryParse(numbers[0], out int n1) || !int.TryParse(numbers[1], out int n2))
                        {
                            writer.WriteLine("Error: Invalid numbers");
                            Console.WriteLine("Fejl: Modtaget ugyldige tal");
                            continue;
                        }

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
                                writer.WriteLine("Error: Unknown command");
                                Console.WriteLine("Fejl: Ukendt kommando");
                                continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Fejl opstod: {ex.Message}");
                        writer.WriteLine("Error: Server encountered an issue.");
                        break; // Luk forbindelsen ved en uventet fejl
                    }
                }
            }
        }

    }
}
