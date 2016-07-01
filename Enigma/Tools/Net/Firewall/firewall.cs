using System;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Enigma
{
	public class firewall
	{
		public static int process = 0;

		public static void activeConnections()
		{
			while (process < 1)
			{
				process++;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("-[nwall] Local <==> External-");
				Console.WriteLine("---- Active Connections -----");
				Console.WriteLine("-----------------------------");
				IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
				TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();

				foreach (TcpConnectionInformation c in connections)
				{
					Console.WriteLine("{0} <==> {1}",
					                  c.LocalEndPoint.ToString(),
					                  c.RemoteEndPoint.ToString());
				}
				Console.WriteLine("-----------------------------");
			}
		}
	}
}

