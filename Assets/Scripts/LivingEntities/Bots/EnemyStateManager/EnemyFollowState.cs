namespace Nedoshooter.Enemies.StateManager
{
    public class EnemyFollowState : EnemyState
    {
        public EnemyFollowState(Enemy enemy) : base(enemy)
        {
        }

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            if (_enemy.IsPlayerInAttackRange())
            {
                _stateManager.ChangeState(EnemyStates.Attack);
                return;
            }
            if (_enemy.CanSeePlayer() == false)
            {
                _stateManager.ChangeState(EnemyStates.Patrol);
                return;
            }

            _enemy.Follow(_target.Position);
        }
    }
}