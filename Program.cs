using Simulation;
using Simulation.Actions;
using Simulation.Actions.Move;
using Simulation.Actions.Spawn;
using Simulation.MapSim;
using Simulation.Path;
using Simulation.Ui;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
    
        //Добавить ловушки с едой (либо хилит либо бьёт)
      
        //Добавить атаку игроку!

        //Поменять механику!!! если мало хп - увеличиваем скорость

        //Добавить эволюцию! 
        ConsoleRenderer c = new();
        Map map = MapFactory.GetMapInstance(20, 30);

        INeighborFinder neighborFinder = new NeighborFinderWithFourDirs();

        IPathFinderWeight finder = new BFSPAthFinderWithWeight(neighborFinder);
        IAction move = new MoveAction(finder);

        RandomFreeCoordinatesGenerator generator = new RandomFreeCoordinatesGenerator();
        //SpawnAction generalSpawn = new SpawnAction(generator);
        IAction init = new InitAction(generator);
        IAction respawn = new RespawnAction(generator);

        List<IAction> initActions = [init];
        List<IAction> turnActions = [move, respawn];

        Game g = new(map, c, turnActions, initActions);
        g.Start();
    }
}