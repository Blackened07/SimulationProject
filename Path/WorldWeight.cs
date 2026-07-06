

namespace Simulation.Path
{
    internal static class WorldWeight
    {
        public const float Earth = 0f;
        public const float Grass = 15f;
        public const float PredatorForHerbivores = -60f;
        public const float PredatorForPredators = -40f;
        public const float Boss = -200f;
        public const float Herbivore = 65f;
    }
}
