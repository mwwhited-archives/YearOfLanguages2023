using System;
using System.Collections.Generic;

namespace WpfUniverse
{
    public class StarSystem : SimpleStarSystem
    {
        // https://github.com/OneLoneCoder/Javidx9/blob/master/PixelGameEngine/SmallerProjects/OneLoneCoder_PGE_ProcGen_Universe.cpp

        private readonly List<Planet> _planets = new();
        public IReadOnlyList<Planet> Planets => _planets.AsReadOnly();


        public StarSystem(SimpleStarSystem simple) : this(simple.X, simple.Y)
        {
        }
        public StarSystem(int x, int y) : base(x, y)
        {
            // If we are viewing the system map, we need to generate the
            // full system

            // Generate Planets
            double distanceFromStar = this.Diameter * 100 +  Rand.Next(60.0, 200.0);
            int planets = Rand.Next(10);

            for (var planetId = 0; planetId < planets; planetId++)
            {
                var pSeed = Rand.Next();
                var planet = new Planet(this, (int)pSeed, planetId, distanceFromStar);
                _planets.Add(planet);
                distanceFromStar += Rand.Next(planet.Diameter * 2, Math.Min(200.0, planet.Diameter * 3));
            }
        }
    }
}
