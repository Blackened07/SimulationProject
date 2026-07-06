using Simulation.Actions;
using Simulation.MapSim;
using Simulation.MapSim.Entities;
using Simulation.MapSim.Entities.StaticEntities;
using Simulation.Ui;
using System.Numerics;

namespace Simulation
{
    internal class Game(Map map, ConsoleRenderer renderer, List<IAction> turnActions, List<IAction> initActions)
    {
        private bool isRunning = true;
        private int turnsCounter = 0;

        public void Start()
        {
            ExecuteActions(initActions);
            Console.CursorVisible = false;
            while(isRunning)
            { 

                NextTurn();
                ProcessPlayerMove();                   
                
                renderer.Render(map, turnsCounter++);
                Thread.Sleep(48);
            }
        }

        private void NextTurn()
        {
            ExecuteActions(turnActions);
        }

        private void ExecuteActions(List<IAction> actions)
        {
            foreach(var action in actions)
            {
                action.Execute(map);
            }
        }

        private void ProcessPlayerMove()
        {
            if (Console.KeyAvailable)
            {
                Coordinates coordinates = map.GetPlayerCoordinates();
                Entity player = map.GetEntity(coordinates);
                Coordinates destCoordinates;
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.W:
                        destCoordinates = new() { X = coordinates.X, Y = coordinates.Y - 1 };
                        Move(coordinates, destCoordinates, player);
                        break;
                    case ConsoleKey.S:
                        destCoordinates = new() { X = coordinates.X, Y = coordinates.Y + 1 };
                        Move(coordinates, destCoordinates, player);
                        break;
                    case ConsoleKey.A:
                        destCoordinates = new() { X = coordinates.X - 1, Y = coordinates.Y };
                        Move(coordinates, destCoordinates, player);
                        break;
                    case ConsoleKey.D:
                        destCoordinates = new() { X = coordinates.X + 1, Y = coordinates.Y };
                        Move(coordinates, destCoordinates, player);
                        break;
                    case ConsoleKey.Escape:
                        isRunning = false;
                        break;
                }
            }
        }

        private void Move(Coordinates startCoordinates, Coordinates destCoordinates, Entity player)
        {
            if (!map.IsOutOfBounds(destCoordinates))
            {
                Entity e = map.GetEntity(destCoordinates);
                if (e is Earth)
                {
                    map.SetEntity(startCoordinates, destCoordinates, player);
                   
                }
            }
        }

        
    }
}
