using Simulation.MapSim;
using Simulation.MapSim.Entities;
using Simulation.MapSim.Entities.Creatures;
using Simulation.MapSim.Entities.StaticEntities;

namespace Simulation.Ui
{
    internal class ConsoleRenderer : IView
    {
           public void Render(Map map, int turns)
        {
            int height = map.Height;
            int width = map.Width;
            Entity e = null;
            Creature? boss = null;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    e = map.GetEntity(new Coordinates(j, i));
                    if (e is Boss)
                    {
                        boss = (Creature) map.GetEntity(new Coordinates(j, i));
                    }
                        Console.Write(GetSymbol(e) + "  ");
                }
                Console.WriteLine();
            }
            RenderLog(map, boss, turns);
        }

        private void RenderLog(Map map, Creature? boss, int turns)
        {
            int herbivoresAmount = map.GetEntitiesByType(typeof(Herbivore)).Count();
            int predatorsAmount = map.GetEntitiesByType(typeof(Predator)).Count();
            int grassAmount = map.GetEntitiesByType(typeof(Grass)).Count();
            Console.WriteLine($"Turns amount: {turns}");
            Console.WriteLine($"Grass amount on the map: {grassAmount};");
            Console.WriteLine($"Herbivores amount on the map: {herbivoresAmount};");
            Console.WriteLine($"Predators amount on the map: {predatorsAmount};");
            Console.WriteLine($"Boss Health: {boss?.Health ?? 0};");
            Console.WriteLine($"Boss Hunger: {boss?.Hunger ?? 0};\n");

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
