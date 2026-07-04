

using Simulation.MapSim.Entities.Creatures;
using Simulation.MapSim.Entities.StaticEntities;

namespace Simulation.MapSim.Entities
{
    internal class EntityFactory
    {
        private static readonly IReadOnlyDictionary<Type, Func<Entity>> creators = new Dictionary<Type, Func<Entity>>
        {
            {typeof(Predator), () => new Predator{Health = 20, AttackPower = 3 } },
            {typeof(Herbivore), () => new Herbivore{Health = 20, AttackPower = 1 } },
            {typeof(Grass), () => new Grass() },
            {typeof(Rock), () => new Rock() } 
        };
        public static Entity Create(Type type)
        {
            if (creators.TryGetValue(type, out var creator))
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
