

using Simulation.MapSim.Entities.Creatures;
using Simulation.MapSim.Entities.StaticEntities;

namespace Simulation.MapSim.Entities
{
    internal class EntityFactory
    {
        private static readonly Dictionary<Type, Func<Genome, Entity>> creatureСreators = new()
        {
            {typeof(Predator), (genome) => new Predator{Health = 30, AttackPower = 10, Hunger = 1, Fear = 1, Dna = genome, CurrentEnergy = 50} },
            {typeof(Herbivore), (genome) => new Herbivore{Health = 15, AttackPower = 1, Hunger = 1, Fear = 1, Dna = genome, CurrentEnergy = 50 } },
            {typeof(Grass), (genome) => new Grass() },
            {typeof(Rock), (genome) => new Rock() } 
        };
        private static readonly Dictionary<Type, Func<Entity>> staticСreators = new()
        { 
            {typeof(Grass), () => new Grass() },
            {typeof(Rock), () => new Rock() }
        };

        public static Entity CreateCreature(Type type, Genome genome)
        {
            if (creatureСreators.TryGetValue(type, out var creator))
            {
                
                return creator(genome);
            }

            return new Earth();
        }
        public static Entity Create(Type type)
        {
            if (staticСreators.TryGetValue(type, out var creator))
            {

                return creator();
            }

            return new Earth();
        }

        public static Entity CreateEarth()
        {
            return new Earth();
        }
    }
}
