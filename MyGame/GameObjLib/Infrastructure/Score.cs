using System;
using static System.Console;

namespace GameObjLib
{
    public class Score : GameObjectStatic
    {
        // Public property for getting and setting number of points
        public virtual int Points { get; set; }
        // Constructor
        public Score(int x = 0, int y = 0) : base(x, y)
        {
            Points = 0;
        }
        public override void Update()
        {
            throw new NotImplementedException();
        }
        // Draw score points
        public override void Draw()
        {
            ForegroundColor = ConsoleColor.Magenta;
            SetCursorPosition(X, Y);
            Write("Score: {0}", Points);
            CursorLeft -= 1;
        }
        public override void Clear()
        {
            throw new NotImplementedException();
        }
        // Draw Game Over mini-state
        public void DrawGameOver()
        {
            SetCursorPosition(WindowWidth / 2 - 11, WindowHeight / 2);
            WriteLine("!!!!!!!!!GAME OVER!!!!!!!!!");
        }
        // Draw Game Winner mini-state
        public void DrawWin()
        {
            SetCursorPosition(WindowWidth / 2 - 11, WindowHeight / 2 - 2);
            WriteLine("!!!!!!!!!CONGRATULATIONS!!!!!!!!!");
            SetCursorPosition(WindowWidth / 2 - 11, (WindowHeight / 2) - 1);
            WriteLine("!!!!!!!!!YOU ARE THE WINNER!!!!!!!!!");
            SetCursorPosition(WindowWidth / 2 - 11, (WindowHeight / 2));
            WriteLine("!!!!!!!!!YOU SCORE: {0}!!!!!!!!!!", Points);
        }
    }
}
