
using Simulation.Path;
using Simulation.MapSim.Entities.Creatures;
using Simulation.MapSim;
using Simulation.MapSim.Entities.StaticEntities;

namespace Simulation.Actions.Spawn
{

    internal class InitAction(RandomFreeCoordinatesGenerator random) : SpawnAction(random), IAction
    {
        
        public void Execute(Map map)
        {
            foreach(KeyValuePair<Type, int> kvp in entityMaxNumbers)
            {
                Type type = kvp.Key;
                int num = kvp.Value;

                if (type == typeof(Grass) || type == typeof(Rock))
                {
                    Spawn(type, num, map);
                    continue;
                }

                if (genomeCreators.TryGetValue(type, out var genome))
                {
                    Spawn(type, num, map, genome());
                }
              
            }    
        }
    }
}
