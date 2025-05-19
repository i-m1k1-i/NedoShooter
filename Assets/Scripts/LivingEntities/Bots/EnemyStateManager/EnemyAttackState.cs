namespace Nedoshooter.Enemies.StateManager
{
    public class EnemyAttackState : EnemyState
    {
        public EnemyAttackState(Enemy enemy) : base(enemy)
        {
        }

        public override void Enter()
        {
        }

        public override void Update()
        {
            if (_enemy.IsPlayerInAttackRange() == false)
            {
                if (_enemy.CanSeePlayer())
                {
                    _stateManager.ChangeState(EnemyStates.Follow);
                }
                else
                {
                    _stateManager.ChangeState(EnemyStates.Patrol);
                }
            }
            else
            {
                _enemy.Attack();
            }
        }

        public override void Exit()
        {
        }
    }
}
