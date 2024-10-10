using System;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] int _enemiesNum = 10;
    [SerializeField] int _radius = 5;
    [SerializeField, Range(1, 10)] float _height;

    private ObjectPool<GameObject> _enemyPool;
    private void Start()
    {
        _enemyPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_enemyPrefab),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,  
            defaultCapacity: _enemiesNum,  
            maxSize: _enemiesNum
            );

        SpawnMax();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ReturnAllDied();
        }
    }

    private void SpawnMax()
    {
        float angleStep = 360 / _enemiesNum * Mathf.Deg2Rad;
        GameObject enemy;

        for (int i = 0; i <= _enemiesNum; i++)
        {
            float angle = angleStep * i;
            Vector2 localPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _radius;
            enemy = _enemyPool.Get();
            enemy.transform.position = new Vector3(transform.position.x + localPosition.x, _height, transform.position.z + localPosition.y);
            Debug.Log("enemy setted");
        }
    }

    private void ReturnAllDied() 
    {
        int inactiveAmount = _enemyPool.CountInactive;

        for (int i = 0; i < inactiveAmount; i++)
        {
            _enemyPool.Get();
        }
    }

    public void ReturnToPool(GameObject enemy)
    {
        _enemyPool.Release(enemy);
    }
}
