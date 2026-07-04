
using Simulation.MapSim.Entities;
using Simulation.MapSim.Entities.StaticEntities;
using Simulation.MapSim.Entities.Creatures;

namespace Simulation.MapSim
{
    internal class MapFactory
    {
        public static Map GetMapInstance(int height, int width)
        {
            Dictionary<Coordinates, Entity> map = new Dictionary<Coordinates, Entity>();

            for (int i = 0; i < height; i++)
            {
                for (int  j = 0; j < width; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        map.Add(new Coordinates(j, i), new Player {Health = 30 });
                    }
                    else if (i == height - 1  && j == width - 1)
                    {
                        map.Add(new Coordinates(j, i), new Boss {Health = 50, AttackPower = 5});
                    }
                             
                }
            }

            return new Map(map, height, width);
        }

   
    }
}
