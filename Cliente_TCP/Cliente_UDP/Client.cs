using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main(string[] args)
    {
        try
        {
            TcpClient client = new TcpClient("192.168.3.15", 11000);
            Console.WriteLine("Digite um número inteiro...");
            var message = Console.ReadLine();
            int sqr = int.Parse(message);

            while (message != string.Empty)
            {
                NetworkStream stream = client.GetStream();
                byte[] sendbuf = Encoding.ASCII.GetBytes(message);

                stream.Write(sendbuf, 0, sendbuf.Length);

                byte[] bytes = new byte[256];
                int bytesRead = stream.Read(bytes, 0, bytes.Length);
                var response = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                Console.WriteLine($"Resposta do servidor: {response}");

                message = Console.ReadLine();
            }

            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ocorreu um erro: {e}");
        }
    }
}
