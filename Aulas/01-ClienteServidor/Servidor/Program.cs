using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace servidor
{
    class Program
    {

        const int porta = 7000;

        static void Main(string[] args)
        {
            TcpListener servidor = new TcpListener(IPAddress.Any, porta);

            Console.WriteLine("Servidor no ar!");
            servidor.Start();

            while (true)
            {
                TcpClient cliente = servidor.AcceptTcpClient();
                Console.WriteLine("Conexao estabelecida!");

                byte[] bytes = new byte[1024];
                NetworkStream stream = cliente.GetStream();
                
                while (true)
                {
                    int bytesRead = stream.Read(bytes, 0, bytes.Length);

                    if (bytesRead == 0) {
                        break;
                    }
                    
                    Console.WriteLine("Mensagem recebida: {0}", Encoding.ASCII.GetString(bytes, 0, bytesRead));
                }
                
                stream.Close();
                cliente.Close();
                Console.WriteLine("Conexao encerrada!");
            }

            // servidor.Stop();
        }
    }
}