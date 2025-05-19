using UnityEngine;
using UnityEngine.AI;

namespace Nedoshooter.Enemies.StateManager
{
    public abstract class EnemyState : IEnemyState
    {
        protected Enemy _enemy;
        protected Transform _self;
        protected NavMeshAgent _agent;

        protected ITarget _target => _enemy.Target;
        protected EnemyStateManager _stateManager => _enemy.StateManager;

        public EnemyState(Enemy enemy)
        {
            _enemy = enemy;
            _self = enemy.transform;
            _agent = enemy.Agent;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
    }
}
