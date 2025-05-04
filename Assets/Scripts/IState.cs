namespace Nedoshooter
{
    public interface IState
    {
        void Enter();
        void Update();
        void Exit();
    }
}
