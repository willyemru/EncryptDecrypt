using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

namespace Decryptor
{
    class Decryptor
    {

        const string TEXTFILE = "../../../encripedtext.txt";
        // <summary>
        /// Класс который содержит символ и его порядковый номер в строке, зависящий от алфавита.
        /// </summary>
        class CharNum
        {
            #region Fields
            /// <summary>
            /// Символ.
            /// </summary>
            private char _ch;
            /// <summary>
            /// Порядковый номер зависящий от алфавита.
            /// </summary>
            private int _numberInWord;
            #endregion Fieds

            #region Properties
            /// <summary>
            /// Символ.
            /// </summary>
            public char Ch
            {
                get { return _ch; }
                set
                {
                    if (_ch == value)
                        return;
                    _ch = value;
                }
            }
            /// <summary>
            /// Порядковый номер в строке, зависящий от алфавита.
            /// </summary>
            public int NumberInWord
            {
                get { return _numberInWord; }
                set
                {
                    if (_numberInWord == value)
                        return;
                    _numberInWord = value;
                }
            }
            #endregion Properties
        }

  /*      public struct Char_Num
        {
            char Ch;
            int NumberInWord;
        }
  */

        class Program
        {
            public static string RSAEncryptString(string textBox1, string textBox2, string textBox3)
            {
                BigInteger result = new BigInteger();
                int M = Convert.ToInt32(textBox1);
                int E = Convert.ToInt32(textBox2);
                int N = Convert.ToInt32(textBox3);

            https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger.modpow?view=net-5.0
                result = BigInteger.ModPow(M, E, N);
                return (Convert.ToString(result));


            }


            public static int GetNumberInThealphabet(char s)
            {
                string str = @"00112233445566778899AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZzАаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя";

                return (str.IndexOf(s) / 2);


                //   return number;
            }



            public static List<CharNum> FillListKey(char[] chars)
            {
                List<CharNum> listKey = new List<CharNum>(chars.Length);

                for (int i = 0; i < chars.Length; i++)
                {
                    CharNum charNum = new CharNum()
                    {
                        Ch = chars[i],
                        NumberInWord = GetNumberInThealphabet(chars[i])

                    };
                    //         Console.WriteLine(charNum.Ch + " " + charNum.NumberInWord);
                    listKey.Add(charNum);
                }
                return listKey;
            }

            public static List<CharNum> FillingSerialsNumber(
                List<CharNum> listCharNum)
            {
                int count = 0;

                var result = listCharNum.OrderBy(a =>
                    a.NumberInWord);

                foreach (var i in result)
                {
                    //       Console.WriteLine(i.Ch+" " +i.NumberInWord);
                    i.NumberInWord = count++;
                    Console.WriteLine(i.Ch + " " + i.NumberInWord);

                }

                return listCharNum;
            }

            
            public static void Pause(string s)
            {
                if (s == "") s = "Нажмите клавишу для продолжения";
                Console.WriteLine(s);
                Console.ReadKey();
            }

            static void Main(string[] args)
            {


                // int p = Convert.ToInt32(textBox1.Text);
                // int q = Convert.ToInt32(textBox2.Text);

                // http://altaev-aa.narod.ru/security/Rsa.html



                //File.WriteAllBytes(@"public.txt", E.ToByteArray());


                // Первый ключ, количество столбцов
                string firstKey = "ПАРОЛЬ";
                // Второй ключ, количество строк
                string secondKey = "312";
                
                // Предложение расшифрованное
                string stringUser = "";

                // Предложение шифрованное
                string EncryptedstringUser;

                // открываем файл с зашифрованным сообщением
                if (!File.Exists(TEXTFILE))
                { Console.WriteLine("File " + TEXTFILE + " is not found"); return; }
                else 
                {
                    using (StreamReader sr = File.OpenText(TEXTFILE))
                    {
                        EncryptedstringUser = sr.ReadLine();                // в первой же строке сообщение
                        Console.WriteLine("Зашифрованное сообщение: "+ EncryptedstringUser);
                        
                    };
                };

                Pause("");

                // EncryptedstringUser = "Ина в лчиИь араМжа";

                int Length1 = firstKey.Length;
                int Length2 = secondKey.Length;

                // Матрица в которой производим шифрование
                char[,] matrix = new char[Length2, Length1];
                char[,] matrix2 = new char[Length2, Length1];

                // Счетчик символов в строке
                int countSymbols = 0;

                // Переводим строки в массивы типа char
                char[] charsFirstKey = firstKey.ToCharArray();
                char[] charsSecondKey = secondKey.ToCharArray();
                char[] charStringUser = EncryptedstringUser.ToCharArray();

                // Создаем списки в которых будут храниться символы и порядковы номера символов
                List<CharNum> listCharNumFirst = new List<CharNum>(Length1);

                List<CharNum> listCharNumSecond = new List<CharNum>(Length2);

                // Заполняем символами из ключей
                listCharNumFirst = FillListKey(charsFirstKey);
                listCharNumSecond = FillListKey(charsSecondKey);
                // Заполняем порядковыми номерами
                listCharNumFirst = FillingSerialsNumber(listCharNumFirst);
                listCharNumSecond = FillingSerialsNumber(listCharNumSecond);


                Console.WriteLine("Первый ключ: " + firstKey);
                Console.WriteLine("Второй ключ: " + secondKey);

                Console.WriteLine();
                Console.WriteLine("Первоначальное значение:");

                // Заполнение матрицы зашифрованной строкой пользователя

                for (int i = 0; i < Length2; i++)
                {
                    for (int j = 0; j < Length1; j++)
                    {
                        matrix[i, j] = EncryptedstringUser[countSymbols++];
                        Console.Write(matrix[i, j] + " ");
                    }
                    Console.WriteLine();
                }


                // Заполнение матрицы с учетом шифрования. 
                // Переставляем столбцы по порядку следования в первом ключе. 
                // Затем переставляем строки по порядку следования во втором ключа. 

                int newi, newj = 0;
                countSymbols = 0;

                Console.WriteLine("Маршрут шифрования был:");

                for (int i = 0; i < Length2; i++)
                {
                    int IndexInEncryptedString;

                    for (int j = 0; j < Length1; j++)
                    {
                        newi = listCharNumSecond[i].NumberInWord;
                        newj = listCharNumFirst[j].NumberInWord;
                        IndexInEncryptedString = newi * Length1 + newj;

                        Console.WriteLine(i + ":" + j + "\t >> \t" + newi + ":" + newj + " " + IndexInEncryptedString + ":" + EncryptedstringUser[IndexInEncryptedString]);


                  //      matrix[newi, newj] = charStringUser[countSymbols++];          А шифровать не надо!

                    }
                    Console.WriteLine();
                }

                //вывод зашифрованной матрицы 
                countSymbols = 0;

                Pause("");

             

           charStringUser = EncryptedstringUser.ToCharArray();                 // Записали шифрованную строку в массив

           ////////////////////////////////////////////////////////////////////


           countSymbols = 0;


           Console.Write("Рашифрованное значение: ");

           for (int i = 0; i < Length2; i++)
           {
               int IndexInEncryptedString;

               for (int j = 0; j < Length1; j++)
               {
                   newi = listCharNumSecond[i].NumberInWord;
                   newj = listCharNumFirst[j].NumberInWord;
                   IndexInEncryptedString = newi * Length1 + newj;     // преобразование координат i:j в координату в строке charStringUser
             
                   stringUser += charStringUser[IndexInEncryptedString]; countSymbols++;

               }
           }

                Pause(stringUser + "\nНажмите клавишу для продолжения");
            }
        }
    }
}
