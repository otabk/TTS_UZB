using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	class Program
	{
		private static string[] _birlik = new string[] { "бир", "икки", "уч", "тўрт", "беш", "олти", "етти", "саккиз", "тўққиз" },
						 _10lik = new string[] { "ўн", "йигирма", "ўттиз", "қирқ", "эллик", "олтмиш", "етмиш", "саксон", "тўқсон" },
						_1000lik = new string[] { "юз", "минг", "миллион", "миллиард", "триллион" },
						_kasr = new string[] { "ўндан", "юздан", "мингдан", "миллиондан", "миллиарддан", "триллиондан" };
		private static bool _isKasr = false;

		static void Main(string[] args)
		{
			string input = Console.ReadLine();
			var c = input.ToString().ToCharArray();
			var numbers = Array.ConvertAll(c, i => (int)char.GetNumericValue(i));
			var nlemght = numbers.Length;
			List<string> result = new List<string>();
			for (int i = 0, j = nlemght; i < nlemght; i++, j--)
			{
				result.Add(Num2Text(numbers[i], j));
			}
			Console.WriteLine(string.Join(" ", result.ToArray()));
			Console.ReadKey();
		}

		private static string Num2Text(int i, int index)
		{
			var sb = new List<string>();
			switch (index)
			{
				case 1:
					sb.Add(DigitToText(i));
					break;
				case 2:
					sb.Add(Digit10ToText(i));
					break;
				case 3:
				if (i == 0)
					break;
					sb.Add(DigitToText(i));
					sb.Add(_1000lik[0]);
					break;
				case 4:
					sb.Add(DigitToText(i));
					sb.Add(_1000lik[1]);
					break;
				case 5:
					sb.Add(Digit10ToText(i));
					break;
				case 6:
					sb.Add(DigitToText(i));
					sb.Add(_1000lik[0]);
					break;
				case 7:
					sb.Add(DigitToText(i));
					sb.Add(_1000lik[2]);
					break;
				case 8:
					sb.Add(Digit10ToText(i));
					break;
				case 9:
					sb.Add(DigitToText(i));
					sb.Add(_1000lik[0]);
					break;
				case 10:
					sb.Add(DigitToText(i));
					sb.Add(_1000lik[3]);
					break;
				case 11:
					sb.Add(Digit10ToText(i));
					break;
				case 12:
					sb.Add(DigitToText(i));
					sb.Add(_1000lik[0]);
					break;
				case 13:
					sb.Add(DigitToText(i));
					sb.Add(_1000lik[4]);
					break;
				case 14:
					sb.Add(Digit10ToText(i));
					break;
				case 15:
					sb.Add(DigitToText(i));
					sb.Add(_1000lik[0]);
					break;
					//todo kasr sonlarni qoshish
				default:
					break;
			}
			return string.Join(" ", sb.ToArray());
		}

		private static string DigitToText(int c)
		{
			string r = "";
			switch (c)
			{
				case 1:
					r = _birlik[0];
					break;
				case 2:
					r = _birlik[1];
					break;
				case 3:
					r = _birlik[2];
					break;
				case 4:
					r = _birlik[3];
					break;
				case 5:
					r = _birlik[4];
					break;
				case 6:
					r = _birlik[5];
					break;
				case 7:
					r = _birlik[6];
					break;
				case 8:
					r = _birlik[7];
					break;
				case 9:
					r = _birlik[8];
					break;
				default:
					break;
			}
			return r;
		}

		private static string Digit10ToText(int c)
		{
			string r = "";
			switch (c)
			{
				case 1:
					r = _10lik[0];
					break;
				case 2:
					r = _10lik[1];
					break;
				case 3:
					r = _10lik[2];
					break;
				case 4:
					r = _10lik[3];
					break;
				case 5:
					r = _10lik[4];
					break;
				case 6:
					r = _10lik[5];
					break;
				case 7:
					r = _10lik[6];
					break;
				case 8:
					r = _10lik[7];
					break;
				case 9:
					r = _10lik[8];
					break;
				default:
					break;
			}
			return r;
		}

		public static string CreateMD5(string input)
		{
			// Use input string to calculate MD5 hash
			using (MD5 md5 = MD5.Create())
			{
				byte[] inputBytes = Encoding.ASCII.GetBytes(input);
				byte[] hashBytes = md5.ComputeHash(inputBytes);

				// Convert the byte array to hexadecimal string
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < hashBytes.Length; i++)
				{
					sb.Append(hashBytes[i].ToString("x2"));
				}
				return sb.ToString();
			}
		}
	}
}
