namespace CaesarCipher
{
    public static class Ciphers
    {
        /// <summary>
        /// XOR
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IEnumerable<byte> Encode(IEnumerable<byte> input, byte[] key)
        {
            int index = 0;
            foreach (var b in input)
            {
                yield return (byte)(b ^ (key.Length == 0 ? 0 : key[index % key.Length]));
                index++;
            }
        }
        public static IEnumerable<byte> Decode(IEnumerable<byte> input, byte[] key) => Encode(input, key);

        /// <summary>
        /// https://en.wikipedia.org/wiki/Vigen%C3%A8re_cipher
        /// </summary>
        /// <param name="input"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string Encode(string input, string code) =>
            (input, BuildKey(input.Length, code)) switch
            {
                (null, _) => "",
                (string, string key) => new string(input.Zip(key).Select(item => Encode(item.First, item.Second)).ToArray())
            };
        public static string Decode(string input, string code) =>
            (input, BuildKey(input.Length, code)) switch
            {
                (null, _) => "",
                (string, string key) => new string(input.Zip(key).Select(item => Decode(item.First, item.Second)).ToArray())
            };

        /// <summary>
        /// https://en.wikipedia.org/wiki/Caesar_cipher
        /// </summary>
        /// <param name="input"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string Encode(string input, char code) =>
            (GetOffset(code), input) switch
            {
                (_, null) => "",
                (int offset, _) => new string(input.Select(c => Encode(c, offset)).ToArray())
            };
        public static string Decode(string input, char code) =>
            (GetOffset(code), input) switch
            {
                (_, null) => "",
                (int offset, _) => new string(input.Select(c => Decode(c, offset)).ToArray())
            };

        public static char Encode(char input, char code) => Encode(input, GetOffset(code));
        public static char Decode(char input, char code) => Decode(input, GetOffset(code));

        public static char Encode(char input, int offset) =>
            input switch
            {
                >= 'A' and <= 'Z' => (char)('A' + ((input - 'A' + offset) % 26)),
                >= 'a' and <= 'z' => (char)('a' + ((input - 'a' + offset) % 26)),
                _ => input
            };
        public static char Decode(char input, int offset) =>
            input switch
            {
                >= 'A' and <= 'Z' => (char)('A' + ((input - 'A' - offset + 26) % 26)),
                >= 'a' and <= 'z' => (char)('a' + ((input - 'a' - offset + 26) % 26)),
                _ => input
            };

        public static int GetOffset(char code) => code switch
        {
            >= 'A' and <= 'Z' => code - 'A',
            >= 'a' and <= 'z' => code - 'a',
            _ => throw new ArgumentOutOfRangeException(nameof(code), "\"code\" must be between 'A' and 'Z'")
        };

        public static string BuildKey(int length, string? code)
        {
            code = new string((code ?? string.Empty).Where(c => char.IsLetter(c)).ToArray());
            if (string.IsNullOrWhiteSpace(code)) return new string(Enumerable.Range(0, length).Select(i => (char)('A' + (i % 26))).ToArray());
            return string.Join("", Enumerable.Range(0, length / code.Length + 1).Select(_ => code)).Substring(0, length);
        }
    }
}