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
        // ВОЗВРАТ ИЗ ГЕТ ПЛЕЕР КОРДС - посмотреть что возвращать! Пока создаю новые координаты

        // setEntity - rename PutEntity

        //Добавить хищников, травоядных, траву и ловушки с едой (либо хилит либо бьёт)
        //Реализовать спавн при условии что мало сущностей (спавн экшн)
        // Добавить Интеракты, атаку и тд (придумать механики голод и прочее)
        //Добавить атаку игроку!

        //МЫСЛЬ:* Вероятно можно до спавна новых существ сохранять в классе определённые знания, которые потом можно применить при спавне нового поколения

        //Поменять механику!!! если мало хп - увеличиваем скорость
        ConsoleRenderer c = new();
        Map map = MapFactory.GetMapInstance(20, 30);

        INeighborFinder neighborFinder = new NeighborFinderWithFourDirs();

        IPathFinder finder = new BFSPathFinder(neighborFinder);
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