using Simulation.MapSim;
using Simulation.MapSim.Entities;

namespace Simulation.MapSim.Entities.Creatures
{
    internal class Predator : Creature
    {
        public override void InteractWithTarget(Map map, Entity target, Coordinates final)
        {
            Creature c = (Creature)target;
            Attack(c);

            if (c.IsDead())
            {
                map.Put(final, EntityFactory.CreateEarth());
            }
        }

        public override bool IsTarget(Entity entity)
        {
            if (Health < 15)
            {
                return entity is Herbivore && entity is Boss;
            }
            return  entity is Herbivore;
        }
    }
}
