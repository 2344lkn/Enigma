using System;

namespace Enigma
{
	public class blockCipher
	{
		private byte[] Input;
		private byte[] Key;
		private byte[] CipherA;
		private byte[] CipherB;

		public const int SIZE4 = (1 << 4);
		public const int SIZE3 = (1 << 3);
		public const int KTM_COUNT = (1 << 4);

		private byte[] decryptKTM()
		{
			initialization();
			byte[] tempCipherA = new byte[SIZE3];
			byte[] tempCipherB = new byte[SIZE3];

			for (int i = 0; i < KTM_COUNT; i++)
			{
				tempCipherB = duplicate(CipherA);
				tempCipherA = bitwiseXOR(CipherB, new blockCipherEnDc(Key, CipherA).encrypt());

				tempCipherB = duplicate(tempCipherA);
				tempCipherA = duplicate(tempCipherB);
			}

			byte[] ret = new byte[SIZE4];
			int k = 0;
			for (; k < SIZE3; k++)
			{
				ret[k] = CipherA[k];
			}
			for (int j = 0; k < SIZE4; k++, j++)
			{
				ret[k] = CipherB[j];
			}
			return ret;
		}

		private byte[] encryptKTM()
		{
			initialization();
			byte[] tempCipherA = new byte[SIZE3];
			byte[] tempCipherB = new byte[SIZE3];
			for (int i = 0; i < KTM_COUNT; i++)
			{
				tempCipherA = duplicate(CipherB);
				tempCipherB = bitwiseXOR(CipherA, new blockCipherEnDc(Key, CipherB).encrypt());

				CipherA = duplicate(tempCipherA);
				CipherB = duplicate(tempCipherB);
			}

			byte[] ret = new byte[SIZE4];
			int k = 0;
			for (; k < SIZE3; k++)
			{
				ret[k] = CipherA[k];
			}
			for (int j = 0; k < SIZE4; k++, j++)
			{
				ret[k] = CipherB[j];
			}
			return ret;
		}

		private byte[] bitwiseXOR(byte[] A, byte[] B)
		{
			byte[] ret = new byte[A.Length];
			for (int i = 0; i < A.Length; i++)
			{
				ret[i] = (byte)(A[i] ^ B[i]);
			}
			return ret;
		}

		private void initialization()
		{
			CipherA = new byte[SIZE3];
			CipherB = new byte[SIZE3];
			int i = 0;
			for (; i < SIZE3; i++)
			{
				CipherA[i] = Input[i];
			}
			for (int k = 0; i < SIZE4; i++, k++)
			{
				CipherB[k] = Input[i];
			}
		}

		private byte[] duplicate(byte[] x)
		{
			byte[] ret = new byte[x.Length];
			for (int i = 0; i < x.Length; i++)
			{
				ret[i] = x[i];
			}
			return ret;
		}

		public blockCipher(byte[] input, byte[] key)
		{
			Input = duplicate(input);
			Key = duplicate(key);
		}

		public byte[] encrypt()
		{
			return encryptKTM();
		}
		public byte[] decrypt()
		{
			return decryptKTM();
		}
	}
}