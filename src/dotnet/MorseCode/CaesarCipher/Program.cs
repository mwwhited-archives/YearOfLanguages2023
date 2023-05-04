using System.Text;

namespace CaesarCipher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var toEncode = new string(Enumerable.Range(0, 32 * 3).Select(i => (char)(i + ' ')).ToArray());
            var code = 'H';
            var longCode = "Hello World";
            var longerCode = Enumerable.Range(0, 26).Select(i => (char)(i + 'A')).ToArray();

            var encoded = Ciphers.Encode(toEncode, code);
            Console.WriteLine(encoded);
            var decoded = Ciphers.Decode(encoded, code);
            Console.WriteLine(decoded);

            var encoded2 = Ciphers.Encode(toEncode, (char)(code + 32));
            Console.WriteLine(encoded2);
            var decoded2 = Ciphers.Decode(encoded2, (char)(code + 32));
            Console.WriteLine(decoded2);

            var encoded3 = Ciphers.Encode(toEncode, longCode);
            Console.WriteLine(encoded3);
            var decoded3 = Ciphers.Decode(encoded3, longCode);
            Console.WriteLine(decoded3);

            var encoded4 = Ciphers.Encode(Encoding.UTF8.GetBytes(toEncode), Encoding.UTF8.GetBytes(longCode)).ToArray();
            Console.WriteLine(Encoding.UTF8.GetString(encoded4));
            var decoded4 = Ciphers.Decode(encoded4, Encoding.UTF8.GetBytes(longCode)).ToArray();
            Console.WriteLine(Encoding.UTF8.GetString(decoded4));
        }
    }
}