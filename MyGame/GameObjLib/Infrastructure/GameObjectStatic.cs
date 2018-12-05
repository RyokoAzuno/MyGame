using System;
using System.Collections.Generic;
using System.Text;

namespace GameObjLib
{
    // Class for creating static game objects(not movable but can be collidable)
    public abstract class GameObjectStatic : GameObject
    {
        public GameObjectStatic(int x = 0, int y = 0) : base(x, y)
        {

        }
    }
}
