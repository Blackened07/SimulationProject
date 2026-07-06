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
            IncreaseVitality();

        }

        public override float GetCellWeight(Entity entity, bool isCreature)
        {
            Creature c = null;
            if (isCreature)
            {
                c = (Creature)entity;
            }

            if (entity is Grass)
            {
                return Dna.FoodAttraction * Hunger;
            }
            else if (entity is Predator)
            {
                if (c.Health < c.Health / 5)
                {
                    return -Dna.FearDanger / 5 * Fear;
                }
                return - Dna.FearDanger * Fear;
            }
            else if (entity is Boss)
            {
                if (c.Health < c.Health / 2)
                {
                    return (-Dna.FearDanger - 25) / 2 * Fear;
                }
                return (-Dna.FearDanger - 25) * Fear;
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
