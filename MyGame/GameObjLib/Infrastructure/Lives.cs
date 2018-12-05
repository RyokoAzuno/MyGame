using System;
using static System.Console;

namespace GameObjLib
{
    public class Lives : GameObjectStatic
    {
        readonly char ch;
        public virtual int NumLives { get; set; }

        public Lives(int x = 0, int y = 0, int ch = 3, int numLives = 0) : base(x, y)
        {
            this.ch = (char)ch;
            NumLives = numLives;
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            ForegroundColor = ConsoleColor.Magenta;
            SetCursorPosition(X, Y);
            for (int i = 0; i < NumLives; ++i)
            {
                Write("{0}", ch);
            }
            if (NumLives > 0)
                CursorLeft -= 1;
        }

        public override void Clear()
        {
            SetCursorPosition(X, Y);
            for (int i = 0; i < NumLives; ++i)
            {
                Write(" ");
            }
        }
    }
}
