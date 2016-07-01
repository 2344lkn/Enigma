using System;

namespace Enigma
{
	public class Crypto
	{
		// TODO: Skype Encrypted Communication Message()
		#region Encrypted Communication

		//static int SIZE = (1 << 4);

		// Encrypted Communication Concept using Skype
		static void Message()
		{
			// using SKYPE4COMLib
			//Skype skype = new Skype();

			Console.WriteLine("Encrypt/Decrypt Skype");
			Console.WriteLine("1. Mode Encrypt");
			Console.WriteLine("2. Mode Decrypt");
			Console.WriteLine("Insert mode : ");

			int mode = Convert.ToInt32(Console.ReadLine());

			if (mode == 1)
			{
			}
		}

		#endregion

		#region kTm Crypto

		public static void kTmEncrypt()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;

			Console.WriteLine("*** Starting Encryption [MODE=ECB]***");
			kTmEngine.StartEncryption("test.txt", "e-ECB.txt", "12345", kTmEngine.ECB, 16);
			Console.WriteLine("Complete");

			Console.WriteLine("*** Starting Encryption [MODE=CBC]***");
			kTmEngine.StartEncryption("test.txt", "e-CBC.txt", "12345", kTmEngine.CBC, 16);
			Console.WriteLine("Complete");

			Console.WriteLine("*** Starting Encryption [MODE=CFB]***");
			kTmEngine.StartEncryption("test.txt", "e-CFB.txt", "12345", kTmEngine.CFB, 16);
			Console.WriteLine("Complete");

			Console.WriteLine("*** Starting Encryption [MODE=OFB]***");
			kTmEngine.StartEncryption("test.txt", "e-OFB.txt", "12345", kTmEngine.OFB, 16);
			Console.WriteLine("Complete");
		}
		// TODO: Fix Modes that do not Decrypt correctly
		public static void kTmDecrypt()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;

			Console.WriteLine("*** Starting Decryption [ECB]***");
			kTmEngine.StartDecryption("e-ECB.txt", "d-ECB.txt", "12345");
			Console.WriteLine("Complete");

			Console.WriteLine("*** Starting Decryption [CBC]***");
			kTmEngine.StartDecryption("e-CBC.txt", "d-CBC.txt", "12345");
			Console.WriteLine("Complete");

			Console.WriteLine("*** Starting Decryption [CFB]***");
			kTmEngine.StartDecryption("e-CFB.txt", "d-CFB.txt", "12345");
			Console.WriteLine("Complete");

			Console.WriteLine("*** Starting Decryption [OFB]***");
			kTmEngine.StartDecryption("e-OFB.txt", "d-OFB.txt", "12345");
			Console.WriteLine("Complete");
		}

		static void print(byte[] x)
		{
			for (int i = 0; i < x.Length; i++)
			{
				Console.Write(x[i]);
				Console.Write(" ");
			}
			Console.WriteLine();
		}

		public static void BC()
		{
			Random r = new Random();
			byte[] input = new byte[1 << 4];
			byte[] key = new byte[1 << 4];
			r.NextBytes(input);
			r.NextBytes(key);

			Console.WriteLine("Input:");
			print(input);
			Console.WriteLine("Key:");
			print(key);

			blockCipher Block = new blockCipher(input, key);
			Console.WriteLine("EN:");
			print(Block.encrypt());

			blockCipher Block2 = new blockCipher(Block.encrypt(), key);
			Console.WriteLine("DC:");
			print(Block2.decrypt());
		}

		public static void BCED()
		{
			Random r = new Random();
			byte[] input = new byte[1 << 4];
			byte[] key = new byte[1 << 4];
			r.NextBytes(input);
			r.NextBytes(key);

			Console.WriteLine("Input:");
			print(input);
			Console.WriteLine("Key:");
			print(key);

			blockCipherEnDc Block = new blockCipherEnDc(key, input);

			Console.WriteLine("Cipher:");
			print(Block.encrypt());

			blockCipherEnDc Block2 = new blockCipherEnDc(key, Block.encrypt());

			Console.WriteLine("PL:");
			print(Block2.decrypt());
		}

		#endregion

		#region Polynomial Crypto

		public static void PolyEncrypt()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("-=[ Polynomial Encryption ]=-");
			Console.WriteLine("This is an example of Polynomial Encryption, each time");
			Console.WriteLine("a message is encrypted the final output will be different");
			Console.WriteLine("even if the message and key are the same. This method");
			Console.WriteLine("is not a secure encryption use kTm Algo for secured crypto.");
			Console.WriteLine("");

			Console.WriteLine("Type the text or message you would like to encrypt:");
			Console.ForegroundColor = ConsoleColor.Green;
			string text = Console.ReadLine();
			Console.WriteLine("");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Type in a password/key that is 5+ characters long:");
			Console.ForegroundColor = ConsoleColor.Green;
			string pass = Console.ReadLine();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("");

			PolyCrypt.polyEncryptTxt(text, pass);
		}

		public static void PolyDecrypt()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("-=[ Polynomial Decryption ]=-");
			Console.WriteLine("");

			Console.WriteLine("Paste your encrypted text or message:");
			Console.ForegroundColor = ConsoleColor.Green;
			string text = Console.ReadLine();
			Console.WriteLine("");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Type in a key/password to decrypt text or message:");
			Console.ForegroundColor = ConsoleColor.Green;
			string pass = Console.ReadLine();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("");

			PolyCrypt.polyDecryptTxt(text, pass);
		}

		#endregion
	}
}