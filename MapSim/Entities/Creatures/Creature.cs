using Simulation.MapSim;
using Simulation.MapSim.Entities;
using Simulation.Path;

namespace Simulation.MapSim.Entities.Creatures
{
    internal abstract class Creature : Entity
    {
        public int Health { get; set;}
        public int AttackPower { get; set;}
        public abstract bool IsTarget(Entity entity);
        public abstract void InteractWithTarget(Map map, Entity target, Coordinates final);
        public void MakeMove(Map map, IPathFinder finder)
        {
            if (this.IsDead()) return;

            Coordinates currentCoordinates = map.GetCoordinates(this);

            List<Coordinates> path = finder.Find(map, currentCoordinates, coordinates => IsTarget(map.GetEntity(coordinates)));
            if (!HasNextStep(path))
            {
                return;
            }
            
            Coordinates next = getReachablePosition(path);
            Coordinates final = getFinalPosition(path);
 
            if (CanInteractWithTarget(map, next, final))
            {
                Entity target = map.GetEntity(final);
                InteractWithTarget(map, target, final);
                return;
            }

            MoveTo(map, currentCoordinates, next, this);
        }
        public bool IsDead()
        {
            return this.Health <= 0;
        }
        protected void Attack(Creature target)
        {
            target.TakeDamage(this.AttackPower);
        }
        protected void TakeDamage(int damage)
        {
            this.Health = Math.Max(0, this.Health - damage);
        }
        private bool HasNextStep(List<Coordinates> path)
        {
            return path.Count > 1;
        }

        private void MoveTo(Map map, Coordinates start, Coordinates next, Entity e)
        {
            map.SetEntity(start, next, e);
        }

        private bool CanInteractWithTarget(Map map, Coordinates next, Coordinates final)
        {
            return next.Equals(final) && map.IsOccupied(final);
        }

        private Coordinates getFinalPosition(List<Coordinates> path)
        {
            return path[path.Count - 1];
        }

        private Coordinates getReachablePosition(List<Coordinates> path)
        {   //Min func is for creatures speed
            return path[Math.Min(1, path.Count - 1)];
        }

        
    }
}
