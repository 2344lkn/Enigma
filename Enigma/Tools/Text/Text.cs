using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Enigma
{
	public class Text
	{
		#region Password Analyzer

		public static void PassAnalyzer()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("-=[ Password Analyzer ]=-");
			Console.WriteLine("");
			Console.WriteLine("Type in a password to examine: ");
			Console.ForegroundColor = ConsoleColor.Green;
			string pwd = Console.ReadLine();
			Console.WriteLine("");
			Console.ForegroundColor = ConsoleColor.Red;

			// Count password
			int count = 0;
			foreach (char c in pwd)
				count++;
			Console.WriteLine("Characters: " + count);

			// Count special characters
			int sCount = 0;
			foreach (char c in pwd)
				if ( c == '/' || c == '!' || c == '@' || c == '#' || c == '$' || c == '%' || c == '^' || c == '&' || c == '*' ||
				    c == '(' || c == ')' || c == '-' || c == '_' || c == '+' || c == '=' || c == '{' || c == '}' || c == '[' ||
				    c == ']' || c == '?' || c == '|' || c == '.' || c == ',' || c == '<' || c == '>' || c == ';' || c == ':' || 
				    c == '"' || c == '`' || c == '~') 
					sCount++;
			Console.WriteLine("Special Chars:" + sCount);

			// Count upper case
			int uCount = 0;
			foreach (char c in pwd)
				if (c == 'A' || c == 'B' || c == 'C' || c == 'D' || c == 'E' || c == 'F' || c == 'G' || c == 'H' || c == 'I' ||
				    c == 'J' || c == 'K' || c == 'L' || c == 'M' || c == 'N' || c == 'O' || c == 'P' || c == 'Q' || c == 'R' ||
				    c == 'S' || c == 'T' || c == 'U' || c == 'V' || c == 'W' || c == 'X' || c == 'Y' || c == 'Z') 
					uCount++;
			Console.WriteLine("Upper Case: " + uCount);

			// Number count
			int nCount = 0;
			foreach (char c in pwd)
				if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' ||
				    c == '9')
					nCount++;
			Console.WriteLine("Numbers: " + nCount);

			// Reverse password
			char[] cArray = pwd.ToCharArray();
			string sPwd = String.Empty;
			for (int i = cArray.Length - 1; i > -1; i--)
			{
				sPwd += cArray[i];
			}
			Console.WriteLine("Reversed: " + sPwd);

			// Score
			// TODO - Refine the score to more accuratley score a password.
			int score = 0;
			if (count > 3)
				score++;
			if (count > 5)
				score++;
			if (count > 8)
				score++;
			if (count > 11)
				score++;
			if (sCount > 0)
				score++;
			if (sCount > 1)
				score++;
			if (uCount > 0)
				score++;
			if (uCount > 1)
				score++;
			if (nCount > 0)
				score++;
			if (nCount > 1)
				score++;
			if (count < 6)
				score--;
			if (count < 5)
				score--;
			Console.WriteLine("\n*[Score: " + score + "/10]*");
		}

		#endregion

		#region Text File Reader

		public static void TextFileReader()
		{
			int counter = 0;
			string line;

			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Type in a file path..");
			Console.WriteLine("Example: /home/user/test.txt");
			Console.ForegroundColor = ConsoleColor.Green;
			string fPath = Console.ReadLine();
			Console.ForegroundColor = ConsoleColor.Red;

			try
			{
				System.IO.StreamReader file =
					new System.IO.StreamReader(fPath);

				while ((line = file.ReadLine()) != null)
				{
					Console.WriteLine(line);
					counter++;
				}
			}
			catch
			{
				Console.WriteLine("File not found, or Path is invalid.");
			}
		}

		#endregion

		#region Text to Byte/Hex Reversing

		public static void StringToByte()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Type in a string you would like to convert to a byte: ");
			Console.ForegroundColor = ConsoleColor.Green;
			string input = Console.ReadLine();
			Console.ForegroundColor = ConsoleColor.Red;

			byte[] array = Encoding.ASCII.GetBytes(input);
			foreach (byte element in array)
			{
				Console.WriteLine("{0} = {1}", element, (char)element);
			}

			Console.WriteLine("");
			Console.WriteLine("Binary:");
			string result = string.Empty;
			foreach (char ch in input)
			{
				result += Convert.ToString((int)ch,2);
			}
			Console.WriteLine(result);
		}

		public static void StringToHEX()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Type in a string you would like to convert to HEX: ");
			Console.ForegroundColor = ConsoleColor.Green;
			string input = Console.ReadLine();
			Console.ForegroundColor = ConsoleColor.Red;

			char[] values = input.ToCharArray();
			Console.WriteLine("Char = HEX");

			foreach (char letter in values)
			{
				int value = Convert.ToInt32(letter);
				string hexOut = String.Format("{0:X}", value);
				Console.WriteLine("CHAR:{0}, HEX:{1}, INT:{2}", letter, hexOut, value);
			}
		}

		#endregion
	}
}