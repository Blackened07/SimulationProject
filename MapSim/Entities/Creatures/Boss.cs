using Simulation.MapSim;
using Simulation.MapSim.Entities;

namespace Simulation.MapSim.Entities.Creatures
{
    internal class Boss : Creature
    {
        public override void InteractWithTarget(Map map, Entity target, Coordinates final)
        {
            if (target is Player)
            {
                Player player = (Player)target;
                player.TakeDamage(this.AttackPower);
                if (player.Health <= 0)
                {
                    map.Put(final, EntityFactory.CreateEarth());
                }
            }
            else
            {
                Creature c = (Creature)target;
                Attack(c);

                if (c.IsDead())
                {
                    map.Put(final, EntityFactory.CreateEarth());
                }
            }
        }

        public override bool IsTarget(Entity entity)
        {//добавить логику здоровья - если мло здоровье таргетом становятся другие энтити
            return entity is Player || entity is Herbivore || entity is Predator;
        }
    }
}
