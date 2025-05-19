using Nedoshooter.Enemies;
using Nedoshooter.Installers;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] EnemyHealth _enemyPrefab;

    private readonly int _defaultCapacity = 12;
    private readonly int _maxSize = 20;

    private ObjectPool<EnemyHealth> _targetPool;

    public ObjectPool<EnemyHealth> Pool => _targetPool;

    private void Awake()
    {
        _targetPool = new ObjectPool<EnemyHealth>(
            createFunc: CreateEnemy,
            actionOnGet: (enemy) => enemy.gameObject.SetActive(true),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy),
            collectionCheck: false,  
            defaultCapacity: _defaultCapacity,  
            maxSize: _maxSize
            );
    }

    private EnemyHealth CreateEnemy()
    {
        EnemyHealth enemyHealth = Instantiate(_enemyPrefab);
        Enemy enemy = enemyHealth.GetComponent<Enemy>();
        SampleInstaller.Instance.Inject(enemy);

        return enemyHealth;
    }

    public void ReturnToPool(EnemyHealth enemy)
    {
        _targetPool.Release(enemy);
    }
}
