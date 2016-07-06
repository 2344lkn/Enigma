using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// Remove after web request testing
using System.Net;
using System.IO;

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
					{
						Console.ForegroundColor = ConsoleColor.Magenta;
						Console.WriteLine(ex.Message);
						Console.ForegroundColor = ConsoleColor.Red;
					}
				}
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Magenta;
				Console.WriteLine("Not an integer!");
				Console.ForegroundColor = ConsoleColor.Red;
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

		// TODO: Test Web Requests
		#region Testing Web Requests

		public static void testWebReq()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("-=[Web Request]=-");
			Console.WriteLine("Type in the url you would like to request: ");
			Console.ForegroundColor = ConsoleColor.Green;
			string url = Console.ReadLine();
			Console.ForegroundColor = ConsoleColor.Red;

			try
			{
				// Create a request for the URL
				WebRequest request = WebRequest.Create (url);
				// If required by the server, set the credentials
				request.Credentials = CredentialCache.DefaultCredentials;
				// Get the response
				WebResponse response = request.GetResponse();
				// Display the status
				Console.WriteLine("");
				Console.WriteLine(((HttpWebResponse)response).StatusDescription);
				// Get the stream containing content returned by the server
				Stream dataStream = response.GetResponseStream();
				// Open the stream using a StreamReader for easy access
				StreamReader reader = new StreamReader(dataStream);
				// Read the content
				string responseFromServer = reader.ReadToEnd();
				// Display the content
				Console.WriteLine(responseFromServer);

				reader.Close();
				response.Close();
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Magenta;
				Console.WriteLine(e.Message);
				Console.ForegroundColor = ConsoleColor.Magenta;
			}
		}

		#endregion
	}	
}

