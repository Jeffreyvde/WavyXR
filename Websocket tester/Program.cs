/*
 * Author: Emiel van den Brink
 * 
 * Description:
 * Starts up a server using websocket-sharp websocket implementation.
 * The server echoes back any incoming messages to all other connected clients.
 * 
 * Requirements for clients:
 * -A wss connection using Tls1.2
 * 
 */

using System;
using System.Net;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;

namespace Websocket_tester
{
    public class Echo : WebSocketBehavior
    {
        protected override void OnOpen()
        {
            Console.WriteLine("Opened: " + ID);
            foreach (var id in Sessions.ActiveIDs)
            {
                Sessions.SendTo("{'heartBeat':144}", id);
            }
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = "You just said: " + e.Data;

            Console.WriteLine("My ID is: " + ID);

            foreach (var id in Sessions.ActiveIDs)
            {
                if (!string.Equals(ID, id))
                {
                    Console.WriteLine("sending message to: " + id);
                    Sessions.SendTo(msg, id);
                }
            }
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Console.WriteLine("Close was called");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("> Initializing server");

            uint port = 443;
            var server = new WebSocketServer((int)port, true);
            server.SslConfiguration.ServerCertificate = new X509Certificate2("/root/certificate.pfx", "2022MaMaProd");
            server.AddWebSocketService<Echo>("/");
            server.Start();

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    Console.WriteLine($"> Server available on wss://{ip}:{port}/");
                }
            }

            Console.ReadLine();
            server.Stop();
        }
    }
}
