using Simulation.MapSim;
using Simulation.MapSim.Entities;
using Simulation.MapSim.Entities.Creatures;
using Simulation.MapSim.Entities.StaticEntities;

namespace Simulation.Ui
{
    internal class ConsoleRenderer : IView
    {
           public void Render(Map map)
        {
            int height = map.Height;
            int width = map.Width;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Entity e = map.GetEntity(new Coordinates(j, i));

                    Console.Write(GetSymbol(e) + "  ");
                }
                Console.WriteLine();
            }
        }

        private string GetSymbol(Entity e) => e switch
        {
            Player => "@",
            Boss => "&",
            Earth => ".",
            Grass => "░",
            Rock => "▲",
            Herbivore => "~",
            Predator => "Ѫ",
            _ => " "
        };
        
    }
}
