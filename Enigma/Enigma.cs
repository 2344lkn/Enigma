/*
 * -=[Project Enigma]=-
 * 
 * [E]ncrypted [N]euraL [I]ntelligence [G]enerated [M]achine [A]pplication
 * 
 * Artificial Intelligence & Cryptography Analysis and Algorithm Generation
 * when combining all of the above in a way that is difficult for us to
 * understand you get Project Enigma. So lets use bad programming practices
 * and do things outside of the box off the beaten path of the standards.
 * 
 * [De] Version State and Progress will be defined by 'Def' a max of 775
 * [ED] Milestones or breakthroughs will be defined by 'ED' a max of 15.
 * [Du] Invention of new cryptographic algorithms and AI defined by 'Dura' a max of 15.
 * [Te] Tele represents the main project itself which if true is +Tele
 * 
 * +775 Def
 * 15. ED
 * 15. Dura
 * +Tele
 * Goal^
 */ 

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Enigma
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("-=[Enigma " + About.def + "]=-");

			String command;
			Boolean quitNow = false;

			while(!quitNow)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("");
				Console.Write("#:");
				command = Console.ReadLine();

				switch (command)
				{
					#region Core Commands

					// Help Cmd
				case "h":
				case "help":
					// Core
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("-[Commands]-");
					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.Write("'h', 'help' ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("- Displays a list of available commands.");

					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.Write("'about' ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("- Displays Enigma's detailed information.");

					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.Write("'clear' ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("- Clears the console.");

					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.Write("'exit' ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("- Exits the program.");

					Console.WriteLine("");

					// Text
					Console.WriteLine("-[Text Commands]-");
					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.Write("'apass' ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("- Password Analyzer - Test Password Strength");

					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.Write("'txtr' ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("- Reads text of a file");

					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.Write("'strb' ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("- Convert a String to a Byte");

					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.Write("'strh' ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("- Convert a String to a HEX");

					Console.WriteLine("");

					// Crypto
					Console.WriteLine("-[Crypto Commands]-");
					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.Write("'kenc', 'ktme' ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("- Encrypt a file using kTm Crypto");

					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.Write("'kdec', 'ktmd' ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("- Decrypt a file using kTm Crypto");

					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.Write("'penc', 'polye' ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("- Encrypt Text using Poly Crypto");

					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.Write("'pdec', 'polyd' ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("- Decrypt Text using Poly Crypto");

					Console.WriteLine("");


					// Net
					Console.WriteLine("-[Net Commands]-");
					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.Write("'nwall', 'netwall' ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("- NetWall - Shows active connections");

					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.Write("'port' ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("- Port Check - Shows open ports");

					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.Write("'url' ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("- Url Scan - Checks url for a list of paths");

					Console.WriteLine("");
					break;

					// About Cmd
				case "about":
					About.about();
					break;

					// Version Cmd
				case "ver":
				case "version":
					About.Version();
					break;

					// Clear Console Cmd
				case "c":
				case "clear":
					Console.Clear();
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("-=[Enigma " + About.def + "]=-");
					break;

					// Exit Enigma Cmd
				case "exit":
				case "quit":
					Environment.Exit(0);
					break;

					#endregion

					#region Text Commands

					// Password Analyzer Cmd
				case "apass":
					Text.PassAnalyzer();
					break;

					// Text File Reader Cmd
				case "txtr":
					Text.TextFileReader();
					break;

					// String Convert to Byte Cmd
				case "strb":
					Text.StringToByte();
					break;

					// String Convert to Hex Cmd
				case "strh":
					Text.StringToHEX();
					break;

					#endregion

					#region Crypto Commands

					// kTm Encrypt Cmd
				case "ktme":
				case "kenc":
					Crypto.kTmEncrypt();
					break;

					// kTm Decrypt Cmd
				case "ktmd":
				case "kdec":
					Crypto.kTmDecrypt();
					break;

					// Poly Encrypt Text Cmd
				case "penc":
				case "polye":
					Crypto.PolyEncrypt();
					break;

					// Poly Decrypt Text Cmd
				case "pdec":
				case "polyd":
					Crypto.PolyDecrypt();
					break;

					#endregion

					#region Net Commands

					// NetWall Cmd
				case "netwall":
				case "nwall":
					Net.NetWall();
					break;

					// Port Check Cmd
				case "port":
					Net.checkPort();
					break;

					// Url Scan Cmd
				case "url":
					Net.UrlScanner();
					break;

					#endregion

				default:
					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.WriteLine("'" + command + "' unknown command! Type: 'help' for a list of commands.");
					break;
				}
			}
		}
	}
}