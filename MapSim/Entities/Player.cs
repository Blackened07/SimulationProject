namespace Simulation.MapSim.Entities
{
    internal class Player : Entity
    {
        public int Health { get; set; }

        public void TakeDamage(int damage)
        {
            this.Health = Math.Max(0, this.Health - damage);
        }
    }
}
