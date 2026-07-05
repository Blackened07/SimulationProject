using Simulation.MapSim;

namespace Simulation.Ui
{
    internal interface IView
    {

        void Render(Map map, int turns);
    }
}
