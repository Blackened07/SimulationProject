using Simulation.MapSim;
using Simulation.MapSim.Entities;
using Simulation.MapSim.Entities.StaticEntities;

namespace Simulation.MapSim.Entities.Creatures 
{
    internal class Herbivore : Creature
    {
        public override void InteractWithTarget(Map map, Entity target, Coordinates final)
        {
           
            map.Put(final, new Earth());
            if (this.Health < 20)
            {
                this.Health++;
            }
            
        }

        public override bool IsTarget(Entity entity)
        {
            
            return entity is Grass;
        }
    }
}
