using Simulation.MapSim;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simulation.Path
{
    internal class NeighborFinderWithFourDirs : INeighborFinder
    {
        private readonly List<Coordinates> dirs =
        [
            new(0, -1), 
            new(0, 1), 
            new(1, 0), 
            new(-1, 0) 
        ];
        
        public List<Coordinates> FindNeighbor(Map map, Coordinates startCoordinate)
        {   
            List<Coordinates> neighbors = [];
            foreach (var c in dirs)
            {
                Coordinates neighbor = new(startCoordinate.X + c.X, startCoordinate.Y + c.Y);
                if (map.IsOutOfBounds(neighbor))
                {
                    continue;
                }
               
                neighbors.Add(neighbor);
            }

            return neighbors;
        }
    }
}
