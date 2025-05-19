using System.Collections.Generic;
using Zenject;

namespace Nedoshooter.Enemies.StateManager
{
    public enum EnemyStates
    {
        Patrol,
        Follow,
        Attack,
        None
    }

    public class EnemyStateManager : ITickable
    {
        private IEnemyState _currentStateHandler;
        private EnemyStates _currentState = EnemyStates.None;

        private List<IEnemyState> _states;

        public EnemyStateManager(EnemyPatrolState patrol, EnemyFollowState follow, EnemyAttackState attack) 
        {
            _states = new List<IEnemyState>
            {
                patrol,
                follow,
                attack
            };
        }

        public void ChangeState(EnemyStates newState)
        {
            if (newState == _currentState)
            {
                return;
            }

            _currentState = newState;
            _currentStateHandler?.Exit();
            _currentStateHandler = _states[(int)newState];
            _currentStateHandler.Enter();
        }

        public void Tick()
        {
            _currentStateHandler?.Update();
        }
    }
}
