

using Simulation.MapSim;
using Simulation.MapSim.Entities;
using Simulation.MapSim.Entities.Creatures;
using Simulation.MapSim.Entities.StaticEntities;

namespace Simulation.Actions.Spawn
{
    internal class RespawnAction(RandomFreeCoordinatesGenerator random) : SpawnAction(random), IAction
    {
        public static int herbivoresWasBornNum;
        public static int predatorsWasBornNum;

        protected Dictionary<Type, int> entityMinNumbers = new()
        {
          /*  {typeof(Predator), 5 },
            {typeof(Herbivore), 15 },*/
            {typeof(Grass), 50 }
        };

         
        public void Execute(Map map)
        {
            CheckMinValues(map);
            
            List<Creature> creatures = map.GetEntities();

            foreach (Creature parent in creatures)
            {
                if (parent is Boss) continue;

                if (parent.CurrentEnergy >= parent.MAX_ENERGY)
                {
                    Type type = parent.GetType();
                    Genome childGenome = parent.Dna.Mutate();

                    if (parent is Herbivore) herbivoresWasBornNum++;
                    if (parent is Predator)
                    {
                        predatorsWasBornNum++;
                       
                    }
                    /*int maxCount = entityMaxNumbers[type];
                    if (maxCount <= getCount(map, type)) continue;*/


                    Spawn(type, 1, map, childGenome);
                    parent.CurrentEnergy = parent.MAX_ENERGY / 2;
                }
            }
        }

        private void CheckMinValues(Map map)
        {
            foreach (var e in entityMinNumbers)
            {
                int actualCount = getCount(map, e.Key);
                int minCount = e.Value;

                if (actualCount <= minCount)
                {
                    int maxCount = entityMaxNumbers[e.Key];
                    int toSpawn = maxCount - minCount;

                    if (e.Key == typeof(Grass))
                    {
                        Spawn(e.Key, toSpawn, map);
                        continue;
                    }

                    if (genomeCreators.TryGetValue(e.Key, out var genome))
                    {
                        Spawn(e.Key, toSpawn, map, genome());
                    }
                }
            }
        }

        private int getCount(Map map, Type t) 
        {
            return map.GetEntitiesByType(t).Count();
        }
    }
}
