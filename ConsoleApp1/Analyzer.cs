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
		//	Кириш ва чикиш файлларини очиш
		string FileName = "WordListin.txt";
		string OutFile = "WordListout.txt";
		public Analyzer(string w)
		{
			_word = w;
		}

		public void Analyze(string word)
		{
			if (!string.IsNullOrEmpty(word))
			{
				// Катор ва сузларни укиш параметрлари
				int[] posStr = new int[30];
				string fullword = "";
				string sword = word;
				string acword = "";
				int totalword = 0;
				// Каторни ва сузни тахлил килиш параметрлари (боши)------------------------
				int Symbol = word.Length;  // Катордаги белгилар сони
				int wordlen = 0;
				int unliall = 0;            // Унли харфлар сони
				int pos = 0;                // Бугинни жойини аниклаш
				int unli = 0;               // Унли харфларни санаш
				string[] strarr = new string[100];        // Сузлар массиви (сигими 100 та суз)
				int nword = 0;              // Сузлар хисоблагичи
				int probel = 1;             // Пробел мавжудлиги
				// Каторни ва сузни тахлил килиш параметрлари (охири)-----------------------
											// Суздаги унли харфлар сонини аниклаш - кодировка Unicode - UTF-8
				for (int i = 0; i < word.Length; i++)
				{
					if (word[i] == 'а' || //а
							word[i] == 'А' || //А
							word[i] == 'о' || //о
							word[i] == 'О' || //О
							word[i] == 1080 || //и
							word[i] == 1048 || //И
							word[i] == 'э' || //э
							word[i] == 'Э' || //Э
							word[i] == 1091 || //у
							word[i] == 1059 || //У
							word[i] == 1118 || //ў
							word[i] == 1038 || //Ў
							word[i] == 1077 || //е
							word[i] == 1045 || //Е
							word[i] == 1105 || //ё
							word[i] == 1025 || //Ё
							word[i] == 1102 || //ю
							word[i] == 1070 || //Ю
							word[i] == 1103 || //я
							word[i] == 1071)  //Я
					{
						posStr[i] = 0;
						unliall++;
					}
					else
					{
						if (word[i] == 1066 || word[i] == 1098) //Ъ ва ъ белгиларини аниклаш
						{
							posStr[i] = 2;
						}
						else
						{
							if (word[i] == 1068 || word[i] == 1100) //Ь ва ь белгиларини аниклаш
								posStr[i] = 3;
							else
								posStr[i] = 1;
						}
					}
					// агар суз бир бугиндан иборат булса
					if (unliall == 1)
					{
						using (StreamWriter sw = new StreamWriter(OutFile, false, Encoding.UTF8))
						{
							sw.WriteLine(sword);
						}
					}

					// Суз икки ва ундан куп бугиндан иборат булса (боши) --------------------------------------------------------------------------------
					else
					{
						for (int l = 0; l < unliall - 1; l++)
						{

							// "Аэро" сузини ажратиб олиш ---------------------------------------------------------------------------------------------
							if ((wordlen - pos > 3 && unliall - unli > 2) && ((int)sword[pos] == 'А' || (int)sword[pos] == 'а'))
							{
								if ((sword[pos + 0] == 'А' & sword[pos + 1] == 'Э' && sword[pos + 2] == 'Р' && sword[pos + 3] == 'О') ||
										(sword[pos + 0] == 'А' && sword[pos + 1] == 'э' && sword[pos + 2] == 'р' && sword[pos + 3] == 'о') ||
										(sword[pos + 0] == 'а' && sword[pos + 1] == 'э' && sword[pos + 2] == 'р' && sword[pos + 3] == 'о'))
								{
									if (wordlen == 4)
										fullword = fullword + sword[pos + 0] + "-" + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3];
									else
										fullword = fullword + sword[pos + 0] + "-" + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + "-";
									pos += 4;
									unli += 3;
									l += 3;
									if (wordlen - pos == 0)
									{
										using (StreamWriter sw = new StreamWriter(OutFile, false, Encoding.UTF8))
										{
											sw.WriteLine(fullword);
										}
										return;
									}
									if (unliall - unli == 1) goto lastsyl;
									//i++;
								}
							}
							/*
									// "Авиа" сузини ажратиб олиш ---------------------------------------------------------------------------------------------
										if ((wordlen-pos > 3 & unliall-unli > 2) & ((int)sword[pos] == 'А' || (int)sword[pos] == 'а'))
										{
										if (((int)sword[pos+0] == 'А' & (int)sword[pos+1] == 'Э' & (int)sword[pos+2] == 'Р' & (int)sword[pos+3] == 'О' ) ||
											((int)sword[pos+0] == 'А' & (int)sword[pos+1] == 'э' & (int)sword[pos+2] == 'р' & (int)sword[pos+3] == 'о' ) ||
											((int)sword[pos+0] == 'а' & (int)sword[pos+1] == 'э' & (int)sword[pos+2] == 'р' & (int)sword[pos+3] == 'о' ))
											{
												if (wordlen == 4)
													fullword = fullword + sword[pos+0] + "-" + sword[pos+1] + "-" + sword[pos+2] + sword[pos+3];
												else
													fullword = fullword + sword[pos+0] + "-" + sword[pos+1] + "-" + sword[pos+2] + sword[pos+3] + "-";
												pos = pos+4;
												unli = unli + 3;
												i = i + 3;
												if (wordlen-pos == 0)
												{
													fileout << fullword << endl;
													goto endword;
												}
												if (unliall-unli == 1) goto lastsyl;
												//i++;
											}
										}
							*/

							// "Фото" ва "Фотон" сузларини ажратиб олиш-----------------------------------------------------------------------------------------------
							if ((wordlen - pos > 3 & unliall - unli > 1) & ((int)sword[pos] == 1060 || (int)sword[pos] == 1092))
							{
								if (((int)sword[pos + 0] == 1060 & (int)sword[pos + 1] == 'О' & (int)sword[pos + 2] == 1058 & (int)sword[pos + 3] == 'О') ||
										((int)sword[pos + 0] == 1060 & (int)sword[pos + 1] == 'о' & (int)sword[pos + 2] == 1090 & (int)sword[pos + 3] == 'о') ||
										((int)sword[pos + 0] == 1092 & (int)sword[pos + 1] == 'о' & (int)sword[pos + 2] == 1090 & (int)sword[pos + 3] == 'о'))
								{
									if (wordlen == 4)
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3];
										pos = pos + 4;
									}
									else
									{
										if (wordlen == 5)
										{
											fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + sword[pos + 4];
											pos = pos + 5;
										}
										else
										{
											if (((int)sword[pos + 4] == 1053 || (int)sword[pos + 4] == 1085) & posStr[pos + 5] == 1)
											{
												fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + "-";
												pos = pos + 5;
											}
											else
											{
												fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + "-";
												pos = pos + 4;
											}
										}
									}

									unli = unli + 2;
									l = l + 2;

									if (wordlen - pos == 0)
									{
										using (StreamWriter sw = new StreamWriter(OutFile, false, Encoding.UTF8))
										{
											sw.WriteLine(fullword);
										}
										return 0;
										//fileout << fullword << endl;
										//goto endword;
									}
									if (unliall - unli == 1) goto lastsyl;
								}
							}

							// "Теле" сузини ажратиб олиш ---------------------------------------------------------------------------------------------
							if ((wordlen - pos > 3 & unliall - unli > 1) & ((int)sword[pos] == 1058 || (int)sword[pos] == 1090))
							{
								if (((int)sword[pos + 0] == 1058 || (int)sword[pos + 0] == 1090) & // Т ёки т
										((int)sword[pos + 1] == 1045 || (int)sword[pos + 1] == 1077) & // Е ёки е
										((int)sword[pos + 2] == 1051 || (int)sword[pos + 2] == 1083) & // Л ёки л
										((int)sword[pos + 3] == 1045 || (int)sword[pos + 3] == 1077)) // Е ёки е
								{
									if (wordlen == 4)
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3];
									else
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + "-";
									pos = pos + 4;
									unli = unli + 2;
									l = l + 2; // бугинлар сони
									if (wordlen - pos == 0)
									{
										using (StreamWriter sw = new StreamWriter(OutFile, false, Encoding.UTF8))
										{
											sw.WriteLine(fullword);
										}
										return 0;
										//fileout << fullword << endl;
										//goto endword;
									}
									if (unliall - unli == 1) goto lastsyl;
									//i++;
								}
							}


							// "Авто" ва "Автор" сузларини ажратиб олиш-----------------------------------------------------------------------------------------------
							if ((wordlen - pos > 3 & unliall - unli > 1) & ((int)sword[pos] == 'А' || (int)sword[pos] == 'а'))
							{
								if (((int)sword[pos + 0] == 'А' || (int)sword[pos + 0] == 'а') & // А ёки а
										((int)sword[pos + 1] == 1042 || (int)sword[pos + 1] == 1074) & // В ёки в
										((int)sword[pos + 2] == 1058 || (int)sword[pos + 2] == 1090) & // Т ёки т
										((int)sword[pos + 3] == 'О' || (int)sword[pos + 3] == 'о')) // О ёки о
								{
									if (wordlen == 4)
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3];
										pos = pos + 4;
									}
									else
									{
										if (wordlen == 5)
										{
											fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + sword[pos + 4];
											pos = pos + 5;
										}
										else
										{
											if ((((int)sword[pos + 4] == 'Р' || (int)sword[pos + 4] == 'р') ||                    // Р ёки р (авто-р)
													((int)sword[pos + 4] == 1051 || (int)sword[pos + 4] == 1083)) & posStr[pos + 5] == 1) // Л ёки л (авто-л)
											{
												fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + "-";
												pos = pos + 5;
											}
											else
											{
												fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + "-";
												pos = pos + 4;
											}
										}
									}

									unli = unli + 2;
									l = l + 2;

									if (wordlen - pos == 0)
									{
										using (StreamWriter sw = new StreamWriter(OutFile, false, Encoding.UTF8))
										{
											sw.WriteLine(fullword);
										}
										//fileout << fullword << endl;
										//goto endword;
									}
									if (unliall - unli == 1) goto lastsyl;
								}
							}

							// охири "ё" билан тугаган сузлар билан ишлаш
							if ((unliall - unli == 2) & ((int)sword[wordlen - 1] == 1025 || (int)sword[wordlen - 1] == 1105)) // 2 та унли ва охири "ё" булса
							{
								for (int j = pos; j < wordlen - 1; j++) fullword = fullword + sword[j];
								fullword = fullword + "-" + sword[wordlen - 1];
								using (StreamWriter sw = new StreamWriter(OutFile, false, Encoding.UTF8))
								{
									sw.WriteLine(fullword);
								}
								//fileout << fullword << endl;
								goto wordfin;
							}

							// Агар суз унли харфдан бошланса------------------------------------------------------------------------------------
							if (posStr[pos] == 0)
							{
								if (wordlen - pos > 4) // агар суз узунлиги 4 харфдан катта булса
								{
									// 0111+0 холатни аниклаш
									if (posStr[pos + 0] == 0 & posStr[pos + 1] == 1 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1 & posStr[pos + 4] == 0) //гссс+г
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //011 ни олиш (авжланмок, антракт)
										pos = pos + 3;
										unli++;
										goto endunli;
									}
								}

								if (wordlen - pos > 3) // агар суз узунлиги 3 харфдан катта булса
								{
									// 011+1 холатни аниклаш
									if (posStr[pos + 0] == 0 & posStr[pos + 1] == 1 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1) //гсс+с
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //011 ни олиш
										pos = pos + 3;
										unli++;
										goto endunli;
									}
									// 011+0 холатни аниклаш
									if (posStr[pos + 0] == 0 & posStr[pos + 1] == 1 & posStr[pos + 2] == 1 & posStr[pos + 3] == 0) //гсс+г
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-"; //01 ни олиш
										pos = pos + 2;
										unli++;
										goto endunli;
									}
									// 02 холатни аниклаш
									if (posStr[pos + 0] == 0 & posStr[pos + 1] == 2) //г+ъ
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-"; //02 ни олиш
										pos = pos + 2;
										unli++;
										goto endunli;
									}
									// 012 холатни аниклаш
									if (posStr[pos + 0] == 0 & posStr[pos + 1] == 1 & posStr[pos + 2] == 2) //гс+ъ
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //012 ни олиш
										pos = pos + 3;
										unli++;
										goto endunli;
									}
								}
								// агар суз узунлиги 3 харф ва ундан кичик булса
								{
									// 013 холатни аниклаш (альфа, ультра) юмшатиш белгили сузлар
									if (posStr[pos + 0] == 0 & posStr[pos + 1] == 1 & posStr[pos + 2] == 3) // гсь
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //013 ни олиш
										pos = pos + 3;
										unli++;
										goto endunli;
									}

									// 01+0 холатни аниклаш
									if (posStr[pos + 0] == 0 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0) //сг+с
									{
										if ((sword[pos + 1] == 1049 || sword[pos + 1] == 1081) & (sword[pos + 2] == 1025 || sword[pos + 2] == 1105)) // агар унлидан кейин "йё" булса
										{
											fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-"; //01 ни олиш (унли+й ни олиш) - ай-ёр, ай-ём
											pos = pos + 2;
											unli++;
											goto endunli;
										}
										else
										{
											fullword = fullword + sword[pos + 0] + "-"; //0 ни олиш (акс холда факат унлини олиш)
											pos = pos + 1;
											unli++;
											goto endunli;
										}
									}
									// 0+0 холатни аниклаш
									if (posStr[pos + 0] == 0 & posStr[pos + 1] == 0) //г+г
									{
										fullword = fullword + sword[pos + 0] + "-"; //0 ни олиш
										pos = pos + 1;
										unli++;
										goto endunli;
									}
								}
							endunli:;
							}

							// Агар суз ундош харфдан бошланса
							else
							{
								if (wordlen - pos > 5) // агар суз узунлиги 5 харфдан катта булса
								{
									// 'э'1+1 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0 & posStr[pos + 3] == 1 & posStr[pos + 4] == 1 & posStr[pos + 5] == 1) //ссгсс+с
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + "-"; //'э'1 ни олиш
										pos = pos + 5;
										unli++;
										goto endundosh;
									}
									// 'э'1+0 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0 & posStr[pos + 3] == 1 & posStr[pos + 4] == 1 & posStr[pos + 5] == 0) //ссгсс+с
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //'э' ни олиш
										pos = pos + 4;
										unli++;
										goto endundosh;
									}
									// 101311+1 холатни аниклаш (фильтрлаш, фильтрнинг), юмшатиш белгили сузлар
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 3 & posStr[pos + 4] == 1 & posStr[pos + 5] == 1 & posStr[pos + 6] == 1) //сгсьсс+с
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + sword[pos + 5] + "-"; //101311 ни олиш
										pos = pos + 6;
										unli++;
										goto endundosh;
									}

									// 10131+1 холатни аниклаш (мультфильм, фильмнинг), юмшатиш белгили сузлар
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 3 & posStr[pos + 4] == 1 & posStr[pos + 5] == 1) //сгсьс+с
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + "-"; //10131 ни олиш
										pos = pos + 5;
										unli++;
										goto endundosh;
									}

									// 10111+0 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1 & posStr[pos + 4] == 1 & posStr[pos + 5] == 0) //сгссс+г
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //101 ни олиш (кон-тракт)
										pos = pos + 3;
										unli++;
										goto endundosh;
									}
								}

								if (wordlen - pos > 4) // агар суз узунлиги 4 харфдан катта булса
								{
									// 'э'+1 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0 & posStr[pos + 3] == 1 & posStr[pos + 4] == 1) //ссгс+с
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //'э' ни олиш
										pos = pos + 4;
										unli++;
										goto endundosh;
									}
									// 'э'+0 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0 & posStr[pos + 3] == 1 & posStr[pos + 4] == 0) //ссгс+г
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //110 ни олиш
										pos = pos + 3;
										unli++;
										goto endundosh;
									}
									// 'э'+3 холатни аниклаш (статья)
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0 & posStr[pos + 3] == 1 & posStr[pos + 4] == 3) //ссгс+ь
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + "-"; //'э'3 ни олиш
										pos = pos + 5;
										unli++;
										goto endundosh;
									}

									// 1011+1 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1 & posStr[pos + 4] == 1) //сгсс+с
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //1011 ни олиш
										pos = pos + 4;
										unli++;
										goto endundosh;
									}
									// 1011+0 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1 & posStr[pos + 4] == 0) //сгсс+г
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //101 ни олиш
										pos = pos + 3;
										unli++;
										goto endundosh;
									}

									// 1011+3 холатни аниклаш (компьютер), юмшатиш белгили сузлар
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1 & posStr[pos + 4] == 3) //сгсс+ь
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + "-"; //1011+3 ни олиш
										pos = pos + 5;
										unli++;
										goto endundosh;
									}
									// 1013 холатни аниклаш (мульти, бальзам), юмшатиш белгили сузлар
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 3) //сгсь
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //1013 ни олиш
										pos = pos + 4;
										unli++;
										goto endundosh;
									}

									// Айриш белгиси катнашган сузларда бугин кучириш
									// 1201 холатни аниклаш (съём-ка)
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 2 & posStr[pos + 2] == 0 & posStr[pos + 3] == 1) //съгс
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //1201 ни олиш
										pos = pos + 4;
										unli++;
										goto endundosh;
									}
									// 1012 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 2) //сгсъ
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //1012 ни олиш
										pos = pos + 4;
										unli++;
										goto endundosh;
									}
									// 1021+1 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 2 & posStr[pos + 3] == 1 & posStr[pos + 4] == 1) //сгъс+с
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //1021 ни олиш
										pos = pos + 4;
										unli++;
										goto endundosh;
									}
									// 1021+0 холатни аниклаш
									if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 2 & posStr[pos + 3] == 1 & posStr[pos + 4] == 0) //сгъс+г
									{
										fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //102 ни олиш
										pos = pos + 3;
										unli++;
										goto endundosh;
									}
								}

								// 110 холатни аниклаш
								if (posStr[pos + 0] == 1 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0) //ссг кейинги товушни текшириш шарт эмас
								{
									fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //110 ни олиш
									pos = pos + 3;
									unli++;
									goto endundosh;
								}

								// 101+1 холатни аниклаш
								if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1) //сгс+с
								{
									fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //101 ни олиш
									pos = pos + 3;
									unli++;
									goto endundosh;
								}

								// 101+0 холатни аниклаш
								if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 0) //сгс+с
								{
									fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-"; //10 ни олиш
									pos = pos + 2;
									unli++;
									goto endundosh;
								}

								// 102+0 холатни аниклаш
								if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 2 & posStr[pos + 3] == 0) //сгъ
								{
									fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //102 ни олиш
									pos = pos + 3;
									unli++;
									goto endundosh;
								}

								// 10 холатни аниклаш
								if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0) //сг кейинги товушни текшириш шарт эмас
								{
									fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-"; //10 ни олиш
									pos = pos + 2;
									unli++;
									goto endundosh;
								}

							endundosh:;
							}

							// Охирги бугинни аниклаш ва олиш
							if ((unliall - unli) == 1)
							{
							lastsyl:
								for (int j = pos; j < wordlen; j++) fullword = fullword + sword[j];
								fileout << fullword << endl;
								goto wordfin;
							}
						}  // Бугинларни санаш ва шакллантириш цикли (охири) -------------------------------------------------
					wordfin:;
					} // Суз икки ва ундан куп бугиндан иборат булса шарти (охири) -------------------------------------------
				}
			}
		}

		public string IsAero(string word)
		{
			word = word.ToLower();
			string result = "";
			if (word.Contains("аэро"))
			{
				int index = word.IndexOf("аэро");
				if (index == 0)
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

		int main()
		{
			// Катор ва сузларни укиш параметрлари
			int[] posStr = new int[30];
			string fullword = "";
			string sword;
			string acword = "";
			int totalword = 0;

			//	Кириш ва чикиш файлларини очиш
			//string FileName = "WordListin.txt";
			//string OutFile = "WordListout.txt";

			if (File.Exists(FileName) || File.Exists(OutFile))  // Файл очилганлигини текшириш
			{
				Console.WriteLine($"Файл мавжуд эмас? {0}", FileName);
				return 1;
			}
			else
			{
				//using (var fs = File.OpenText(FileName))
				//{
				//	_word = fs.ReadToEnd();
				//}
				_word = Console.ReadLine();
				if (!string.IsNullOrEmpty(_word))
				{
					var words = _word.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
					// Каторлардаги сузларни катор охиригача навбатма-навбат укиш
					foreach(string w in words)
					{
						if (!string.IsNullOrEmpty(w)) // агар катор буш булмаса
						{
							totalword++;

							// Каторни ва сузни тахлил килиш параметрлари (боши)------------------------
							int Symbol = w.Length;  // Катордаги белгилар сони
							int wordlen = 0;
							int unliall = 0;            // Унли харфлар сони
							int pos = 0;                // Бугинни жойини аниклаш
							int unli = 0;               // Унли харфларни санаш
							string[] strarr = new string[100];        // Сузлар массиви (сигими 100 та суз)
							int nword = 0;              // Сузлар хисоблагичи
							int probel = 1;             // Пробел мавжудлиги
														// Каторни ва сузни тахлил килиш параметрлари (охири)-----------------------

							//	Катордаги гапларни сузларга ажратиш (боши)-------------------------------------------------------------
							for (int i = 0; i < Symbol; i++)
							{
								if ((int)w[i] != 65279) // UTF-8 ни англатувчи BOM белгини олиб ташлаш
								{
									if ((int)w[i] != 45 & (int)w[i] != 32) //Чизикча "-" ва пробелга " " тенг булмаса
									{
										acword = acword + w[i]; // Харфлардан сузни йигиш
										probel = 0;
									}
									else
									{
										if (probel != 1)  // агар олдинги белги пробел ёки чизикча булмаса
										{
											strarr[nword] = acword; // сузни массивга ёзиш
											acword = "";           // узгарувчини бушатиш
											nword++;
										}
										probel = 1;
									}
								}
							}
							if (probel != 1)
							{
								strarr[nword] = acword;
								acword = "";
							}
							else nword--;
							// Катордаги гапни сузларга ажратиш (охири) --------------------------------------------------------------

							// Сузларни тахлил килиш учун массивдан олиш (боши)--------------------
							for (int i = 0; i < nword + 1; i++) //nword+1
							{
								pos = 0;
								unliall = 0;
								unli = 0;
								sword = strarr[i];
								wordlen = sword.Length;

								// Суздаги унли харфлар сонини аниклаш - кодировка Unicode - UTF-8
								for (int j = 0; j < wordlen; j++)
								{
									if (sword[j] == 'а' || //а
											sword[j] == 'А' || //А
											sword[j] == 'о' || //о
											sword[j] == 'О' || //О
											sword[j] == 'и' || //и
											sword[j] == 'И' || //И
											sword[j] == 'э' || //э
											sword[j] == 'Э' || //Э
											sword[j] == 'у' || //у
											sword[j] == 'У' || //У
											sword[j] == 'ў' || //ў
											sword[j] == 'Ў' || //Ў
											sword[j] == 'е' || //е
											sword[j] == 'Е' || //Е
											sword[j] == 'ё' || //ё
											sword[j] == 'Ё' || //Ё
											sword[j] == 'ю' || //ю
											sword[j] == 'Ю' || //Ю
											sword[j] == 'я' || //я
											sword[j] == 'Я')  //Я
									{
										posStr[j] = 0;
										unliall++;
									}
									else
									{
										if (sword[j] == 'Ъ' || sword[j] == 'ъ') //Ъ ва ъ белгиларини аниклаш
										{
											posStr[j] = 2;
										}
										else
										{
											if (sword[j] == 'ь' || sword[j] == 'Ь') //Ь ва ь белгиларини аниклаш
												posStr[j] = 3;
											else
												posStr[j] = 1;
										}
									}
								}

								// агар суз бир бугиндан иборат булса
								if (unliall == 1) 
								{
									using (StreamWriter sw = new StreamWriter(OutFile, false, Encoding.UTF8))
									{
										sw.WriteLine(sword);
									}
								}

								// Суз икки ва ундан куп бугиндан иборат булса (боши) --------------------------------------------------------------------------------
								else
								{
									for (int l = 0; l < unliall - 1; l++)
									{

										// "Аэро" сузини ажратиб олиш ---------------------------------------------------------------------------------------------
										if ((wordlen - pos > 3 && unliall - unli > 2) && (sword[pos] == 'А' || sword[pos] == 'а'))
										{
											if ((sword[pos + 0] == 'А' & sword[pos + 1] == 'Э' && sword[pos + 2] == 'Р' && sword[pos + 3] == 'О') ||
													(sword[pos + 0] == 'А' && sword[pos + 1] == 'э' && sword[pos + 2] == 'р' && sword[pos + 3] == 'о') ||
													(sword[pos + 0] == 'а' && sword[pos + 1] == 'э' && sword[pos + 2] == 'р' && sword[pos + 3] == 'о'))
											{
												if (wordlen == 4)
													fullword = fullword + sword[pos + 0] + "-" + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3];
												else
													fullword = fullword + sword[pos + 0] + "-" + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + "-";
												pos += 4;
												unli += 3;
												l += 3;
												if (wordlen - pos == 0)
												{
													using (StreamWriter sw = new StreamWriter(OutFile, false, Encoding.UTF8))
													{
														sw.WriteLine(fullword);
													}
													return 0;
												}
												if (unliall - unli == 1) goto lastsyl;
												//i++;
											}
										}
										/*
												// "Авиа" сузини ажратиб олиш ---------------------------------------------------------------------------------------------
													if ((wordlen-pos > 3 & unliall-unli > 2) & ((int)sword[pos] == 'А' || (int)sword[pos] == 'а'))
													{
													if (((int)sword[pos+0] == 'А' & (int)sword[pos+1] == 'Э' & (int)sword[pos+2] == 'Р' & (int)sword[pos+3] == 'О' ) ||
														((int)sword[pos+0] == 'А' & (int)sword[pos+1] == 'э' & (int)sword[pos+2] == 'р' & (int)sword[pos+3] == 'о' ) ||
														((int)sword[pos+0] == 'а' & (int)sword[pos+1] == 'э' & (int)sword[pos+2] == 'р' & (int)sword[pos+3] == 'о' ))
														{
															if (wordlen == 4)
																fullword = fullword + sword[pos+0] + "-" + sword[pos+1] + "-" + sword[pos+2] + sword[pos+3];
															else
																fullword = fullword + sword[pos+0] + "-" + sword[pos+1] + "-" + sword[pos+2] + sword[pos+3] + "-";
															pos = pos+4;
															unli = unli + 3;
															i = i + 3;
															if (wordlen-pos == 0)
															{
																fileout << fullword << endl;
																goto endword;
															}
															if (unliall-unli == 1) goto lastsyl;
															//i++;
														}
													}
										*/

										// "Фото" ва "Фотон" сузларини ажратиб олиш-----------------------------------------------------------------------------------------------
										if ((wordlen - pos > 3 & unliall - unli > 1) & ((int)sword[pos] == 1060 || (int)sword[pos] == 1092))
										{
											if (((int)sword[pos + 0] == 1060 & (int)sword[pos + 1] == 'О' & (int)sword[pos + 2] == 1058 & (int)sword[pos + 3] == 'О') ||
													((int)sword[pos + 0] == 1060 & (int)sword[pos + 1] == 'о' & (int)sword[pos + 2] == 1090 & (int)sword[pos + 3] == 'о') ||
													((int)sword[pos + 0] == 1092 & (int)sword[pos + 1] == 'о' & (int)sword[pos + 2] == 1090 & (int)sword[pos + 3] == 'о'))
											{
												if (wordlen == 4)
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3];
													pos = pos + 4;
												}
												else
												{
													if (wordlen == 5)
													{
														fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + sword[pos + 4];
														pos = pos + 5;
													}
													else
													{
														if (((int)sword[pos + 4] == 1053 || (int)sword[pos + 4] == 1085) & posStr[pos + 5] == 1)
														{
															fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + "-";
															pos = pos + 5;
														}
														else
														{
															fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + "-";
															pos = pos + 4;
														}
													}
												}

												unli = unli + 2;
												l = l + 2;

												if (wordlen - pos == 0)
												{
													using (StreamWriter sw = new StreamWriter(OutFile, false, Encoding.UTF8))
													{
														sw.WriteLine(fullword);
													}
													return 0;
													//fileout << fullword << endl;
													//goto endword;
												}
												if (unliall - unli == 1) goto lastsyl;
											}
										}

										// "Теле" сузини ажратиб олиш ---------------------------------------------------------------------------------------------
										if ((wordlen - pos > 3 & unliall - unli > 1) & ((int)sword[pos] == 1058 || (int)sword[pos] == 1090))
										{
											if (((int)sword[pos + 0] == 1058 || (int)sword[pos + 0] == 1090) & // Т ёки т
													((int)sword[pos + 1] == 1045 || (int)sword[pos + 1] == 1077) & // Е ёки е
													((int)sword[pos + 2] == 1051 || (int)sword[pos + 2] == 1083) & // Л ёки л
													((int)sword[pos + 3] == 1045 || (int)sword[pos + 3] == 1077)) // Е ёки е
											{
												if (wordlen == 4)
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3];
									else
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + "-";
												pos = pos + 4;
												unli = unli + 2;
												l = l + 2; // бугинлар сони
												if (wordlen - pos == 0)
												{
													using (StreamWriter sw = new StreamWriter(OutFile, false, Encoding.UTF8))
													{
														sw.WriteLine(fullword);
													}
													return 0;
													//fileout << fullword << endl;
													//goto endword;
												}
												if (unliall - unli == 1) goto lastsyl;
												//i++;
											}
										}


										// "Авто" ва "Автор" сузларини ажратиб олиш-----------------------------------------------------------------------------------------------
										if ((wordlen - pos > 3 & unliall - unli > 1) & ((int)sword[pos] == 'А' || (int)sword[pos] == 'а'))
										{
											if (((int)sword[pos + 0] == 'А' || (int)sword[pos + 0] == 'а') & // А ёки а
													((int)sword[pos + 1] == 1042 || (int)sword[pos + 1] == 1074) & // В ёки в
													((int)sword[pos + 2] == 1058 || (int)sword[pos + 2] == 1090) & // Т ёки т
													((int)sword[pos + 3] == 'О' || (int)sword[pos + 3] == 'о')) // О ёки о
											{
												if (wordlen == 4)
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3];
													pos = pos + 4;
												}
												else
												{
													if (wordlen == 5)
													{
														fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + sword[pos + 4];
														pos = pos + 5;
													}
													else
													{
														if ((((int)sword[pos + 4] == 'Р' || (int)sword[pos + 4] == 'р') ||                    // Р ёки р (авто-р)
																((int)sword[pos + 4] == 1051 || (int)sword[pos + 4] == 1083)) & posStr[pos + 5] == 1) // Л ёки л (авто-л)
														{
															fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + "-";
															pos = pos + 5;
														}
														else
														{
															fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-" + sword[pos + 2] + sword[pos + 3] + "-";
															pos = pos + 4;
														}
													}
												}

												unli = unli + 2;
												l = l + 2;

												if (wordlen - pos == 0)
												{
													using (StreamWriter sw = new StreamWriter(OutFile, false, Encoding.UTF8))
													{
														sw.WriteLine(fullword);
													}
													//fileout << fullword << endl;
													//goto endword;
												}
												if (unliall - unli == 1) goto lastsyl;
											}
										}

										// охири "ё" билан тугаган сузлар билан ишлаш
										if ((unliall - unli == 2) & ((int)sword[wordlen - 1] == 1025 || (int)sword[wordlen - 1] == 1105)) // 2 та унли ва охири "ё" булса
										{
											for (int j = pos; j < wordlen - 1; j++) fullword = fullword + sword[j];
											fullword = fullword + "-" + sword[wordlen - 1];
											using (StreamWriter sw = new StreamWriter(OutFile, false, Encoding.UTF8))
											{
												sw.WriteLine(fullword);
											}
											//fileout << fullword << endl;
											goto wordfin;
										}

										// Агар суз унли харфдан бошланса------------------------------------------------------------------------------------
										if (posStr[pos] == 0)
										{
											if (wordlen - pos > 4) // агар суз узунлиги 4 харфдан катта булса
											{
												// 0111+0 холатни аниклаш
												if (posStr[pos + 0] == 0 & posStr[pos + 1] == 1 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1 & posStr[pos + 4] == 0) //гссс+г
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //011 ни олиш (авжланмок, антракт)
													pos = pos + 3;
													unli++;
													goto endunli;
												}
											}

											if (wordlen - pos > 3) // агар суз узунлиги 3 харфдан катта булса
											{
												// 011+1 холатни аниклаш
												if (posStr[pos + 0] == 0 & posStr[pos + 1] == 1 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1) //гсс+с
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //011 ни олиш
													pos = pos + 3;
													unli++;
													goto endunli;
												}
												// 011+0 холатни аниклаш
												if (posStr[pos + 0] == 0 & posStr[pos + 1] == 1 & posStr[pos + 2] == 1 & posStr[pos + 3] == 0) //гсс+г
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-"; //01 ни олиш
													pos = pos + 2;
													unli++;
													goto endunli;
												}
												// 02 холатни аниклаш
												if (posStr[pos + 0] == 0 & posStr[pos + 1] == 2) //г+ъ
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-"; //02 ни олиш
													pos = pos + 2;
													unli++;
													goto endunli;
												}
												// 012 холатни аниклаш
												if (posStr[pos + 0] == 0 & posStr[pos + 1] == 1 & posStr[pos + 2] == 2) //гс+ъ
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //012 ни олиш
													pos = pos + 3;
													unli++;
													goto endunli;
												}
											}
											// агар суз узунлиги 3 харф ва ундан кичик булса
											{
												// 013 холатни аниклаш (альфа, ультра) юмшатиш белгили сузлар
												if (posStr[pos + 0] == 0 & posStr[pos + 1] == 1 & posStr[pos + 2] == 3) // гсь
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //013 ни олиш
													pos = pos + 3;
													unli++;
													goto endunli;
												}

												// 01+0 холатни аниклаш
												if (posStr[pos + 0] == 0 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0) //сг+с
												{
													if ((sword[pos + 1] == 1049 || sword[pos + 1] == 1081) & (sword[pos + 2] == 1025 || sword[pos + 2] == 1105)) // агар унлидан кейин "йё" булса
													{
														fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-"; //01 ни олиш (унли+й ни олиш) - ай-ёр, ай-ём
														pos = pos + 2;
														unli++;
														goto endunli;
													}
													else
													{
														fullword = fullword + sword[pos + 0] + "-"; //0 ни олиш (акс холда факат унлини олиш)
														pos = pos + 1;
														unli++;
														goto endunli;
													}
												}
												// 0+0 холатни аниклаш
												if (posStr[pos + 0] == 0 & posStr[pos + 1] == 0) //г+г
												{
													fullword = fullword + sword[pos + 0] + "-"; //0 ни олиш
													pos = pos + 1;
													unli++;
													goto endunli;
												}
											}
										endunli:;
										}

										// Агар суз ундош харфдан бошланса
										else
										{
											if (wordlen - pos > 5) // агар суз узунлиги 5 харфдан катта булса
											{
												// 'э'1+1 холатни аниклаш
												if (posStr[pos + 0] == 1 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0 & posStr[pos + 3] == 1 & posStr[pos + 4] == 1 & posStr[pos + 5] == 1) //ссгсс+с
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + "-"; //'э'1 ни олиш
													pos = pos + 5;
													unli++;
													goto endundosh;
												}
												// 'э'1+0 холатни аниклаш
												if (posStr[pos + 0] == 1 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0 & posStr[pos + 3] == 1 & posStr[pos + 4] == 1 & posStr[pos + 5] == 0) //ссгсс+с
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //'э' ни олиш
													pos = pos + 4;
													unli++;
													goto endundosh;
												}
												// 101311+1 холатни аниклаш (фильтрлаш, фильтрнинг), юмшатиш белгили сузлар
												if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 3 & posStr[pos + 4] == 1 & posStr[pos + 5] == 1 & posStr[pos + 6] == 1) //сгсьсс+с
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + sword[pos + 5] + "-"; //101311 ни олиш
													pos = pos + 6;
													unli++;
													goto endundosh;
												}

												// 10131+1 холатни аниклаш (мультфильм, фильмнинг), юмшатиш белгили сузлар
												if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 3 & posStr[pos + 4] == 1 & posStr[pos + 5] == 1) //сгсьс+с
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + "-"; //10131 ни олиш
													pos = pos + 5;
													unli++;
													goto endundosh;
												}

												// 10111+0 холатни аниклаш
												if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1 & posStr[pos + 4] == 1 & posStr[pos + 5] == 0) //сгссс+г
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //101 ни олиш (кон-тракт)
													pos = pos + 3;
													unli++;
													goto endundosh;
												}
											}

											if (wordlen - pos > 4) // агар суз узунлиги 4 харфдан катта булса
											{
												// 'э'+1 холатни аниклаш
												if (posStr[pos + 0] == 1 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0 & posStr[pos + 3] == 1 & posStr[pos + 4] == 1) //ссгс+с
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //'э' ни олиш
													pos = pos + 4;
													unli++;
													goto endundosh;
												}
												// 'э'+0 холатни аниклаш
												if (posStr[pos + 0] == 1 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0 & posStr[pos + 3] == 1 & posStr[pos + 4] == 0) //ссгс+г
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //110 ни олиш
													pos = pos + 3;
													unli++;
													goto endundosh;
												}
												// 'э'+3 холатни аниклаш (статья)
												if (posStr[pos + 0] == 1 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0 & posStr[pos + 3] == 1 & posStr[pos + 4] == 3) //ссгс+ь
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + "-"; //'э'3 ни олиш
													pos = pos + 5;
													unli++;
													goto endundosh;
												}

												// 1011+1 холатни аниклаш
												if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1 & posStr[pos + 4] == 1) //сгсс+с
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //1011 ни олиш
													pos = pos + 4;
													unli++;
													goto endundosh;
												}
												// 1011+0 холатни аниклаш
												if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1 & posStr[pos + 4] == 0) //сгсс+г
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //101 ни олиш
													pos = pos + 3;
													unli++;
													goto endundosh;
												}

												// 1011+3 холатни аниклаш (компьютер), юмшатиш белгили сузлар
												if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1 & posStr[pos + 4] == 3) //сгсс+ь
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + sword[pos + 4] + "-"; //1011+3 ни олиш
													pos = pos + 5;
													unli++;
													goto endundosh;
												}
												// 1013 холатни аниклаш (мульти, бальзам), юмшатиш белгили сузлар
												if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 3) //сгсь
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //1013 ни олиш
													pos = pos + 4;
													unli++;
													goto endundosh;
												}

												// Айриш белгиси катнашган сузларда бугин кучириш
												// 1201 холатни аниклаш (съём-ка)
												if (posStr[pos + 0] == 1 & posStr[pos + 1] == 2 & posStr[pos + 2] == 0 & posStr[pos + 3] == 1) //съгс
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //1201 ни олиш
													pos = pos + 4;
													unli++;
													goto endundosh;
												}
												// 1012 холатни аниклаш
												if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 2) //сгсъ
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //1012 ни олиш
													pos = pos + 4;
													unli++;
													goto endundosh;
												}
												// 1021+1 холатни аниклаш
												if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 2 & posStr[pos + 3] == 1 & posStr[pos + 4] == 1) //сгъс+с
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + sword[pos + 3] + "-"; //1021 ни олиш
													pos = pos + 4;
													unli++;
													goto endundosh;
												}
												// 1021+0 холатни аниклаш
												if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 2 & posStr[pos + 3] == 1 & posStr[pos + 4] == 0) //сгъс+г
												{
													fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //102 ни олиш
													pos = pos + 3;
													unli++;
													goto endundosh;
												}
											}

											// 110 холатни аниклаш
											if (posStr[pos + 0] == 1 & posStr[pos + 1] == 1 & posStr[pos + 2] == 0) //ссг кейинги товушни текшириш шарт эмас
											{
												fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //110 ни олиш
												pos = pos + 3;
												unli++;
												goto endundosh;
											}

											// 101+1 холатни аниклаш
											if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 1) //сгс+с
											{
												fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //101 ни олиш
												pos = pos + 3;
												unli++;
												goto endundosh;
											}

											// 101+0 холатни аниклаш
											if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 1 & posStr[pos + 3] == 0) //сгс+с
											{
												fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-"; //10 ни олиш
												pos = pos + 2;
												unli++;
												goto endundosh;
											}

											// 102+0 холатни аниклаш
											if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0 & posStr[pos + 2] == 2 & posStr[pos + 3] == 0) //сгъ
											{
												fullword = fullword + sword[pos + 0] + sword[pos + 1] + sword[pos + 2] + "-"; //102 ни олиш
												pos = pos + 3;
												unli++;
												goto endundosh;
											}

											// 10 холатни аниклаш
											if (posStr[pos + 0] == 1 & posStr[pos + 1] == 0) //сг кейинги товушни текшириш шарт эмас
											{
												fullword = fullword + sword[pos + 0] + sword[pos + 1] + "-"; //10 ни олиш
												pos = pos + 2;
												unli++;
												goto endundosh;
											}

										endundosh:;
										}

										// Охирги бугинни аниклаш ва олиш
										if ((unliall - unli) == 1)
										{
										lastsyl:
											for (int j = pos; j < wordlen; j++) fullword = fullword + sword[j];
											fileout << fullword << endl;
											goto wordfin;
										}
									}  // Бугинларни санаш ва шакллантириш цикли (охири) -------------------------------------------------
								wordfin:;
								} // Суз икки ва ундан куп бугиндан иборат булса шарти (охири) -------------------------------------------
							endword:
								sword = "";
								fullword = "";
							} // Укилган катордаги сузларни танлаш for цикли охири----------------------

						} // Укилган катор бушлигини текшириш if(str!="") шарти охири---------------
						w = "";
					} // Католарни укиш while(getline) фукцияси охири---------------------------
				}
				
			}
				

			//cout << "Жами сузлар: " << totalword << "\n\n";
			Console.WriteLine($"Жами сузлар: {0}", totalword);
			//filein.close();
			//fileout.close();

			//system("Pause");
			Console.ReadKey();
			return 0;
		} // Main процедураси охири ------------------------------------------------
	}
}
