
using static System.Console;

namespace GameObjLib
{
    // Base class for all objects in the game
    public abstract class GameObject
    {
        protected int x;            // Position x
        protected int y;            // Position y

        public virtual int X
        {
            get { return x; }
            set
            {
                if (value < (WindowWidth - 2) && value > WindowLeft)
                    x = value;
            }
        }
        public virtual int Y
        {
            get { return y; }
            set
            {
                if (value < WindowHeight && value >= WindowTop)
                    y = value;
            }
        }
        // Constructor
        public GameObject(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }
        // Update GameObject
        public abstract void Update();
        // Draw GameObject
        public abstract void Draw();
        // Remove GameObject
        public abstract void Clear();
    }
}
