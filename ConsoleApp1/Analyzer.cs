using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	class Analyzer
	{
		string _word;

		public Analyzer(string w)
		{
			_word = w;
		}

		public IEnumerable<string> Analyze(string word)
		{
			word = word.ToLower();
			List<string> slogs = new List<string>();
			int jamiunli = 0;
			int wordLenght = word.Length;
			//int[] wordMap = new int[wordLenght];
			string wordMap = "";
			StringBuilder wordMapBuilder = new StringBuilder();
			string tempword = word;
			int position = 0;
			for (int i = 0; i < word.Length; i++) //todo lotin alfavit uchun ham yozish kerak
			{
				if (word[i] == 'а' || //а
						word[i] == 'о' || //о
						word[i] == 'и' || //И
						word[i] == 'э' || //э
						word[i] == 'у' || //у
						word[i] == 'ў' || //ў
						word[i] == 'е' || //е
						word[i] == 'ё' || //ё
						word[i] == 'ю' || //ю
						word[i] == 'я') //я
				{
					//wordMap[i] = '0';
					wordMapBuilder.Append("0");
					jamiunli++;
				}
				else
				{
					if (word[i] == 'ъ') //Ъ ва ъ белгиларини аниклаш
					{
						//wordMap[i] = '2';
						wordMapBuilder.Append("2");
					}
					else
					{
						if (word[i] == 'ь') //Ь ва ь белгиларини аниклаш
						{
							//wordMap[i] = '3';
							wordMapBuilder.Append("3");
						}
						else
						{
							//wordMap[i] = '1';
							wordMapBuilder.Append("1");
						}
					}
				}
			}

			wordMap = wordMapBuilder.ToString();

			// агар суз бир бугиндан иборат булса
			if (jamiunli == 1)
			{
				slogs.Add(word);
				return slogs;
			}
			// Суз икки ва ундан куп бугиндан иборат булса (боши) --------------------------------------------------------------------------------
			else
			{
				for (int i = 0; i < ; i++)
				{
					// охирги бзғинни олиш
					if(jamiunli == 1)
					{
						slogs.Add(tempword);
						return slogs;
					}

					// охири "ё" билан тугаган сузлар билан ишлаш
					if ((jamiunli == 2) && (word[wordLenght - 1] == 'ё')) // 2 та унли ва охири "ё" булса
					{
						for (int j = position; j < wordLenght - 1; j++)
						{
							tempword += word[j];
						}
						slogs.Add(tempword);
						slogs.Add("ё");
						return slogs;
					}
					/* maxsus so'zlaga tekshirish */
					/**/
					// Агар суз унли харфдан бошланса------------------------------------------------------------------------------------
					if (wordMap[0] == '0')
					{
						if (tempword.Length  > 4) // агар суз узунлиги 4 харфдан катта булса
						{
							// 0111+0 холатни аниклаш
							if (wordMap[0] == '0' && wordMap[1] == '1' && wordMap[2] == '1' && wordMap[3] == '1' && wordMap[4] == '0') //гссс+г
							{
								slogs.Add(tempword.Substring(0, 3)); //011 ни олиш (авжланмок, антракт)
								tempword = tempword.Remove(0, 3);// Substring(2, wordLenght - position  - 3);
								wordMap.Remove(0, 3);
								continue;
							}
						}
						if (tempword.Length  > 3) // агар суз узунлиги 3 харфдан катта булса
						{
							// 011+1 холатни аниклаш
							if (wordMap[0] == '0' && wordMap[1] == '1' && wordMap[2] == '1' && wordMap[3] == '1') //гсс+с
							{
								slogs.Add(tempword.Substring(0, 3)); //011 ни олиш
								tempword = tempword.Remove(0, 3);
								wordMap.Remove(0, 3);
								continue;
							}
							// 011+0 холатни аниклаш
							if (wordMap[0] == '0' && wordMap[1] == '1' && wordMap[2] == '1' && wordMap[3] == '0') //гсс+г
							{
								slogs.Add(tempword.Substring(0, 2)); //01 ни олиш
								tempword = tempword.Remove(0, 2);
								wordMap.Remove(0, 2);
								continue;
							}
							// 02 холатни аниклаш
							if (wordMap[0] == '0' && wordMap[1] == '2') //г+ъ
							{
								slogs.Add(tempword.Substring(0, 2)); //02 ни олиш
								tempword = tempword.Remove(0, 2);
								wordMap.Remove(0, 2);
								continue;
							}
							// 012 холатни аниклаш
							if (wordMap[0] == '0' && wordMap[1] == '1' && wordMap[2] == '2') //гс+ъ
							{
								slogs.Add(tempword.Substring(0, 3)); //012 ни олиш
								tempword = tempword.Remove(0, 3);
								wordMap.Remove(0, 3);
								continue;
							}
						}
						else // агар суз узунлиги 3 харф ва ундан кичик булса
						{
							// 013 холатни аниклаш (альфа, ультра) юмшатиш белгили сузлар
							if (wordMap[0] == '0' && wordMap[1] == '1' && wordMap[2] == '3') // гсь
							{
								slogs.Add(tempword.Substring(0, 3)); //013 ни олиш
								tempword = tempword.Remove(0, 3);
								wordMap.Remove(0, 3);
								continue;
							}

							// 01+0 холатни аниклаш
							if (wordMap[0] == '0' && wordMap[1] == '1' && wordMap[2] == '0') //сг+с
							{
								if ((word[1] == 1049 || word[1] == 1081) && (word[2] == 1025 || word[2] == 1105)) // агар унлидан кейин "йё" булса
								{
									slogs.Add(tempword.Substring(0, 2)); //01 ни олиш (унли+й ни олиш) - ай-ёр, ай-ём
									tempword = tempword.Remove(0, 2);
									wordMap.Remove(0, 2);
									continue;
								}
								else
								{
									slogs.Add(tempword.Substring(0, 1)); //0 ни олиш (акс холда факат унлини олиш)
									tempword = tempword.Remove(0, 1);
									wordMap.Remove(0, 1);
									continue;
								}
							}
							// 0+0 холатни аниклаш
							if (wordMap[0] == '0' && wordMap[1] == '0') //г+г
							{
								slogs.Add(tempword.Substring(0, 1)); //0 ни олиш
								tempword = tempword.Remove(0, 1);
								wordMap.Remove(0, 1);
								continue;
							}
						}
					}
					else // Агар суз ундош харфдан бошланса
					{
						if (wordLenght - position  > 5) // агар суз узунлиги 5 харфдан катта булса
						{
							// 11011+1 холатни аниклаш
							if (wordMap[0] == '1' && wordMap[1] == '1' && wordMap[2] == '0' && wordMap[3] == '1' && wordMap[4] == '1' && wordMap[5] == '1') //ссгсс+с
							{
								slogs.Add(tempword.Substring(0, 5)); //11011 ни олиш
								tempword = tempword.Remove(0, 5);
								wordMap.Remove(0, 5);
								continue;
							}
							// 11011+0 холатни аниклаш
							if (wordMap[0] == '1' && wordMap[1] == '1' && wordMap[2] == '0' && wordMap[3] == '1' && wordMap[4] == '1' && wordMap[5] == '0') //ссгсс+с
							{
								slogs.Add(tempword.Substring(0, 4));  //1101 ни олиш
								tempword = tempword.Remove(0, 4);
								wordMap.Remove(0, 4);
								continue;
							}
							// 101311+1 холатни аниклаш (фильтрлаш, фильтрнинг), юмшатиш белгили сузлар
							if (wordMap[0] == '1' && wordMap[1] == '0' && wordMap[2] == '1' && wordMap[3] == '3' && wordMap[4] == '1' && wordMap[5] == '1' && wordMap[6] == '1') //сгсьсс+с
							{
								slogs.Add(tempword.Substring(0, 6)); //101311 ни олиш
								tempword = tempword.Remove(0, 6);
								wordMap.Remove(0, 6);
								continue;
							}

							// 10131+1 холатни аниклаш (мультфильм, фильмнинг), юмшатиш белгили сузлар
							if (wordMap[0] == '1' && wordMap[1] == '0' && wordMap[2] == '1' && wordMap[3] == '3' && wordMap[4] == '1' && wordMap[5] == '1') //сгсьс+с
							{
								slogs.Add(tempword.Substring(0, 5)); //10131 ни олиш
								tempword = tempword.Remove(0,5);
								wordMap.Remove(0, 5);
								continue;
							}

							// 10111+0 холатни аниклаш
							if (wordMap[0] == '1' && wordMap[1] == '0' && wordMap[2] == '1' && wordMap[3] == '1' && wordMap[4] == '1' && wordMap[5] == '0') //сгссс+г
							{
								slogs.Add(tempword.Substring(0, 3)); //101 ни олиш (кон-тракт)
								tempword = tempword.Remove(0, 3);
								wordMap.Remove(0, 3);
								continue;
							}
						}

						if (wordLenght - position > 4) // агар суз узунлиги 4 харфдан катта булса
						{
							// 1101+1 холатни аниклаш
							if (wordMap[0] == '1' && wordMap[1] == '1' && wordMap[2] == '0' && wordMap[3] == '1' && wordMap[4] == '1') //ссгс+с
							{
								slogs.Add(tempword.Substring(0, 4)); //1101 ни олиш
								tempword = tempword.Remove(0, 4);
								wordMap.Remove(0, 4);
								continue;
							}
							// 1101+0 холатни аниклаш
							if (wordMap[0] == '1' && wordMap[1] == '1' && wordMap[2] == '0' && wordMap[3] == '1' && wordMap[4] == '0') //ссгс+г
							{
								slogs.Add(tempword.Substring(0, 3)); //110 ни олиш
								tempword = tempword.Remove(0, 3);
								wordMap.Remove(0, 3);
								continue;
							}
							// 1101+3 холатни аниклаш (статья)
							if (wordMap[0] == '1' && wordMap[1] == '1' && wordMap[2] == '0' && wordMap[3] == '1' && wordMap[4] == '3') //ссгс+ь
							{
								slogs.Add(tempword.Substring(0, 5)); //11013 ни олиш
								tempword = tempword.Remove(0, 5);
								wordMap.Remove(0, 5);
								continue;
							}

							// 1011+1 холатни аниклаш
							if (wordMap[0] == '1' && wordMap[1] == '0' && wordMap[2] == '1' && wordMap[3] == '1' && wordMap[4] == '1') //сгсс+с
							{
								slogs.Add(tempword.Substring(0, 4)); //1011 ни олиш
								tempword = tempword.Remove(0, 4);
								wordMap.Remove(0, 4);
								continue;
							}
							// 1011+0 холатни аниклаш
							if (wordMap[0] == '1' && wordMap[1] == '0' && wordMap[2] == '1' && wordMap[3] == '1' && wordMap[4] == '0') //сгсс+г
							{
								slogs.Add(tempword.Substring(0, 3)); //101 ни олиш
								tempword = tempword.Remove(0, 3);
								wordMap.Remove(0, 3);
								continue;
							}

							// 1011+3 холатни аниклаш (компьютер), юмшатиш белгили сузлар
							if (wordMap[0] == '1' && wordMap[1] == '0' && wordMap[2] == '1' && wordMap[3] == '1' && wordMap[4] == '3') //сгсс+ь
							{
								slogs.Add(tempword.Substring(0, 5)); //1011+3 ни олиш
								tempword = tempword.Remove(0, 5);
								wordMap.Remove(0, 5);
								continue;
							}
							// 1013 холатни аниклаш (мульти, бальзам), юмшатиш белгили сузлар
							if (wordMap[0] == '1' && wordMap[1] == '0' && wordMap[2] == '1' && wordMap[3] == '3') //сгсь
							{
								slogs.Add(tempword.Substring(0, 4)); //1013 ни олиш
								tempword = tempword.Remove(0, 4);
								wordMap.Remove(0, 4);
								continue;
							}

							// Айриш белгиси катнашган сузларда бугин кучириш
							// 1201 холатни аниклаш (съём-ка)
							if (wordMap[0] == '1' && wordMap[1] == '2' && wordMap[2] == '0' && wordMap[3] == '1') //съгс
							{
								slogs.Add(tempword.Substring(0, 4)); //1201 ни олиш
								tempword = tempword.Remove(0, 4);
								wordMap.Remove(0, 4);
								continue;
							}
							// 1012 холатни аниклаш
							if (wordMap[0] == '1' && wordMap[1] == '0' && wordMap[2] == '1' && wordMap[3] == '2') //сгсъ
							{
								slogs.Add(tempword.Substring(0, 4)); //1012 ни олиш
								tempword = tempword.Remove(0, 4);
								wordMap.Remove(0, 4);
								continue;
							}
							// 1021+1 холатни аниклаш
							if (wordMap[0] == '1' && wordMap[1] == '0' && wordMap[2] == '2' && wordMap[3] == '1' && wordMap[4] == '1') //сгъс+с
							{
								slogs.Add(tempword.Substring(0, 4)); //1021 ни олиш
								tempword = tempword.Remove(0,4);
								wordMap.Remove(0, 4);
								continue;
							}
							// 1021+0 холатни аниклаш
							if (wordMap[0] == '1' && wordMap[1] == '0' && wordMap[2] == '2' && wordMap[3] == '1' && wordMap[4] == '0') //сгъс+г
							{
								slogs.Add(tempword.Substring(0, 3)); //102 ни олиш
								tempword = tempword.Remove(0,3);
								wordMap.Remove(0, 3);
								continue;
							}
						}

						// 110 холатни аниклаш
						if (wordMap[0] == '1' && wordMap[1] == '1' && wordMap[2] == '0') //ссг кейинги товушни текшириш шарт эмас
						{
							slogs.Add(tempword.Substring(0, 3)); //110 ни олиш
							tempword = tempword.Remove(0, 3);
							wordMap.Remove(0, 3);
							continue;
						}

						// 101+1 холатни аниклаш
						if (wordMap[0] == '1' && wordMap[1] == '0' && wordMap[2] == '1' && wordMap[3] == '1') //сгс+с
						{
							slogs.Add(tempword.Substring(0, 3)); //101 ни олиш
							tempword = tempword.Remove(0, 3);
							wordMap.Remove(0, 3);
							continue;
						}

						// 101+0 холатни аниклаш
						if (wordMap[0] == '1' && wordMap[1] == '0' && wordMap[2] == '1' && wordMap[3] == '0') //сгс+с
						{
							slogs.Add(tempword.Substring(0, 2)); //10 ни олиш
							tempword = tempword.Remove(0, 2);
							wordMap.Remove(0, 2);
							continue;
						}

						// 102+0 холатни аниклаш
						if (wordMap[0] == '1' && wordMap[1] == '0' && wordMap[2] == '2' && wordMap[3] == '0') //сгъ
						{
							slogs.Add(tempword.Substring(0, 3)); //102 ни олиш
							tempword = tempword.Remove(0, 3);
							wordMap.Remove(0, 3);
							continue;
						}

						// 10 холатни аниклаш
						if (wordMap[0] == '1' && wordMap[1] == '0') //сг кейинги товушни текшириш шарт эмас
						{
							slogs.Add(tempword.Substring(0, 2)); //10 ни олиш
							tempword = tempword.Remove(0, 2);
							wordMap.Remove(0, 2);
							continue;
						}
					}
				}
			}
			return slogs;
		}


		public string IsAero(string word)
		{
			word = word.ToLower();
			string result = "";
			if (word.Contains("аэро"))
			{
				int index = word.IndexOf("аэро");
				if (index == '0')
				{
					result = "а-э-ро";
				}
				else
				{
					result = "а-э-ро-";
				}
			}
			return result;
		}

		public string IsAvia(string word)
		{
			word = word.ToLower();
			string result = "";
			if (word.Contains("авиа"))
			{
				int index = word.IndexOf("авиа");
				if (index == '0')
				{
					result = "а-ви-а";
				}
				else
				{
					result = "а-ви-а-";
				}
			}
			return result;
		}

		public string IsFoto(string word)
		{
			word = word.ToLower();
			string result = "";
			if (word.Contains("фото"))
			{
				int index = word.IndexOf("фото");
				if (index == '0')
				{
					result = "фо-то";
				}
				else
				{
					result = "фо-то-";
				}
			}
			return result;
		}

		public string IsFoton(string word)
		{
			word = word.ToLower();
			string result = "";
			if (word.Contains("фотон"))
			{
				int index = word.IndexOf("фотон");
				if (index == '0')
				{
					result = "фо-тон";
				}
				else
				{
					result = "фо-тон-";
				}
			}
			return result;
		}

		public string IsTele(string word)
		{
			word = word.ToLower();
			string result = "";
			if (word.Contains("теле"))
			{
				int index = word.IndexOf("теле");
				if (index == '0')
				{
					result = "те-ле";
				}
				else
				{
					result = "те-ле-";
				}
			}
			return result;
		}

		public string IsAvto(string word)
		{
			word = word.ToLower();
			string result = "";
			if (word.Contains("авто"))
			{
				int index = word.IndexOf("авто");
				if (index == '0')
				{
					result = "ав-то";
				}
				else
				{
					result = "ав-то-";
				}
			}
			return result;
		}
	}
}
