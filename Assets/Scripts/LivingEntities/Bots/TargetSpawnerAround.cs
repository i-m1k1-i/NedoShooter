using System;
using UnityEngine;
using UnityEngine.Pool;
using Nedoshooter.Enemies;

public class TargetSpawnerAround : MonoBehaviour
{
    [SerializeField, Range(1, 10)] float _height;
    [SerializeField] int _targetNum;
    [SerializeField] int _radius = 5;
    [SerializeField] EnemyPool _targetPool;

    private void Start()
    {
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
        float angleStep = 360 / _targetNum * Mathf.Deg2Rad;

        for (int i = 1; i <= _targetNum; i++)
        {
            float angle = angleStep * i;
            Vector2 localPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _radius;
            EnemyHealth enemy = _targetPool.Pool.Get();
            enemy.transform.position = new Vector3(transform.position.x + localPosition.x, _height, transform.position.z + localPosition.y);
        }
    }

    private void ReturnAllDied()
    {
        int inactiveAmount = _targetPool.Pool.CountInactive;

        for (int i = 0; i < inactiveAmount; i++)
        {
            _targetPool.Pool.Get();
        }
    }
}