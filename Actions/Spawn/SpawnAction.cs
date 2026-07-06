using Simulation.MapSim;
using Simulation.MapSim.Entities;
using Simulation.MapSim.Entities.Creatures;
using Simulation.MapSim.Entities.StaticEntities;
using Simulation.Path;

namespace Simulation.Actions.Spawn
{
    internal class SpawnAction(RandomFreeCoordinatesGenerator random) 
    {
        protected Dictionary<Type, int> entityMaxNumbers = new()
        {
            {typeof(Predator), 5 },
            {typeof(Herbivore), 35 },
            {typeof(Grass), 70 },
            {typeof(Rock), 15 },
        };
        protected Dictionary<Type, Func<Genome>> genomeCreators = new()
        {
            {typeof(Predator), () => new Genome(WorldWeight.Herbivore, WorldWeight.PredatorForPredators) },
            {typeof(Herbivore), () => new Genome(WorldWeight.Grass, WorldWeight.PredatorForHerbivores) }
        };

        protected void Spawn(Type type, int number, Map map, Genome genome)
        {
           for(int i = 0; i < number; i++)
            {
                Coordinates randomCoordinates = random.Generate(map);
                Entity e = EntityFactory.CreateCreature(type, genome);

                map.Put(randomCoordinates, e);
            }
        }
        protected void Spawn(Type type, int number, Map map)
        {
            for (int i = 0; i < number; i++)
            {
                Coordinates randomCoordinates = random.Generate(map);
                Entity e = EntityFactory.Create(type);

                map.Put(randomCoordinates, e);
            }
        }
     

    }
}
