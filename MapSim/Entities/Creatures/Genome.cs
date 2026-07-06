
namespace Simulation.MapSim.Entities.Creatures
{
    internal struct Genome(float foodAttraction, float fearDanger)
    {
        public float FoodAttraction { get; set; } = foodAttraction;
        public float FearDanger { get; set; } = fearDanger;

        public readonly Genome Mutate()
        {
            float coeff = 0015f;

            float mutationRangeFood = this.FoodAttraction * coeff;
            float newFoodAttraction = Math.Max(2.0f, this.FoodAttraction + (Random.Shared.NextSingle() * 2f - 1f) * mutationRangeFood);

            float mutationRangeFear = this.FearDanger * coeff;
            float newFear = Math.Min(-2.0f, this.FearDanger + (Random.Shared.NextSingle() * 2f - 1f) * mutationRangeFear);

            return new Genome(newFoodAttraction, newFear);
        }
    }
}
