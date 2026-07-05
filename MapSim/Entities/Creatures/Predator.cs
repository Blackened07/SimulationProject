
using Simulation.Path;

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

        public override float GetCellWeight(Entity entity, Func<Entity, bool> isCreature)
        {
            Creature c = null;
            if (isCreature(entity))
            {
                c = (Creature)entity;
            }

            if (entity is Herbivore)
            {
                return WorldWeight.Herbivore * Hunger;
            }
            else if (entity is Predator)
            {
                return -WorldWeight.PredatorForPredators * Fear;
            }
            else if (entity is Boss)
            {
                if (c.Health < c.Health / 2)
                {
                    return -WorldWeight.Boss / 2 * Fear;
                }
                return -WorldWeight.Boss * Fear;
            }
            return 0f;
        }

        public override bool IsTarget(Entity entity)
        {
            if (this.Health < this.Health / 4)
            {
                return entity is Herbivore || entity is Predator || entity is Boss;
            }
            return entity is Herbivore;
        }

        public override bool IsEnemy(Entity entity)
        {
            return entity is Boss;
        }
    }
}
