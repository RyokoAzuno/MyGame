using GameObjLib;
using System;
using System.Runtime.InteropServices;

using static System.Console;

namespace MyGame
{
    public class Win32Invoker
    {
        [DllImport("winmm.dll", EntryPoint = "timeGetTime")]
        public static extern uint TimeGetTime();
    }
    public class Game : IDrawable, IUpdatable, IRemovable
    {
        const double GameSpeed = 33.33d;
        double _frameCount;
        double _startTime;
        double _lastTime;
        double _fps;

        bool isPaused;
        GameObjectDynamic player;
        Score score;
        // Lives object for player and enemy classes
        Lives playerLives;
        Lives enemyLives;
        // Array of Enemies
        GameObjectDynamic[] enemies;
        GameObjectDynamic enemyBoss;
        // Random for randomize enemies positions
        Random rnd;

        public Game(int width, int height, int cursorSize, bool visible)
        {
            SetWindowSize(width, height);
            SetBufferSize(width, height);
            Title = $"My Game";
            CursorSize = cursorSize;
            CursorVisible = visible;
            _startTime = Win32Invoker.TimeGetTime(); //DateTime.Now.Millisecond
            _frameCount = 0;
            _fps = 0d;
            isPaused = false;
            rnd = new Random();
            score = new Score(0, 39);
            playerLives = new Lives(70, 39, 3, 5);
            enemyLives = new Lives(0, 0, 3, 20);
            player = new Player(40, 35, 3, score, playerLives);
            enemies = new Enemy[8];
            for (int i = 0; i < enemies.Length; ++i)
            {
                enemies[i] = new Enemy() { Score = score };
                enemies[i].IsAlive = false;
            }
            enemyBoss = new EnemyBoss(20, 0, 2, score, enemyLives);
        }
        protected bool GetInput(ref ConsoleKeyInfo keyInfo)
        {
            if (KeyAvailable)
            {
                keyInfo = ReadKey(true);
                return true;
            }
            return false;
        }

        public bool Run()
        {
            //ConsoleKeyInfo key = new ConsoleKeyInfo();
            DrawMenu();
            ConsoleKeyInfo key = ReadKey(true);
            if (key.Key == ConsoleKey.D1)
            {
                Clear();
                while (key.Key != ConsoleKey.Escape)
                {
                    while (!GetInput(ref key))
                    {
                        double currTime = Win32Invoker.TimeGetTime() - _lastTime;
                        if (currTime < GameSpeed)
                            continue;
                        // Clear section
                        Remove();

                        switch (key.Key)
                        {
                            case ConsoleKey.RightArrow: player.MoveRight(); break;
                            case ConsoleKey.LeftArrow: player.MoveLeft(); break;
                            case ConsoleKey.UpArrow: player.MoveUp(); break;
                            case ConsoleKey.DownArrow: player.MoveDown(); break;
                            case ConsoleKey.Spacebar: ((Player)player).Fire(); break;
                            case ConsoleKey.P: isPaused = !isPaused; break;
                            default: break;
                        }
                        key = new ConsoleKeyInfo();

                        if (!isPaused)
                        {
                            // Update player section
                            Update();
                        }
                        // Draw section
                        Draw();
                        _frameCount++;
                        _lastTime = Win32Invoker.TimeGetTime();
                    }
                    _fps = _frameCount / ((Win32Invoker.TimeGetTime() - _startTime) / 1000);
                    Title = $"Game speed: {_fps:0.##} fps";
                    //WriteLine($"Here's what you pressed: {key}");
                }
            }
            WriteLine($"{(_frameCount / ((Win32Invoker.TimeGetTime() - _startTime) / 1000)):0.##} fps");
            //WriteLine($"{Win32Interop.TimeGetTime() - _startTime} ms");
            //WriteLine($"Frame count: {_frameCount}");
            WriteLine("End of the game");

            return true;
        }
        // Draw all game objects(enemies, player, projectiles...)
        public void Draw()
        {
            if (player.IsAlive && enemyBoss.NumLives <= 0)
            {
                score.DrawWin();
            }
            else if ((enemyBoss.IsAlive && player.NumLives <= 0) || player.NumLives <= 0)
            {
                score.DrawGameOver();
            }
            else
            {
                foreach (var item in enemies)
                    if (item.IsAlive)
                        item.Draw();
                enemyBoss.Draw();
                //player.Clear();
                player.Draw();
                //score.Draw();
            }
            Title = $"Game speed: {_fps:0.##} fps";
        }
        // Update all game objects(enemies, player, projectiles...)
        public void Update()
        {
            UpdateEnemies(enemies);
            enemyBoss.Collide(player);
            player.Collide(enemies, enemyBoss);
            player.Update();
        }
        // Create Enemies if array element equals null or not alive 
        void UpdateEnemies(GameObjectDynamic[] enemies)
        {
            // Update enemy status IsAlive from false to true and set new position
            for (int i = 0; i < enemies.Length; ++i)
            {
                if (enemies[i].IsAlive == false)
                {
                    enemies[i].IsAlive = true;
                    enemies[i].X = rnd.Next(WindowTop, WindowWidth - 6);
                    enemies[i].Y = 0;
                    break;
                }
            }
            // Update all enemies
            foreach (var item in enemies)
            {
                if (item.IsAlive)
                    item.Update();
            }
            // Update Enemy Boss 
            enemyBoss.Update();
        }

        public void Remove()
        {
            player.Clear();
            enemyBoss.Clear();
            foreach (var item in enemies)
                item.Clear();
        }
        void DrawMenu()
        {
            ForegroundColor = ConsoleColor.White;
            SetCursorPosition(20, 10);
            Write("**********************************");
            WriteLine();
            SetCursorPosition(20, 11);
            Write("-------------GAME MENU------------");
            WriteLine();
            SetCursorPosition(20, 12);
            Write("**********************************");
            WriteLine();
            SetCursorPosition(20, 13);
            Write("-----Press 1 for Start Game-------");
            WriteLine();
            SetCursorPosition(20, 14);
            Write("**********************************");
            WriteLine();
            SetCursorPosition(20, 15);
            Write("-----Press Any key for Exit-------");
            WriteLine();
            SetCursorPosition(20, 16);
            Write("**********************************");
        }
    }
}
