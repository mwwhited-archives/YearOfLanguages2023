namespace WpfUniverse
{
    public class SimpleStarSystem
    {
        // https://github.com/OneLoneCoder/Javidx9/blob/master/PixelGameEngine/SmallerProjects/OneLoneCoder_PGE_ProcGen_Universe.cpp
        public int X { get; init; }
        public int Y { get; init; }
        public bool Exists { get; init; }
        public double Diameter { get; init; }
        public StarColors Color { get; init; }


        protected uint Seed { get; init; }
        protected Lehmer32 Rand { get; init; }

        public SimpleStarSystem(int x, int y)
        {
            X = x;
            Y = y;
            Seed = (uint)(X & 0xffff) | ((uint)(Y & 0xffff) << 16);
            Rand = new Lehmer32(Seed);
            Exists = Rand.Next(20) == 1;
            if (!Exists) return;

            // Generate Star
            Diameter = Rand.Next(10.0, 40.0);
            Color = Rand.Next<StarColors>();
        }
    }
}
