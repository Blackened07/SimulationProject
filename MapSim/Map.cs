using Simulation.MapSim.Entities;
using Simulation.MapSim.Entities.Creatures;
using Simulation.MapSim.Entities.StaticEntities;

namespace Simulation.MapSim
{
    internal class Map(Dictionary<Coordinates, Entity> map, int height, int width)
    {
        public int Height 
        {
            get { return height; }
        }
        public int Width 
        { 
            get { return width; }
        }

        public Dictionary<Coordinates, Entity> getMap()
        {
            return map;
        }
        public Entity GetEntity(Coordinates coordinates)
        {
            if (map.TryGetValue(coordinates, out Entity entity))
            {
                return entity;
            }
            return new Earth();
        }

        public IEnumerable<Entity> GetEntitiesByType(Type t)
        {
            return map.Values.Where(e => t.IsAssignableFrom(e.GetType()));
        }

        public void SetEntity(Coordinates startCoordinates, Coordinates destCoordinates, Entity entity)
        {
            map[destCoordinates] = entity;
            map[startCoordinates] = new Earth();
        }
        public void Put(Coordinates coordinates, Entity entity)
        {
            map[coordinates] = entity;
        }

        public Coordinates GetPlayerCoordinates()
        {
            foreach (KeyValuePair<Coordinates, Entity> kvp in map)
            {
                if (kvp.Value is Player)
                {
                    return kvp.Key;
                }
            }
            throw new EntryPointNotFoundException("Entity not fount");
        }

        public Coordinates GetCoordinates(Entity entity)
        {
            foreach(KeyValuePair<Coordinates, Entity> kvp in map)
            {
                if (kvp.Value ==  entity)
                {
                    return kvp.Key;
                }    
            }
            throw new EntryPointNotFoundException("Entity not fount: " + entity.GetType());
        }

        public List<Creature> GetEntities()
        {
            return map.Values.OfType<Creature>().ToList();
        }

        public List<Entity> GetAllEntities()
        {
            return map.Values.OfType<Entity>().ToList();
        }

        public bool IsOccupied(Coordinates coordinates)
        {
            return map.ContainsKey(coordinates) && GetEntity(coordinates) is not Earth;
        }

        public bool IsOutOfBounds(Coordinates destCoordinates)
        {
            return destCoordinates.X < 0 || destCoordinates.X >= Width || 
                destCoordinates.Y < 0 || destCoordinates.Y >= Height;
        }

        
    }
}
