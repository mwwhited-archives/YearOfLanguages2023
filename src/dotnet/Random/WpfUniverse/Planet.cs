namespace WpfUniverse
{
    public class Planet
    {
        public override string ToString() => $"{PlanetId}: {Distance}->{Diameter} ({Temperature},{Foliage},{Minerals},{Gases},{Water})";

        public StarSystem System { get; init; }
        public double Distance { get; init; }
        public int PlanetId { get; init; }

        public double Diameter { get; init; }
        public double Temperature { get; init; }

        public double Foliage { get; init; }
        public double Minerals { get; init; }
        public double Gases { get; init; }
        public double Water { get; init; }

        protected uint Seed { get; init; }
        protected Lehmer32 Rand { get; init; }

        public Planet(StarSystem system, int seed,  int planetId, double distance)
        {
            System = system;
            Distance = distance;
            PlanetId = planetId;
            Seed = (uint)(((System.X & 0xffff) | ((System.Y & 0xffff) << 16) ^ (planetId * 0x10001F)) + seed);
            Rand = new Lehmer32(Seed);

            Diameter = Rand.Next(5.0, 60.0);
            Temperature = Rand.Next(-200.0, 300.0);

            // Normalise to 100%
            var foliage = Rand.Next(0.0, 1.0);
            var minerals = Rand.Next(0.5, 1.0);
            var gases = Rand.Next(0.5, 1.0);
            var water = Rand.Next(0.3, 1.0);

            var sum = 1.0 / (foliage + minerals + gases + water);
            Foliage = foliage * sum;
            Minerals = minerals * sum;
            Gases = gases * sum;
            Water = 1.0 - Foliage - Minerals - Gases;
        }

        //    // Population could be a function of other habitat encouraging
        //    // properties, such as temperature and water
        //    p.population = std::max(rndInt(-5000000, 20000000), 0);

        //    // 10% of planets have a ring
        //    p.ring = rndInt(0, 10) == 1;

        //    // Satellites (Moons)
        //    int nMoons = std::max(rndInt(-5, 5), 0);
        //    for (int n = 0; n < nMoons; n++)
        //    {
        //        // A moon is just a diameter for now, but it could be
        //        // whatever you want!
        //        p.vMoons.push_back(rndDouble(1.0, 5.0));
        //    }
    }
}
