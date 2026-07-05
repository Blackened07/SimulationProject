using Simulation.MapSim;
using Simulation.MapSim.Entities;
using Simulation.Path;

namespace Simulation.MapSim.Entities.Creatures
{
    internal class Boss : Creature
    {
        public override void InteractWithTarget(Map map, Entity target, Coordinates final)
        {
           /* if (target is Player)
            {
                Player player = (Player)target;
                player.TakeDamage(this.AttackPower);
                if (player.Health <= 0)
                {
                    map.Put(final, EntityFactory.CreateEarth());
                }
            }
            else
            {*/
                Creature c = (Creature)target;
                Attack(c);

                if (c.IsDead())
                {
                    map.Put(final, EntityFactory.CreateEarth());
                }
            /*}*/
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
                return (WorldWeight.Herbivore - 10) * Hunger;
            }
            else if (entity is Player)
            {
                if (this.Health < this.Health / 2) { 
                    return -(WorldWeight.Herbivore + 50) * Fear;
                }
                return (WorldWeight.Herbivore + 50) * Hunger;
            }
            return 0f;
        }

        public override bool IsTarget(Entity entity)
        {
            /*if (this.Health >= this.Health / 2)
            {
                return entity is Player;
            }*/

            return entity is Predator || entity is Herbivore;
        }

        public override bool IsEnemy(Entity entity)
        {
            if (this.Health < this.Health * 3)
            {
                return entity is Player;
            }
            return false;
        }
    }
}
