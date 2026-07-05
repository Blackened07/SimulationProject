using Simulation.MapSim;
using Simulation.MapSim.Entities.StaticEntities;
using Simulation.MapSim.Entities.Creatures;
using Simulation.MapSim.Entities;

namespace Simulation.Actions.Spawn
{
    internal class SpawnAction(RandomFreeCoordinatesGenerator random) 
    {
        protected Dictionary<Type, int> entityMaxNumbers = new()
        {
            {typeof(Predator), 15 },
            {typeof(Herbivore), 25 },
            {typeof(Grass), 32 },
            {typeof(Rock), 15 },
        };

        protected void Spawn(Type type, int number, Map map)
        {
           for(int i = 0; i < number; i++)
            {
                Coordinates randomCoordinates = random.Generate(map);
                Entity e = EntityFactory.Create(type);

                map.Put(randomCoordinates, e);
            }
        }
    }
}
