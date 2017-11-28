using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace SOAP_client.Helpers
{
    public static class IpAddress
    {


        public static IPAddress GetIpAddress()
        {
            // Getting Ip address of local machine...
            // First get the host name of local machine.
            var strHostName = Dns.GetHostName();
            Console.WriteLine("Local Machine's Host Name: " + strHostName);
            // Then using host name, get the IP address list..
            var ipEntry = Dns.GetHostEntry(strHostName);
            var addr = ipEntry.AddressList;
            for (var i = 0; i < addr.Length; i++)
            {
                Console.WriteLine("IP Address {0}: {1} ", i, addr[i]);
            }
            Console.WriteLine(Environment.NewLine);
            return addr.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);


        }

    }
}
