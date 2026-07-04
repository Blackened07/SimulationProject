using Simulation.MapSim.Entities.Creatures;
using Simulation.Path;
using Simulation.MapSim;

namespace Simulation.Actions.Move
{
    internal class MoveAction : IAction
    {
        private readonly IPathFinder finder;

        public MoveAction(IPathFinder finder)
        {
            this.finder = finder;
        }

        public void Execute(Map map)
        {
            List<Creature> creatures = map.GetEntities();

            foreach (Creature creature in creatures)
            {
                creature.MakeMove(map, finder);
            }
        }
    }
}
