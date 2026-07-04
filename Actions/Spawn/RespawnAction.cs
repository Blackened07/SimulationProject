

using Simulation.MapSim;
using Simulation.MapSim.Entities;
using Simulation.MapSim.Entities.Creatures;
using Simulation.MapSim.Entities.StaticEntities;

namespace Simulation.Actions.Spawn
{
    internal class RespawnAction(RandomFreeCoordinatesGenerator random) : SpawnAction(random), IAction
    {
        protected Dictionary<Type, int> entityMinNumbers = new()
        {
            {typeof(Predator), 2 },
            {typeof(Herbivore), 5 },
            {typeof(Grass), 8 }
        };
        public void Execute(Map map)
        {
            foreach (var e in entityMinNumbers)
            {
                int actualCount = getCount(map, e.Key);
                int minCount = e.Value;

                if (actualCount <= minCount)
                {
                    int maxCount = entityMaxNumbers[e.Key];
                    int toSpawn = maxCount - minCount;
                    Spawn(e.Key, toSpawn, map);
                }
            }
        }     

        private int getCount(Map map, Type t) 
        {
            return map.GetEntitiesByType(t).Count();
        }
    }
}
