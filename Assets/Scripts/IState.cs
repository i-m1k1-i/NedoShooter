namespace Assets.Scripts
{
    public interface IState
    {
        void Enter();
        void Update();
        void Exit();
    }
}
