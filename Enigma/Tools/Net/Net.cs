using System;

namespace Enigma
{
	public class Net
	{
		#region NetWall

		public static void NetWall()
		{
			firewall.process = 0;
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;
			checkCon();
			GC.Collect();
		}

		private static void checkCon()
		{
			firewall.activeConnections();
		}

		#endregion

		#region Check Port

		public static void checkPort()
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Hostname: ");
			Console.ForegroundColor = ConsoleColor.Green;
			string hostname = Console.ReadLine();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Port Number: ");
			Console.ForegroundColor = ConsoleColor.Green;
			string input = Console.ReadLine();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("");
			int portNo;

			if(int.TryParse(input, out portNo))
			{
				System.Net.IPAddress ipa = (System.Net.IPAddress) System.Net.Dns.GetHostAddresses(hostname)[0];

				try
				{
					System.Net.Sockets.Socket sock = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
					sock.Connect(ipa, portNo);
					if (sock.Connected == true)  // Port is in use and connection is successful
						Console.WriteLine("Port is Closed");
					sock.Close();
				}
				catch (System.Net.Sockets.SocketException ex)
				{
					if (ex.ErrorCode == 10061)  // Port is unused and could not establish connection 
						Console.WriteLine("Port is Open!");
					else
						Console.WriteLine(ex.Message);
				}
			}
			else
			{
				Console.WriteLine("Not an integer!");
			}

			Console.WriteLine(portNo);
		}

		#endregion

		#region Url Scan

		public static void UrlScanner()
		{
			UrlScan.urlPath();
		}

		#endregion
	}	
}

