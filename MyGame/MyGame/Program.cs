using GameObjLib;
using System;

using static System.Console;

namespace MyGame
{
    class Program
    {
            
        static void Main(string[] args)
        {
            Game game = new Game(80, 40, 1, false);
            game.Run();
            //Score score = new Score(0, 39);
            //Lives playerLives = new Lives(70, 39, 3, 5);
            //GameObjectDynamic player = new Player(40, 35, 3, score, playerLives);
            //bool flag = true;
            //int gameSpeed = 33; 
            //while (flag)
            //{
            //    // If keyboard key is pressed
            //    if (KeyAvailable)
            //    {
            //        // Get pressed key info
            //        ConsoleKeyInfo key = ReadKey(true);
            //        switch (key.Key)
            //        {
            //            case ConsoleKey.RightArrow: player.MoveRight(); break;
            //            case ConsoleKey.LeftArrow: player.MoveLeft(); break;
            //            case ConsoleKey.UpArrow: player.MoveUp(); break;
            //            case ConsoleKey.DownArrow: player.MoveDown(); break;
            //            //case ConsoleKey.Spacebar: player.Fire(); break;
            //            case ConsoleKey.OemPlus: gameSpeed += 10; break;
            //            case ConsoleKey.OemMinus:
            //                {
            //                    if (gameSpeed > 10)
            //                        gameSpeed -= 10;
            //                }
            //                break;
            //            case ConsoleKey.Escape: flag = false;  break;
            //            default: break;
            //        }
            //        //player.Update();
                    
            //    }
            //    player.Draw();
            //    System.Threading.Thread.Sleep(gameSpeed);
            //    player.Clear();
            //    Title = "Game speed: " + gameSpeed.ToString();
            //}
        }
        void HandleInputs()
        {
            ConsoleKeyInfo key = ReadKey(true);
            //Console.CursorLeft -= 1;
            switch (key.Key)
            {
                
                default: break;
            }

        }

    }
}
