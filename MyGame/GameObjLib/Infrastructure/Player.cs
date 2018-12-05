using System;
using static System.Console;

namespace GameObjLib
{
    public class Player : GameObjectDynamic, IFireable
    {
        GameObjectDynamic[] bullets;    // bullets
        int numProjs;                   // Number of projectiles
        Score score;                    // Player's score
        Lives lives;                    // Player's lives

        // Constructor
        public Player(int x = 0, int y = 0, int numProjs = 0, Score score = null, Lives lives = null)
            : base(x, y)
        {
            this.numProjs = numProjs;
            bullets = new Projectile[this.numProjs];
            for (int i = 0; i < this.numProjs; ++i)
            {
                bullets[i] = new Projectile();
            }
            IsAlive = true;
            this.score = score;
            this.lives = lives;
            NumLives = lives.NumLives;
        }
        // Update player
        public override void Update()
        {
            foreach (var item in bullets)
            {
                item.Update();
            }
            lives.NumLives = NumLives;
        }
        // Draw player
        public override void Draw()
        {
            if (bullets != null)
                foreach (var item in bullets)
                {
                    item.Draw();
                }
            if (IsAlive)
            {
                ForegroundColor = ConsoleColor.Green;
                SetCursorPosition(X, Y);
                Write("+^+");
                CursorLeft -= 1;
                score.Draw();
                lives.Draw();
            }
        }
        public override void Clear()
        {
            SetCursorPosition(X, Y);
            Write("   ");
            CursorLeft -= 1;
            foreach (var item in bullets)
                item.Clear();
            lives.Clear();
        }
        public override void MoveRight()
        {
            if(X < (WindowWidth - 4))
                X += 1;
        }
        // Method-Flag: Calls when player is shooting
        // Create a new projectile and add it into the bullets list 
        public void Fire()
        {
            int px = X + 1;
            int py = Y;
            for (int i = 0; i < numProjs; ++i)
                if (bullets[i].IsAlive == false)
                {
                    bullets[i].IsAlive = true;
                    bullets[i].X = px;
                    bullets[i].Y = py;
                    break;
                }
        }
        // Checking collisions between player and other game objects
        public override void Collide(GameObjectDynamic[] objs)
        {
            base.Collide(objs);
            Collide(bullets, objs);
        }
        public override void Collide(GameObjectDynamic[] objs, GameObjectDynamic obj)
        {
            //CollideProjectiles(objs);
            for (int i = 0; i < objs.Length; ++i)
                if ((this != null && objs[i] != null) && (IsAlive && objs[i].IsAlive))
                {
                    // I think that hardcoding is the best way for this game to detect collisions
                    if ((X == objs[i].X && Y == objs[i].Y) || ((X + 1) == objs[i].X && Y == objs[i].Y) ||
                        ((X + 2) == objs[i].X && Y == objs[i].Y) || ((X) == (objs[i].X + 1) && Y == objs[i].Y) ||
                        ((X == (objs[i].X + 2)) && (Y == objs[i].Y)))
                    {
                        if (NumLives > 0)
                        {
                            //this.Alive = false;
                            NumLives -= 1;
                            objs[i].IsAlive = false;
                        }
                        else
                        {
                            IsAlive = false;
                            break;
                        }
                    }
                }
            if (IsAlive)
            {
                Collide(bullets, objs);
                // collide with boss
                for (int i = 0; i < bullets.Length; ++i)
                {
                    if (bullets[i].IsAlive && obj.IsAlive)
                    {
                        if ((bullets[i].X >= obj.X && bullets[i].X <= obj.X + 15) && bullets[i].Y <= obj.Y + 7)
                        {
                            if (obj.NumLives > 0)
                            {
                                obj.NumLives -= 1;
                                score.Points += 200;
                                bullets[i].IsAlive = false;
                            }
                        }
                    }
                }
            }
        }
        // Checking collisions between projectiles and enemies
        public override void Collide(GameObjectDynamic[] objs1, GameObjectDynamic[] objs2)
        {
            for (int i = 0; i < objs2.Length; ++i)
            {
                if (objs2[i].IsAlive)
                {
                    for (int j = 0; j < objs1.Length; ++j)
                    {
                        if (objs1[j].IsAlive)
                        {
                            if ((objs1[j].X == objs2[i].X || (objs1[j].X == (objs2[i].X + 1))
                                || (objs1[j].X == (objs2[i].X + 2))) && ((objs1[j].Y == objs2[i].Y)
                                || (objs1[j].Y == objs2[i].Y - 1)))
                            {
                                score.Points += 100;
                                objs1[j].IsAlive = false;
                                objs2[i].IsAlive = false;
                                //break;
                            }
                        }
                    }
                }
            }
        }
    }
}
