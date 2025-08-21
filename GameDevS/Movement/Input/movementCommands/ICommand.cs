namespace GameDevS.Movement.Input.movementCommands
{
    public interface ICommand
    {
        void Execute(IMovable movable, float dt);
    }
}
