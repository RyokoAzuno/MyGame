namespace GameObjLib
{
    // If you want your game object will be able to collide with something
    // implement this interface
    public interface ICollidable<in T> where T : class
    {
        void Collide(T[] objs);
        void Collide(T[] objs1, T[] objs2);
        void Collide(T[] objs, T obj);
        void Collide(T obj);
    }
}
