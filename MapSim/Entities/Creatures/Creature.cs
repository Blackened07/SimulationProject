using Simulation.MapSim;
using Simulation.MapSim.Entities;
using Simulation.MapSim.Entities.StaticEntities;
using Simulation.Path;

namespace Simulation.MapSim.Entities.Creatures
{
    internal abstract class Creature : Entity
    {
        public readonly int MAX_ENERGY = 100;
        public int CurrentEnergy { get; set;}
        public int Health { get; set;}
        public int AttackPower { get; set;}
        public float Hunger { get; set;}
        public float Fear { get; set;}
        public Genome Dna { get; set;}
        
        public abstract float GetCellWeight(Entity entity, bool isCreature);
        public abstract bool IsTarget(Entity entity);
        public abstract bool IsEnemy(Entity entity);
        public abstract void InteractWithTarget(Map map, Entity target, Coordinates final);
        public void MakeMove(Map map, IPathFinderWeight finder)
        {
            if (this.IsDead())
            { 
                return;
            }

            if (this.Hunger >= 5 || this.CurrentEnergy == 0)
            {
                this.Health -= 1;
            }

            Coordinates currentCoordinates = map.GetCoordinates(this);

            Coordinates bestStep = finder.Find(map, this, currentCoordinates, coordinates => IsTarget(map.GetEntity(coordinates)), radius: 6);

            if (!bestStep.Equals(currentCoordinates))
            {
                if (IsTarget(map.GetEntity(bestStep))) 
                { 
                    Entity target = map.GetEntity(bestStep);
                    InteractWithTarget(map, target, bestStep);
                    
                    return;
                }
            }
            DecreaseVitality();
            MoveTo(map, currentCoordinates, bestStep, this);
        }

        public bool IsCreature(Entity entity)
        {
            return entity is Herbivore || entity is Predator || entity is Boss;
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
       
        protected void IncreaseVitality()
        {
            this.Hunger = 0f;
            if (this is Herbivore)
            {
                this.CurrentEnergy += 20;
            }

            if (this is Predator) 
            {
                this.CurrentEnergy += 9;
            }

            if (this.CurrentEnergy > MAX_ENERGY)
            {
                this.CurrentEnergy = MAX_ENERGY;
            }
        }
        protected void DecreaseVitality()
        {
            this.Hunger += 0.1f;

            if (this.Fear > 2.0f)
            {
                this.CurrentEnergy -= 4;
            }
            else
            {
                this.CurrentEnergy -= 2;
            }
           
        }
        private void MoveTo(Map map, Coordinates start, Coordinates next, Entity e)
        {
            map.SetEntity(start, next, e);
        }


      
    }
}
