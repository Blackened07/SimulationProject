using Simulation.MapSim;

namespace Simulation.Path
{
    internal interface INeighborFinder
    {
        List<Coordinates> FindNeighbor(Map map, Coordinates startCoordinate);
    }
}
