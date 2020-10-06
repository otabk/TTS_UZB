using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
	class Program
	{
		private static string[] _birlik = new string[] { "бир", "икки", "уч", "тўрт", "беш", "олти", "етти", "саккиз", "тўққиз" },
						 _10lik = new string[] { "ўн", "йигирма", "ўттиз", "қирқ", "эллик", "олтмиш", "етмиш", "саксон", "тўқсон" },
						_1000lik = new string[] { "юз", "минг", "миллион", "миллиард", "триллион" },
						_kasr = new string[] { "ўндан", "юздан", "мингдан", "миллиондан", "миллиарддан", "триллиондан" };
		private static bool _isKasr = false;
		private static string _butun = "бутун";

		static void Main(string[] args)
		{
			/*string input = Console.ReadLine();
			var c = input.ToString().ToCharArray();
			var numbers = Array.ConvertAll(c, i => (int)char.GetNumericValue(i));
			var nlemght = numbers.Length;
			List<string> result = new List<string>();
			for (int i = 0, j = nlemght; i < nlemght; i++, j--)
			{
				result.Add(Num2Text(numbers[i], j));
			}
			Console.WriteLine(string.Join(" ", result.ToArray()));*/

			/*string str = "ён"; kirildan lotinga
			string[] lat_low = { "a", "b", "v", "g", "d", "e", "yo", "j", "z", "i", "y", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "x", "ts", "ch", "sh", "\'", "e", "yu", "ya" };
			string[] rus_low = { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "ъ", "э", "ю", "я" };
			for (int i = 0; i < 30; i++)
			{
				str = str.Replace(rus_low[i], lat_low[i]);
			}
			Console.WriteLine(str);*/

			/* ovoz bilan ishlash
			SoundPlayer player = new SoundPlayer();
			Dictionary<string, List<string>> audioPaths = null;
			using (var sr = File.OpenText("AudioPath.txt"))
			{
				string paths = sr.ReadToEnd();
				audioPaths = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(paths);
				for (int i = 0; i < audioPaths.Count; i++)
				{
					foreach (List<string> item in audioPaths.Values)
					{
						player.SoundLocation = item[0];
						player.Play();
						Thread.Sleep(100);
					}
				}
			}
			player.Dispose();*/

			/* // audio bazani shakllantirish
			Dictionary<string, List<string>> templist = new Dictionary<string, List<string>>();
			//Console.WriteLine(JsonConvert.SerializeObject(dict));
			List<string> list = new List<string>();
			using (var sr = File.OpenText("paths.txt"))
			{
				string[] paths = sr.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < paths.Length; i++)
				{
					var s = paths[i].Split('_');
					//var templist = new Dictionary<string, List<string>>();
					string name = s[0], path = s[1];
					if (i == 0 || !templist.ContainsKey(s[0]))
					{
						templist.Add(s[0], new List<string>() { @"Data\" + paths[i] });
						continue;
					}
					if (templist.ContainsKey(s[0]))
					{
						templist[s[0]].Add(@"Data\" + paths[i]);
					}
				}
			}
			var result = JsonConvert.SerializeObject(templist);
			Console.WriteLine(result);
			using (var sw = File.CreateText("AudioPath.txt"))
			{
				sw.Write(result);
			}
			*/

			//wav faylni o'qish
			//var first = new AudioFileReader(@"Data\ax_2.wav");
			//var second = new AudioFileReader(@"Data\bo_2.wav");
			//var third = new AudioFileReader(@"Data\rot_1.wav");
			//var fours = new AudioFileReader(@"Data\ax_2.wav");
			//var fives = new AudioFileReader(@"Data\bo_2.wav");
			//var six = new AudioFileReader(@"Data\rot_1.wav");

			//var takeDuration1 = new TimeSpan(0, 0, 0, 0, (int)(first.TotalTime.Milliseconds * 0.8)); // otherwise it would emit indefinitely
			//var takeDuration2 = new TimeSpan(0, 0, 0, 0, (int)(second.TotalTime.Milliseconds * 0.8));
			//var takeDuration3 = new TimeSpan(0, 0, 0, 0, (int)(third.TotalTime.Milliseconds * 0.8));

			//ISampleProvider[] sources = new[]
			//{
			//	first.Take(takeDuration1),
			//	second.Take(takeDuration2),
			//	third.Take(takeDuration3)
			//};

			//var sources2 = new[]
			//{
			//	fours.Take(takeDuration1),
			//	fives.Take(takeDuration2),
			//	six.Take(takeDuration3)
			//};

			//var playlist2 = new ConcatenatingSampleProvider(sources2);

			//var playlist = new ConcatenatingSampleProvider(sources);
			////var waveProvider = mixingSampleProvider.ToWaveProvider();
			//using (var wo = new WaveOutEvent())
			//{
			//	wo.Init(playlist);
			//	wo.Play();
			//	while (wo.PlaybackState == PlaybackState.Playing) Thread.Sleep(1000);
			//	wo.Init(playlist2);
			//	wo.Play();
			//	while (wo.PlaybackState == PlaybackState.Playing) Thread.Sleep(1000);
			//}

			//using (var wo2 = new WaveOutEvent())
			//{
			//	wo2.Init(playlist2);
			//	wo2.Play();
			//	while (wo2.PlaybackState == PlaybackState.Playing) Thread.Sleep(100);
			//}

			Regex pattern = new Regex(@"[0-9]+[,.][0-9]+");
			var str = "123 12.36 313262,12 123  -  123";
			var matches = pattern.Matches(str);
			Console.WriteLine($"Matches count: {matches.Count}");
			for (int i = 0; i < matches.Count; i++)
			{
				var tmparr = matches[i].Value.Split(new[] { ".", "," }, StringSplitOptions.RemoveEmptyEntries);
				Console.WriteLine($"{matches[i].Value} {Num2Text(tmparr[0])} бутун {Kasr(tmparr[1].Length)} {Num2Text(tmparr[1])}");
			}

			//var str = "fooBarfooSomethingfoo";
			//var r = AllIndexesOf(str, "foo");
			//foreach (int i in r )
			//{
			//	Console.WriteLine(i);
			//}

			Console.WriteLine("Ta-da!!!!");
			Console.ReadKey();
		}

		public static IEnumerable<int> AllIndexesOf(string str, string value)
		{
			if (string.IsNullOrEmpty(value))
				throw new ArgumentException("the string to find may not be empty", "value");
			for (int index = 0; ; index += value.Length)
			{
				index = str.IndexOf(value, index);
				if (index == -1)
					break;
				yield return index;
			}
		}

		private static string Num2Text(string number) 
		{
			var sb = new List<string>();
			var charArray = number.ToCharArray();
			var numbers = Array.ConvertAll(charArray, i => (int)char.GetNumericValue(i));
			var nlemght = numbers.Length;
			for (int i = 0, j = nlemght; i < nlemght; i++, j--)
			{
				switch (j)
				{
					case 1:
						sb.Add(DigitToText(numbers[i]));
						break;
					case 2:
						sb.Add(Digit10ToText(numbers[i]));
						break;
					case 3:
						if (numbers[i] == 0)
							break;
						sb.Add(DigitToText(numbers[i]));
						sb.Add(_1000lik[0]);
						break;
					case 4:
						sb.Add(DigitToText(numbers[i]));
						sb.Add(_1000lik[1]);
						break;
					case 5:
						sb.Add(Digit10ToText(numbers[i]));
						break;
					case 6:
						sb.Add(DigitToText(numbers[i]));
						sb.Add(_1000lik[0]);
						break;
					case 7:
						sb.Add(DigitToText(numbers[i]));
						sb.Add(_1000lik[2]);
						break;
					case 8:
						sb.Add(Digit10ToText(numbers[i]));
						break;
					case 9:
						sb.Add(DigitToText(numbers[i]));
						sb.Add(_1000lik[0]);
						break;
					case 10:
						sb.Add(DigitToText(numbers[i]));
						sb.Add(_1000lik[3]);
						break;
					case 11:
						sb.Add(Digit10ToText(numbers[i]));
						break;
					case 12:
						sb.Add(DigitToText(numbers[i]));
						sb.Add(_1000lik[0]);
						break;
					case 13:
						sb.Add(DigitToText(numbers[i]));
						sb.Add(_1000lik[4]);
						break;
					case 14:
						sb.Add(Digit10ToText(numbers[i]));
						break;
					case 15:
						sb.Add(DigitToText(numbers[i]));
						sb.Add(_1000lik[0]);
						break;
					//todo kasr sonlarni qoshish
					default:
						break;
				}
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

		private static string Kasr(int lenght)
		{
			var result = "";
			switch (lenght)
			{
				case 1:
					result = "ўндан";
					break;
				case 2:
					result = "юздан";
					break;
				case 3:
					result = "мингдан";
					break;
				case 4:
					result = "ўн мингдан";
					break;
				case 5:
					result = "юз мингдан";
					break;
				case 6:
					result = "миллиондан";
					break;
				case 7:
					result = "ўн миллиондан";
					break;
				case 8:
					result = "юз миллиондан";
					break;
				case 9:
					result = "миллиарддан";
					break;
				default:
					break;
			}
			return result;
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
