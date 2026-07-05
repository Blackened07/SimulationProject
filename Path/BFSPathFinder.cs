using Simulation.MapSim;
using Simulation.MapSim.Entities;
using Simulation.MapSim.Entities.Creatures;
using Simulation.MapSim.Entities.StaticEntities;

namespace Simulation.Path
{
    /*
     * Этот класс ищет ближайшую подходящую цель для Креатуры у которой вызван метод MakeMove();
     * Метод MakeMove() подразумевает любое запрограммированное действие (атака, еда) в зависимости от найденного ближайшего таргета
     * Таргеты для предатора - игрок, травоядные, охотники на травоядных и еда на карте
     * Тарегты охотников - травоядные, еда, игрок
     * Таргеты травоядных - еда, трава
     */
    internal class BFSPathFinder(INeighborFinder neighborFinder) : IPathFinder
    {

        public List<Coordinates> Find(Map map, Coordinates start, Func<Coordinates, bool> isTarget)
        {
            HashSet<Coordinates> visited = [];
            Queue<Coordinates> queue = [];
            Dictionary<Coordinates, Coordinates> parents = [];

            queue.Enqueue(start);
            parents[start] = start;
            visited.Add(start);

            Coordinates? targetFound = null;

            while (queue.Count > 0)
            {
                Coordinates current = queue.Dequeue();

                if (isTarget(current))
                {
                    targetFound = current;
                    break;
                }

                List<Coordinates> currentNeighbors = neighborFinder.FindNeighbor(map, current);
                foreach (Coordinates neighbor in currentNeighbors)
                {
                    if (visited.Contains(neighbor))
                    {
                        continue;
                    }

                    if (!(isTarget(neighbor) || !map.IsOccupied(neighbor)))
                    {
                        continue;
                    }

                    parents[neighbor] = current;
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }

            return BuildPath(start, targetFound, parents);
        }

        private List<Coordinates> BuildPath(Coordinates start, Coordinates? targetFound, Dictionary<Coordinates, Coordinates> parents)
        {   
            List<Coordinates> path = [];

            if (targetFound == null)
            {
                return path;
            }

            Coordinates current = targetFound.Value;

            while (!current.Equals(start))
            {
                path.Add(current);
                current = parents[current];
            }
            path.Add(start);
            path.Reverse();

            return path;
        }
    }
}
