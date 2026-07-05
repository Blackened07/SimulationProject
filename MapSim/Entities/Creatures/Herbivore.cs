using Simulation.MapSim;
using Simulation.MapSim.Entities;
using Simulation.MapSim.Entities.StaticEntities;
using Simulation.Path;

namespace Simulation.MapSim.Entities.Creatures 
{
    internal class Herbivore : Creature
    {
        public override void InteractWithTarget(Map map, Entity target, Coordinates final)
        {
           //if is Predator -> attak
            map.Put(final, new Earth());
           
            if (this.Health < 20)
            {
                this.Health += 2;
            }
           
        }

        public override float GetCellWeight(Entity entity, Func<Entity, bool> isCreature)
        {
            Creature c = null;
            if (isCreature(entity))
            {
                c = (Creature)entity;
            }
            if (entity is Grass)
            {
                return WorldWeight.Grass * Hunger;
            }
            else if (entity is Predator)
            {
                if (c.Health < c.Health / 5)
                {
                    return -WorldWeight.PredatorForHerbivores / 5 * Fear;
                }
                return - WorldWeight.PredatorForHerbivores * Fear;
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
            return entity is Grass;
        }

        public override bool IsEnemy(Entity entity)
        {
            return entity is Predator;
        }
    }
}
