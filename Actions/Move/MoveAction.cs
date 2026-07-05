using Simulation.MapSim.Entities.Creatures;
using Simulation.Path;
using Simulation.MapSim;
using Simulation.MapSim.Entities.StaticEntities;

namespace Simulation.Actions.Move
{
    internal class MoveAction : IAction
    {
        private readonly IPathFinder finder;
        private readonly IPathFinderWeight finderWeight;

        public MoveAction(IPathFinderWeight finder)
        {
            this.finderWeight = finder;
        }

        public void Execute(Map map)
        {
            List<Creature> creatures = map.GetEntities();

            foreach (Creature creature in creatures)
            {
                creature.MakeMove(map, finderWeight);
            }

            List<Creature> afterMove = map.GetEntities();

            foreach (Creature creature in afterMove)
            {
                if (creature.IsDead())
                {
                    map.Put(map.GetCoordinates(creature), new Earth());
                }
            }

            creatures.RemoveAll(creature => creature.IsDead());
        }
    }
}
