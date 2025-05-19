using UnityEngine;

namespace Nedoshooter.Enemies.StateManager
{
    public class EnemyPatrolState : EnemyState
    {
        private int _currentPoint = 0;

        public EnemyPatrolState(Enemy enemy) : base(enemy)
        {
        }

        public override void Enter()
        {
            SetRandomPatrolPoint();
        }

        public override void Exit()
        {

        }

        public override void Update()
        {
            if (_enemy.CanSeePlayer())
            {
                _stateManager.ChangeState(EnemyStates.Follow);
                return;
            }

            if (_agent.pathPending == false && _agent.remainingDistance < 0.5f)
            {
                SetRandomPatrolPoint();
            }
        }

        private void SetRandomPatrolPoint()
        {
            _currentPoint = Random.Range(0, _enemy.PatrolPoints.Length);
            _agent.SetDestination(_enemy.PatrolPoints[_currentPoint].position);
        }
    }
}