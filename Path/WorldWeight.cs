

namespace Simulation.Path
{
    internal static class WorldWeight
    {
        public const float Earth = 0f;
        public const float Grass = 50f;
        public const float PredatorForHerbivores = -75f;
        public const float PredatorForPredators = -10f;
        public const float Boss = -100f;
        public const float Herbivore = 50f;
    }
}
