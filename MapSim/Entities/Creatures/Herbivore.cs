using Simulation.MapSim;
using Simulation.MapSim.Entities;
using Simulation.MapSim.Entities.StaticEntities;

namespace Simulation.MapSim.Entities.Creatures 
{
    internal class Herbivore : Creature
    {
        public override void InteractWithTarget(Map map, Entity target, Coordinates final)
        {
            if (target is Predator)
            {
                Creature c = (Creature)target;
                Attack(c);

                if (c.IsDead())
                {
                    map.Put(final, EntityFactory.CreateEarth());
                }
            }
            else
            {
                map.Put(final, new Earth());
            }
        }

        public override bool IsTarget(Entity entity)
        {
            if (Health < 7)
            {
                return entity is Grass && entity is Predator;
            }
            return entity is Grass;
        }
    }
}
