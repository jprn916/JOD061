using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace par
{
    class Program
    {
        const int porta = 7000;

        const string ip = "10.0.2.255";

        static void Main(string[] args)
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
                UdpClient par = new UdpClient(porta);
                Console.WriteLine("Para sair, pressione CTRL+C.");

                while (true)
                {
                    byte[] bytesRecebidos = par.Receive(ref endPoint);
                    Console.WriteLine("Mensagem recebida: {0}", Encoding.ASCII.GetString(bytesRecebidos));
                }
            }
            catch (Exception)
            {
                UdpClient par = new UdpClient();
                par.EnableBroadcast = true;

                Console.WriteLine("Envie uma mensagem. Para sair, pressione ENTER.");
                Console.Write("> ");
                string msg = Console.ReadLine();

                while (msg.Length > 0)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(msg);
                    par.Send(bytes, bytes.Length, ip, porta);

                    Console.Write("> ");
                    msg = Console.ReadLine();
                }

                par.Close();
            }
        }
    }
}
