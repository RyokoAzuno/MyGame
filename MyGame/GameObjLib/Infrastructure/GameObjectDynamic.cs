namespace GameObjLib
{
    // Class for creating dynamic(movable, collidable) game objects
    public abstract class GameObjectDynamic : GameObject, IMovable, ICollidable<GameObjectDynamic>
    {
        public virtual bool IsAlive { get; set; }

        public virtual int NumLives { get; set; }
        // Constructor
        public GameObjectDynamic(int x = 0, int y = 0, int numLives = 1) : base(x, y)
        {
            NumLives = numLives;
        }

        public virtual void MoveRight() => X += 1;
        public virtual void MoveLeft() => X -= 1;
        public virtual void MoveUp() => Y -= 1;
        public virtual void MoveDown() => Y += 1;

        public virtual void Collide(GameObjectDynamic[] objs)
        {
            //CollideProjectiles(objs);
            for (int i = 0; i < objs.Length; ++i)
            {
                if ((this != null && objs[i] != null) && (IsAlive && objs[i].IsAlive))
                {
                    // !!!!I think that hardcoding is the best way for console games to detect collisions!!!!
                    if ((X == objs[i].X && Y == objs[i].Y) ||
                        ((X + 1) == objs[i].X && Y == objs[i].Y) ||
                        ((X + 2) == objs[i].X && Y == objs[i].Y) ||
                        ((X) == (objs[i].X + 1) && Y == objs[i].Y) ||
                        ((X) == (objs[i].X + 2) && Y == objs[i].Y))
                    {
                        if (NumLives > 0)
                        {
                            //this.Alive = false;
                            NumLives -= 1;
                            objs[i].IsAlive = false;
                        }
                    }
                }
            }
        }

        public virtual void Collide(GameObjectDynamic[] objs1, GameObjectDynamic[] objs2)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Collide(GameObjectDynamic[] objs, GameObjectDynamic obj)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Collide(GameObjectDynamic obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
