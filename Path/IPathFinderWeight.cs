using Simulation.MapSim;
using Simulation.MapSim.Entities.Creatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simulation.Path
{
    internal interface IPathFinderWeight
    {
        Coordinates Find(Map map, Creature creature, Coordinates start, Func<Coordinates, bool> isTarget, int radius);
    }
}
