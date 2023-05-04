namespace WpfUniverse
{
    public class StarSystem : SimpleStarSystem
    {
        // https://github.com/OneLoneCoder/Javidx9/blob/master/PixelGameEngine/SmallerProjects/OneLoneCoder_PGE_ProcGen_Universe.cpp
        public StarSystem(int x, int y) : base(x, y)
        {
            // If we are viewing the system map, we need to generate the
            // full system

            // Generate Planets
            double distanceFromStar = Rand.Next(60.0, 200.0);
            int planets = Rand.Next(10);
            //for (int i = 0; i < planets; i++)
            //{
            //    sPlanet p;
            //    p.distance = distanceFromStar;
            //    distanceFromStar += rndDouble(20.0, 200.0);
            //    p.diameter = rndDouble(4.0, 20.0);

            //    // Could make temeprature a function of distance from star
            //    p.temperature = rndDouble(-200.0, 300.0);

            //    // Composition of planet
            //    p.foliage = rndDouble(0.0, 1.0);
            //    p.minerals = rndDouble(0.0, 1.0);
            //    p.gases = rndDouble(0.0, 1.0);
            //    p.water = rndDouble(0.0, 1.0);

            //    // Normalise to 100%
            //    double dSum = 1.0 / (p.foliage + p.minerals + p.gases + p.water);
            //    p.foliage *= dSum;
            //    p.minerals *= dSum;
            //    p.gases *= dSum;
            //    p.water *= dSum;

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

            //    // Add planet to vector
            //    vPlanets.push_back(p);
            //}

        }
    }

}
