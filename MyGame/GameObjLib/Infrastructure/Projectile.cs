using System;
using static System.Console;

namespace GameObjLib
{
    public class Projectile : GameObjectDynamic
    {
        bool dirToDown;
        // Constructor
        public Projectile(int x = 0, int y = 0, bool dirToDown = false) : base(x, y)
        {
            this.dirToDown = dirToDown;
            IsAlive = false;
        }
        // Update projectile
        public override void Update()
        {
            if (dirToDown == false)
            {
                if (Y <= 1)
                    IsAlive = false;
                else
                {
                    Y -= 1;
                }
            }
            else if (dirToDown == true)
            {
                if (Y >= (WindowHeight - 1))
                    IsAlive = false;
                else
                {
                    Y += 1;
                }
            }
        }
        // Draw projectile
        public override void Draw()
        {
            ForegroundColor = ConsoleColor.White;
            if (IsAlive)
            {
                SetCursorPosition(X, Y);
                Write("*");
                //CursorLeft -= 1;
            }
        }
        public override void Clear()
        {
            SetCursorPosition(X, Y);
            Write(" ");
            CursorLeft -= 1;
        }
        public override void Collide(GameObjectDynamic[] objs)
        {
            for (int i = 0; i < objs.Length; ++i)
                if ((this != null && objs[i] != null) && (IsAlive && objs[i].IsAlive))
                {
                    // I think that hardcoding is the best variant for Console-Game check collision method
                    if ((X == objs[i].X && Y == objs[i].Y) ||
                        ((X + 1) == objs[i].X && Y == objs[i].Y) ||
                        ((X + 2) == objs[i].X && Y == objs[i].Y) ||
                        ((X) == (objs[i].X + 1) && Y == objs[i].Y) ||
                        ((X) == (objs[i].X + 2) && Y == objs[i].Y))
                    {
                        IsAlive = false;
                        objs[i].IsAlive = false;
                    }
                }
        }
    }
}
