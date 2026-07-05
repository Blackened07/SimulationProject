using Simulation.MapSim;
using Simulation.MapSim.Entities.Creatures;

namespace Simulation.Path
{
    internal interface IPathFinder
    {
        List<Coordinates> Find(Map map, Coordinates start, Func<Coordinates, bool> isTarget);
    }
}
