

using Simulation.MapSim;

namespace Simulation.Actions.Spawn
{
    internal class RandomFreeCoordinatesGenerator
    {
        public Coordinates Generate(Map map)
        {
            int w = map.Width;
            int h = map.Height;

            while (true)
            {
                int randomForW = Random.Shared.Next(0, w);
                int randomForH = Random.Shared.Next(0, h);

                Coordinates c = new Coordinates(randomForW, randomForH);
                if (isFree(map, c))
                {
                    return c;
                }
            }
        }

        private bool isFree(Map map, Coordinates c)
        {
            return !map.IsOccupied(c) && ((c.X != 0 && c.Y != 1) || (c.X == 1 && c.Y == 0) || (c.X == map.Width - 2 && c.Y == map.Height - 1) || 
                (c.X == map.Width - 1 && c.Y == map.Height - 2));
        }
    }
}
