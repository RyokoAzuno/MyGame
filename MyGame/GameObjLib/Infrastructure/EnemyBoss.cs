using System;
using System.Diagnostics;
using static System.Console;

namespace GameObjLib
{
    enum Dir { LEFT, RIGHT, UP, DOWN, DLEFT, DRIGHT, DUP, DDOWN }

    public class EnemyBoss : GameObjectDynamic, IFireable
    {
        Dir dir = Dir.RIGHT;
        public GameObjectDynamic[] bulletsR;    // Right weapon bullets(projectiles)
        GameObjectDynamic[] bulletsL;           // Left weapon bullets
        Score score;                            // Player's score
        int numProjs;                           // Number of projectiles in da weapons
        Lives lives;                            // Nuber of lives
        Stopwatch sw = new Stopwatch();         // Special timer class instance
        static long startTime;                  // Start time value
        static long endTime;                    // End time value
        //bool dirRight;                        // Enemy boss direction(true = right; false = left)
        // Constructor
        public EnemyBoss(int x = 0, int y = 0, int numProjs = 0, Score score = null, Lives lives = null)
            : base(x, y)
        {
            this.numProjs = numProjs;
            bulletsR = new Projectile[this.numProjs];
            bulletsL = new Projectile[this.numProjs];
            for (int i = 0; i < this.numProjs; ++i)
            {
                bulletsR[i] = new Projectile(0, 0, true);
                bulletsL[i] = new Projectile(0, 0, true);
            }
            //dirRight = true;
            IsAlive = false;
            this.score = score;
            this.lives = lives;
            NumLives = lives.NumLives;
            sw.Start();
            startTime = sw.ElapsedMilliseconds;
        }
        // Update enemy boss
        public override void Update()
        {
            if (score.Points == 2300)
                IsAlive = true;
            //endTime = sw.ElapsedMilliseconds - startTime;
            if (IsAlive && score.Points >= 2300)
            {
                if (score.Points >= 3500 && score.Points <= 4500)
                {
                    if (dir == Dir.DRIGHT)
                    {
                        if (X + 16 >= WindowWidth)
                            dir = Dir.DLEFT;
                        else
                        {
                            //MoveRight();
                            //MoveRight();
                            dir = Dir.DRIGHT;
                        }
                    }
                    else
                    {
                        if (X <= 1)
                            dir = Dir.DRIGHT;
                        else
                        {
                            //MoveLeft();
                            //MoveLeft();
                            dir = Dir.DLEFT;
                        }
                    }
                }
                else if (score.Points >= 4500)
                {
                    if (dir != Dir.UP)
                    {
                        if ((Y + 7) <= WindowHeight)
                        {
                            dir = Dir.DOWN;
                            //MoveDown();
                        }
                        else
                        {
                            dir = Dir.UP;
                            //MoveUp();
                        }
                    }
                    if (Y <= 0 && dir == Dir.UP)
                    {
                        dir = Dir.DLEFT;
                        if (dir == Dir.RIGHT)
                        {
                            if (X + 16 >= WindowWidth)
                                dir = Dir.DLEFT;
                            else
                            {
                                //MoveRight();
                                dir = Dir.RIGHT;
                            }
                        }
                        else
                        {
                            if (X <= 1)
                                dir = Dir.RIGHT;
                            else
                            {
                                //MoveLeft();
                                dir = Dir.DLEFT;
                            }
                        }
                    }
                }
                else
                {
                    if (dir == Dir.RIGHT)
                    {
                        if (X + 16 >= WindowWidth)
                            dir = Dir.LEFT;
                        else
                        {
                            //MoveRight();
                            dir = Dir.RIGHT;
                        }
                    }
                    else
                    {
                        if (X <= 1)
                            dir = Dir.RIGHT;
                        else
                        {
                            //MoveLeft();
                            dir = Dir.LEFT;
                        }
                    }
                }
                //foreach (var item in bulletsR)
                //{
                //    if (endTime >= 50)
                //    {
                //        Fire();
                //        startTime = endTime;
                //        item.Update();
                //        //sw.Reset();
                //    }
                //}
                switch (dir)
                {
                    case Dir.LEFT: MoveLeft(); break;
                    case Dir.RIGHT: MoveRight(); break;
                    case Dir.UP: MoveUp(); break;
                    case Dir.DOWN: MoveDown(); break;
                    case Dir.DLEFT: MoveLeft(); MoveLeft(); break;
                    case Dir.DRIGHT: MoveRight(); MoveRight(); break;
                    case Dir.DUP: MoveUp(); MoveUp(); break;
                    case Dir.DDOWN: MoveDown(); MoveDown(); break;
                    default: break;
                }
                for (int i = 0; i < bulletsR.Length; ++i)
                {
                    //if (endTime >= 10)
                    {
                        Fire();
                        startTime = endTime;
                        bulletsR[i].Update();
                        bulletsL[i].Update();
                        //sw.Reset();
                    }
                }
                lives.NumLives = NumLives;
            }
        }
        // Draw enemy boss
        public override void Draw()
        {
            if (IsAlive && score.Points >= 1500)
            {
                //foreach (var item in bulletsR)
                //{
                //    item.Draw();
                //}
                //foreach (var item in bulletsL)
                //{
                //    item.Draw();
                //}
                for (int i = 0; i < bulletsR.Length; ++i)
                {
                    bulletsR[i].Draw();
                    bulletsL[i].Draw();
                }
                DrawShip();
                //lives.Clear();
                lives.Draw();
            }
            //if (this.Alive && this.score.Points >= 2500)
            //{
            //    for (int i = 0; i < bulletsR.Length; ++i)
            //    {
            //        bulletsR[i].Draw();
            //        bulletsL[i].Draw();
            //    }
            //    DrawShip();
            //}
        }
        public override void Clear()
        {
            if (Y < (WindowHeight - 6))
            {
                SetCursorPosition(X, Y);
                Write("            ");
                WriteLine();
                SetCursorPosition(X, Y + 1);
                Write("               ");
                WriteLine();
                SetCursorPosition(X, Y + 2);
                Write("               ");
                WriteLine();
                SetCursorPosition(X, Y + 3);
                Write("               ");
                WriteLine();
                SetCursorPosition(X, Y + 4);
                Write("           ");
                WriteLine();
                SetCursorPosition(X, Y + 5);
                Write("            ");
                WriteLine();
                SetCursorPosition(X, Y + 6);
                Write("           ");
            }
            CursorLeft -= 1;
            for (int i = 0; i < bulletsR.Length; ++i)
            {
                bulletsR[i].Clear();
                bulletsL[i].Clear();
            }
            lives.Clear();
        }
        // Checking collisions between the enemy boss's projectiles and player
        public override void Collide(GameObjectDynamic obj)
        {
            for (int i = 0; i < bulletsR.Length; ++i)
            {
                if (bulletsR[i].IsAlive && obj.IsAlive)
                {
                    if ((bulletsR[i].X >= obj.X - 1 && bulletsR[i].X <= obj.X + 2) && bulletsR[i].Y >= obj.Y)
                    {
                        if (obj.NumLives > 0)
                        {
                            obj.NumLives -= 1;
                            bulletsR[i].IsAlive = false;
                        }
                    }
                }
            }
            for (int i = 0; i < bulletsL.Length; ++i)
            {
                if (bulletsL[i].IsAlive && obj.IsAlive)
                {
                    if ((bulletsL[i].X >= obj.X - 1 && bulletsL[i].X <= obj.X + 2) && bulletsL[i].Y >= obj.Y)
                    {
                        if (obj.NumLives > 0)
                        {
                            obj.NumLives -= 1;
                            bulletsL[i].IsAlive = false;
                        }
                    }
                }
            }
        }
        // Draw enemy boss ship
        void DrawShip()
        {
            //lives.Draw();
            if (Y < (WindowHeight - 6))
            {
                ForegroundColor = ConsoleColor.Red;
                SetCursorPosition(X, Y);
                Write("   -^-^-^-^-");
                WriteLine();
                SetCursorPosition(X, Y + 1);
                Write("***-^-^-^-^-***");
                WriteLine();
                SetCursorPosition(X, Y + 2);
                Write("***-^-^-^-^-***");
                WriteLine();
                SetCursorPosition(X, Y + 3);
                Write("***-^-^-^-^-***");
                WriteLine();
                SetCursorPosition(X, Y + 4);
                Write("    --vvv--");
                WriteLine();
                SetCursorPosition(X, Y + 5);
                Write("    \\-----/");
                WriteLine();
                SetCursorPosition(X, Y + 6);
                Write("     \\---/");
            }
            CursorLeft -= 1;
        }
        // Fire projectiles
        public void Fire()
        {
            endTime = sw.ElapsedMilliseconds - startTime;
            if (endTime >= 10)
            {
                RightWeapFire();
                LeftWeapFire();
            }
        }
        // Right weapon fire
        void RightWeapFire()
        {
            int px = X + 10;
            int py = Y + 7;
            for (int i = 0; i < numProjs; ++i)
                if (bulletsR[i].IsAlive == false)
                {
                    bulletsR[i].IsAlive = true;
                    bulletsR[i].X = px;
                    bulletsR[i].Y = py;
                    break;
                }
        }
        // Left weapon fire
        void LeftWeapFire()
        {
            int px = X + 5;
            int py = Y + 7;
            for (int i = 0; i < numProjs; ++i)
                if (bulletsL[i].IsAlive == false)
                {
                    bulletsL[i].IsAlive = true;
                    bulletsL[i].X = px;
                    bulletsL[i].Y = py;
                    break;
                }
        }
    }
}
