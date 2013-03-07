using System;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace TCPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Connect("127.0.0.1", "GET Quinlan,Jeremy\r\n");
            Console.Read();
        }
        static void Connect(String serverName, String msg)
        {
            int DNSPort = 21000;
            int QuotePort = 21001;
            String rspIP = String.Empty;
            
            try
            {
                TcpClient myClient = new TcpClient(serverName, DNSPort);
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);
                NetworkStream strm = myClient.GetStream();
                strm.Write(data, 0, data.Length);
                Console.WriteLine("Sent: {0}", msg);

                data = new Byte[256];
                String rspData = String.Empty;
                Int32 bytes = strm.Read(data, 0, data.Length);
                rspData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                rspIP = rspData.Substring(rspData.IndexOf("\r") - 9, 9);
                Console.WriteLine("Received: {0}", rspData);
                strm.Close();
                myClient.Close();
            } 
            catch (SocketException e)
            {
                Console.WriteLine("SocketExecption: {0}", e);
            }

            for (int i = 0; i < 3; i++)
            {
                Random rand = new Random();
                msg = "GET " + rand.Next(1,20) + "\r\n";


                try
                {
                    TcpClient myClient = new TcpClient(rspIP, QuotePort);
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);
                    NetworkStream strm = myClient.GetStream();
                    strm.Write(data, 0, data.Length);
                    Console.WriteLine("Sent: {0}", msg);

                    data = new Byte[256];
                    String rspData = String.Empty;
                    Int32 bytes = strm.Read(data, 0, data.Length);
                    rspData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Received: {0}", rspData);
                    strm.Close();
                    myClient.Close();
                }
                catch (SocketException e)
                {
                    Console.WriteLine("SocketExecption: {0}", e);
                }
            }

            }
            
        }
    
}
