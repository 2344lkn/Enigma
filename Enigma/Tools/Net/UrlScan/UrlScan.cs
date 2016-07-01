using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Net;

namespace Enigma
{
	public class UrlScan
	{
		static string targetURL;
		public static void urlPath()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;
			string path = "paths.txt";
			string[] list = null;
			targetURL = string.Empty;

			Console.WriteLine("-=[Path Scan]=-");
			Console.WriteLine("");
			ServicePointManager.Expect100Continue = false;

			try
			{
				Console.WriteLine("Loading Paths from 'paths.txt'");

				if (File.Exists(path))
				{
					list = File.ReadAllLines(path);
					Console.WriteLine("Successfully loaded {0} paths.", list.Length);
				}
				else
				{
					Console.WriteLine("Path list could not be located, enter file name and .ext: ");
					path = Console.ReadLine();
					if (File.Exists(path))
					{
						list = File.ReadAllLines(path);
						Console.WriteLine("Successfully loaded {0} paths.", list.Length);
						//Remove '/' from the beginning of dorks.
						for (int i = 0; i < list.Length; i++)
						{
							if (list[i][0] == '/')
							{
								list[i] = list[i].Substring(1);
							}
						}
					}
					else
					{
						Console.WriteLine("The file you entered was invalid.");
					}
				}
				Console.Write("Target URL (terminated by /): ");
				Console.ForegroundColor = ConsoleColor.Green;
				targetURL = Console.ReadLine();
				Console.ForegroundColor = ConsoleColor.Red;
				Directory.CreateDirectory(targetURL.ToString().Replace("http://", "").Replace("/", ""));
				//Console.Write("Threads: ");
				//string threadVal = Console.ReadLine();
				//startScan(dorkList,targetURL);
				List<Uri> finalList = new List<Uri>();

				for (int i = 0; i < list.Length; i++)
				{
					finalList.Add(new Uri(targetURL + list[i]));
				}
				startScan(finalList);
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Magenta;
				Console.WriteLine("An error occured~ " + ex.Message);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.ReadKey();
			}
		}

		static void startScan(List<Uri> finalList)
		{
			List<string> goodList = new List<string>();
			List<string> badList = new List<string>();

			Parallel.ForEach(finalList, curURL =>
			{
				try
				{
					WebRequest newReq = HttpWebRequest.Create(curURL);
					HttpWebResponse newRes = newReq.GetResponse() as HttpWebResponse;
					string myResp;
					using (StreamReader sReader = new StreamReader(newRes.GetResponseStream()))
					{
						myResp = sReader.ReadToEnd();
					}
					//StreamReader sReader = new StreamReader(newRes.GetResponseStream());
					//myResp = sReader.ReadToEnd();
					if (!myResp.Contains("404"))
					{
						Console.ForegroundColor = ConsoleColor.Cyan;
						Console.WriteLine("Valid URL @~ " + curURL);
						Console.ForegroundColor = ConsoleColor.Red;
						goodList.Add(curURL.ToString());
					}
					else
					{
						Console.WriteLine("404 occured.");
						badList.Add(curURL.ToString());
					}
				}
				catch
				{
					Console.WriteLine("Invalid @~ " + curURL);
					badList.Add(curURL.ToString());
				}

			});

			Console.ForegroundColor = ConsoleColor.Yellow;
			File.WriteAllLines(targetURL.ToString().Replace("http://", "").Replace("/", "") + @"/Good.txt", goodList);
			File.WriteAllLines(targetURL.ToString().Replace("http://", "").Replace("/", "") + @"/Bad.txt", badList);
			Console.WriteLine("Done scanning the URLs - Results saved in folder.");
		}
	}
}

