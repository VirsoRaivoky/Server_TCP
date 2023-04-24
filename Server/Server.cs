using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    private const int port = 11000;

    private static void StartListener()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, port);
        try
        {
            listener.Start();
            Console.WriteLine($"Servidor iniciado na porta {port}");
            while (true)
            {
                Console.WriteLine("Esperando conexão do cliente");
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine($"Conexão estabelecida com {client.Client.RemoteEndPoint}");

                NetworkStream stream = client.GetStream();

                byte[] bytes = new byte[256];
                int bytesRead = stream.Read(bytes, 0, bytes.Length);

                var message = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                int num = int.Parse(message);
                double Sqrt = Math.Sqrt(num);

                byte[] response = Encoding.ASCII.GetBytes($"Aqui está a raiz quadrada do seu número: {Sqrt}\n");
                stream.Write(response, 0, response.Length);
                Console.WriteLine($"Resposta enviada para {client.Client.RemoteEndPoint}");

                client.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ocorreu um erro: {e}");
        }
        finally
        {
            listener.Stop();
        }
    }

    public static void Main()
    {
        StartListener();
    }
}
