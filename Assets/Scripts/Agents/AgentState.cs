
namespace Assets.Scripts.Agents
{
    public abstract class AgentState : IState
    {
        protected Agent _agent;

        public AgentState(Agent agent)
        {
            _agent = agent;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}
