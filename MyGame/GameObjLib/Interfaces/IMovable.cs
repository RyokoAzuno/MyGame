namespace GameObjLib
{
    // If you want your game object will be able to move game objects right, left, up or down
    // implement this interface
    public interface IMovable
    {
        void MoveRight();
        void MoveLeft();
        void MoveUp();
        void MoveDown();
    }
}
