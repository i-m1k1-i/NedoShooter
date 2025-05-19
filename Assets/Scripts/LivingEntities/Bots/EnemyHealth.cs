public class EnemyHealth : Health
{
    private static EnemyPool _enemyPool;

    private void Start()
    {
        if (_enemyPool == null)
        {
            _enemyPool = FindAnyObjectByType<EnemyPool>();
        }
    }

    protected override void Die()
    {
        _enemyPool.ReturnToPool(this);
    }
}
