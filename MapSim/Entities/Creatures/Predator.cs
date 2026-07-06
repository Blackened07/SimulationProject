
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
                IncreaseVitality();
                map.Put(final, EntityFactory.CreateEarth());
            }

        }

        public override float GetCellWeight(Entity entity, bool isCreature)
        {
            Creature c = null;
            if (isCreature)
            {
                c = (Creature)entity;
            }

            if (entity is Herbivore)
            {
                return Dna.FoodAttraction * Hunger;
            }
            else if (entity is Predator)
            {
                return Dna.FearDanger * Fear;
            }
            else if (entity is Boss)
            {
                if (c.Health < c.Health / 2)
                {
                    return (-Dna.FearDanger - 60) / 2 * Fear;
                }
                return (-Dna.FearDanger - 60) * Fear;
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
