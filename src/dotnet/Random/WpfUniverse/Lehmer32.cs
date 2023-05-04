using System;
using System.Threading.Tasks;

namespace WpfUniverse
{
    public class Lehmer32
    {
        // https://www.youtube.com/watch?v=ZZY9YE7rZJw&ab_channel=javidx9
        private uint _seed;

        public Lehmer32(uint seed = 0)
        {
            _seed = seed;
        }

        public uint Next()
        {
            _seed += 0xe120fc15;
            ulong tmp = _seed * 0x4a39b70d;
            uint m1 = (uint)((tmp >> 32) ^ tmp);
            tmp = ((ulong)m1) * 0x12fad5c9;
            uint m2 = (uint)((tmp >> 32) ^ tmp);
            return m2;
        }

        public int Next(int max) => Next(0, max);
        public int Next(int min, int max) => (int)((Next() % (max - min)) + min);
        public double Next(double max) => Next(0, max);
        public double Next(double min, double max) => (Next() / 0x7fffffff) * (max - min) + min;

        public TEnum Next<TEnum>() where TEnum : struct, Enum
        {
            var values = Enum.GetValues<TEnum>();
            return values[Next(values.Length)];
        }
    }
}
