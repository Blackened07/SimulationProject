using Simulation.MapSim;
using Simulation.MapSim.Entities;
using Simulation.MapSim.Entities.Creatures;
using Simulation.MapSim.Entities.StaticEntities;


namespace Simulation.Path
{
    internal class BFSPAthFinderWithWeight(INeighborFinder neighborFinder) : IPathFinderWeight
    {
    
        public Coordinates Find(Map map, Creature creature, Coordinates start, Func<Coordinates, bool> isTarget, int radius)
        {
            List<Coordinates> creatureNeighborns = neighborFinder.FindNeighbor(map, start);
            
            Coordinates bestNextStep = start;
            float bestScore = float.MinValue;
            creature.Fear = Math.Max(1.0f, creature.Fear - 0.5f);
            creature.Fear = IsEnemyNear(map, start, creature, radius) ? 5.0f : creature.Fear;

            foreach (Coordinates neighborn in creatureNeighborns)
            {
                
                if (isTarget(neighborn))
                {
                    return neighborn;
                }
                if (map.IsOccupied(neighborn))
                {
                    continue;
                }
                float directionScore = EvaluateDirection(start, radius, map, creature) + Random.Shared.NextSingle() * 0.1f;
                if (directionScore > bestScore)
                {
                    bestScore = directionScore;
                    bestNextStep = neighborn;
                }
            }

            return bestNextStep;
        }

        private bool IsEnemyNear(Map map, Coordinates c, Creature e, int diameter)
        {
            int r = diameter / 3;
            Coordinates start = new Coordinates(c.X - r, c.Y - r );

            for(int y = start.Y; y < r - 1; y++)
            {
                for(int x = start.X; x < r - 1; x++)
                {
                    Coordinates current = new Coordinates(x, y);
                    if (map.IsOutOfBounds(current))
                    {
                        continue;
                    }
                    if (e.IsEnemy(map.GetEntity(current)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private float EvaluateDirection(Coordinates start, int radius, Map map, Creature creature)
        {

            Queue<(Coordinates c, int distance)> queue = new Queue<(Coordinates c, int distance)>();
            HashSet<Coordinates> visited = [];

            queue.Enqueue((start, 1));
            visited.Add(map.GetCoordinates(creature));
            visited.Add(start);

            float totalScore = 0f;

            while (queue.Count > 0)
            {
                var (current, dist) = queue.Dequeue();

                if (dist > radius)
                {
                    continue;
                }

                Entity entity = map.GetEntity(current);
                float cellWeight = 0f;
                if (entity != null)
                {
                    cellWeight = creature.GetCellWeight(entity, entity => creature.IsCreature(entity));
                }
                totalScore += cellWeight / dist;

                List<Coordinates> creatureNeighborns = neighborFinder.FindNeighbor(map, current);
                foreach (Coordinates neighbor in creatureNeighborns)
                {
                    if (visited.Contains(neighbor))
                    {
                        continue;
                    }
                    if (map.IsOccupiedByStatic(neighbor))
                    {
                        continue;
                    }
                    visited.Add(neighbor);
                    queue.Enqueue((neighbor, dist + 1));
                }
            }

            return totalScore;
        }
    }
}
