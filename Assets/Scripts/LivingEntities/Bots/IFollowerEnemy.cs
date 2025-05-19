namespace Nedoshooter.Enemies
{
    public interface IFollowerEnemy : IEnemyAttack, IFollower
    {
        bool CanSeePlayer();
    }
}
