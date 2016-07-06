using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Enigma
{
	public class About
	{
		// Define Global Version Strings
		public static string def = "016";
		public static string ed = "01";
		public static string dura = "02";

		public static string ver = (def+ "." +  ed + "." + dura);

		public static void Version()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;

			// Write Version Function to Console
			Console.WriteLine("Enigma's Current State: [{0}]", ver);
			Console.WriteLine("Version State: {0}/755", def);
			Console.WriteLine("Milestones: {0}/15", ed);
			Console.WriteLine("Algorithms: {0}/15", dura);
		}

		public static void about()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("-*- -=[Project Enigma " + ver +"]=- -*-");
			Console.WriteLine(" *");
			Console.WriteLine(" * [E]ncrypted [N]euraL [I]ntelligence [G]enerated [M]achine [A]pplication");
			Console.WriteLine(" *");
			Console.WriteLine(" * [De] Version State and Progress will be defined by 'Def' a max of 775");
			Console.WriteLine(" * [ED] Milestones or breakthroughs will be defined by 'ED' a max of 15.");
			Console.WriteLine(" * [Du] Invention of new crypto & AI algorithms defined by 'Dura' a max of 15.");
			Console.WriteLine(" * [Te] Tele represents the main project itself which if true is +Tele");
			Console.WriteLine("");
			Console.WriteLine("[Change Log]");
			Console.WriteLine("[Te]+1 - Engima's Creation [Nov 3, 2015]");
			Console.WriteLine("[De]001- Add: Console & Commands Structure");
			Console.WriteLine("[De]002- Add: Password Analyzer");
			Console.WriteLine("[De]003- Add: File Text Reader");
			Console.WriteLine("[De]004- Add: kTm Crypto Engine");
			Console.WriteLine("[De]005- Update: kTm Algo 4 Modes 'ECB, CBC, CFB, OFB'");
			Console.WriteLine("[Du]01 - Algorithm: kTm Cryptograph Algo");
			Console.WriteLine("[De]006- Update: kTm Algo Refined blockCipher and secured globals");
			Console.WriteLine("[De]007- Update: kTm Algo Refined blockCipherEnDc and secured globals");
			Console.WriteLine("[De]008- Update: Crypto Architecture Completed");
			Console.WriteLine("[ED]01 - Milestone: Fully Functional Crypto Engine, **NO DEPENDENCIES**");
			Console.WriteLine("[De]009- Add: NetWall shows active connections");
			Console.WriteLine("[De]010- Add: Port Check shows open ports");
			Console.WriteLine("[De]011- Update: Bug Fixes, Method Loops, Spelling and Polished Tools");
			Console.WriteLine("[De]012- Add: String to Byte or Hex Reversing");
			Console.WriteLine("[De]013- Update: String Reversing, Included Int and Binary");
			Console.WriteLine("[De]014- Add: Url Scanner checks for a list of paths");
			Console.WriteLine("[De]015- Add: Polynomial Encryption and Decryption");
			Console.WriteLine("[Du]02 - Algorithm: Polynomial Crypto Algo");
			Console.WriteLine("[De]016- Update: Correct Error checking and handling for all tools");
		}
	}
}