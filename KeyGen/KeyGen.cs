using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace KeyGen
{


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
                      
      
        public static BigInteger NOD(BigInteger _a, BigInteger _b)
        {
            while (_b != 0)
                _b = _a % (_a = _b);
            return _a;
        }
        public static BigInteger VzaimnoProsty(BigInteger _a)               // Поиск взаимно простого числа.
        {
            BigInteger r = new BigInteger();

            for (BigInteger i = 2; i < Int32.MaxValue - 1; i++)
            {
                r = NOD(i, _a);
                if (r == 1)
                    return i;
            }
            return 0;
        }

        public static bool IsPrimeNumber(int num)               // Проверка является ли число простым
        {   
            // https://programm.top/c-sharp/programs/prime-number/
            var result = true;
            if (num > 1)
            {
                for (var i = 2u; i < num; i++)
                {
                    if (num % i == 0)
                    {
                        result = false;
                        break;
                    }
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        public static int GenerateRundomN(int predel1, int predel2)
        // генерация рандомного числа и проверка на то что оно простое
        // http://plssite.ru/csharp/csharp_random_article.html
        {
            int num = 1;
            Random rnd = new Random(predel2);
            for (int i = 1; i < Int32.MaxValue-1; i++)
            {
                num = Math.Abs(rnd.Next(predel1, predel2));
                if (IsPrimeNumber(num)) break; 
            }
            return num;
        }

        
        public static BigInteger SecureD(BigInteger _e, BigInteger _fn)
        {   // d= (k*fn + 1)/e  =>
            // подбор d*e == k*fn + 1
            for (BigInteger k = 1; k < Int32.MaxValue - 1; k++)
            {
                if ((k * _e) % _fn == 1) return k;          // d=k если mod = 1
            }
            return 0;
        }

        static void Main(string[] args)
        {


            // int p = Convert.ToInt32(textBox1.Text);
            // int q = Convert.ToInt32(textBox2.Text);

            // http://altaev-aa.narod.ru/security/Rsa.html

            Console.WriteLine("Генерируем p & q....");

            int p = GenerateRundomN(1, 9999);
           //int p = GenerateRundomN(1, Int32.MaxValue/2-1);
           //int q = GenerateRundomN(Int32.MaxValue / 2 , Int32.MaxValue-1);
            int q = GenerateRundomN(10001, 19999);

            Console.WriteLine("p:" + p + " q:" + q);

            //https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?view=net-5.0
            BigInteger n, fn, E, d;


            Console.WriteLine("Генерируем ключи....");

            n = p * q;                          // простые числа
            fn = (p - 1) * (q - 1);             // функция эйлера

           E = VzaimnoProsty(fn);              // число "E", взаимно простое с числом "fn"
                                               // Е, n - открытый ключ

            Console.WriteLine("n:"+n + " fn:" + fn + " e:" + E);
           
           d = SecureD(E, fn);                      // d, n - закрытый ключ

            Console.WriteLine("Open key pair: (" + E + "," + n +")");
            Console.WriteLine("Secure key pair: (" + d + "," + n + ")");
           
            const string PUBLICPATH = "../../../public.key";
            const string PRIVATEPATH = "../../../private.key";


        // https://docs.microsoft.com/en-us/dotnet/api/system.io.file?view=net-5.0

            using (StreamWriter FileStream = File.CreateText(PUBLICPATH))
            {
                FileStream.WriteLine(Convert.ToString(E));
                FileStream.WriteLine(Convert.ToString(n));
            }

            using (StreamWriter FileStream = File.CreateText(PRIVATEPATH))
            {
                FileStream.WriteLine(Convert.ToString(d));
                FileStream.WriteLine(Convert.ToString(n));
            }

            Console.WriteLine("Созданы "+ PRIVATEPATH+" "+ PUBLICPATH);

            Console.ReadKey();
           
        }

    }
}
