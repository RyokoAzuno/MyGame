using System;
using static System.Console;

namespace GameObjLib
{
    public class Enemy : GameObjectDynamic
    {
        public Score Score { get; set; }
        Random rnd = new Random();
        // Constructor with two arguments
        public Enemy(int x = 0, int y = 0, Score score = null) : base(x, y)
        {
            Score = score;
            IsAlive = true;
        }
        // Update enemy position
        public override void Update()
        {
            if (IsAlive && (Y <= WindowHeight - 2) && Score.Points < 1500)
            {
                MoveDown();
                //MoveWave();
            }
            else if (IsAlive && (Y <= WindowHeight - 2) && (Score.Points >= 1500 && Score.Points < 2300))
            {
                MoveWave();
            }
            else
            {
                IsAlive = false;
            }

        }
        // Draw enemy
        public override void Draw()
        {
            if (IsAlive && Score.Points < 2300)
            {
                ForegroundColor = ConsoleColor.Red;
                SetCursorPosition(X, Y);
                Write("-|-");
                CursorLeft -= 1;
            }
        }
        public override void Clear()
        {
            SetCursorPosition(X, Y);
            Write("   ");
            CursorLeft -= 1;
        }
        // Move enemy down
        public override void MoveDown()
        {
            if (Score.Points >= 1300) 
                Y += 1;
            Y += 1;
        }
        // Move enemy as an oscillator
        void MoveWave()
        {
            if(X < (WindowWidth - 4))
                X += rnd.Next(-1, 2);
            Y += 1;
        }
        public void Create()
        {
            if (IsAlive == false)
            {
                IsAlive = true;
                X = rnd.Next(WindowTop, WindowWidth - 6);
                Y = 0;
            }
        }
    }
}
