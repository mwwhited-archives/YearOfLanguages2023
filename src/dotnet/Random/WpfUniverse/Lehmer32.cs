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

        //https://en.wikipedia.org/wiki/Lehmer_random_number_generator
        public uint Next()
        {
            var product = (ulong)_seed * 279470273;
            product = (product & 0xffffffff) + 5 * (uint)(product >> 32);
            product += 4;

            uint x = (uint)product + 5 * (uint)(product >> 32);
            return _seed = x - 4;

            //_seed += 0xe120fc15;
            //ulong tmp = _seed * 0x4a39b70d;
            //uint m1 = (uint)((tmp >> 32) ^ tmp);
            //tmp = ((ulong)m1) * 0x12fad5c9;
            //uint m2 = (uint)((tmp >> 32) ^ tmp);
            //return m2;

            /*
                uint32_t lcg_rand(uint32_t *state)
                {
	                uint64_t product = (uint64_t)*state * 279470273u;
	                uint32_t x;

	                // Not required because 5 * 279470273 = 0x5349e3c5 fits in 32 bits.
	                // product = (product & 0xffffffff) + 5 * (product >> 32);
	                // A multiplier larger than 0x33333333 = 858,993,459 would need it.

	                // The multiply result fits into 32 bits, but the sum might be 33 bits.
	                product = (product & 0xffffffff) + 5 * (uint32_t)(product >> 32);

	                product += 4;
	                // This sum is guaranteed to be 32 bits.
	                x = (uint32_t)product + 5 * (uint32_t)(product >> 32);
	                return *state = x - 4;
                }
             */
        }

        public int Next(int max) => Next(0, max);
        public int Next(int min, int max) => (int)((Next() % (max - min)) + min);
        public double Next(double max) => Next(0, max);
        public double Next(double min, double max) => ((double)Next() / 0x7fffffff) * (max - min) + min;

        public TEnum Next<TEnum>() where TEnum : struct, Enum
        {
            var values = Enum.GetValues<TEnum>();
            return values[Next(values.Length)];
        }
    }
}
