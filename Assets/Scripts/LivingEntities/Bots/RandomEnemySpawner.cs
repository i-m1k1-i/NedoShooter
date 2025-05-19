using UnityEngine;

namespace Assets.Scripts.LivingEntities.Bots
{
	public class RandomEnemySpawner : MonoBehaviour
	{
        [SerializeField] private Settings _settings;
		[SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private bool _isSpawning = true;

        private int _currentEnemyCount = 0;
        private float _lastSpawnTime = 0f;

        public Vector3 RandomSpawnPoint =>
            new Vector3(
                Random.Range(_settings.MinSpawnPoint.position.x, _settings.MaxSpawnPoint.position.x),
                Random.Range(_settings.MinSpawnPoint.position.y, _settings.MaxSpawnPoint.position.y),
                Random.Range(_settings.MinSpawnPoint.position.z, _settings.MaxSpawnPoint.position.z)
            );


        private void Update()
        {
            if (_isSpawning == false)
                return;
            if (Time.timeSinceLevelLoad - _lastSpawnTime < _settings.SpawnInterval)
                return;
            if (_currentEnemyCount >= _settings.MaxEnemies)
            {
                if (_enemyPool.Pool.CountActive == 0)
                {
                    _currentEnemyCount = 0;
                    _lastSpawnTime = Time.timeSinceLevelLoad;
                }

                return;
            }

            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            var enemy = _enemyPool.Pool.Get();
            enemy.transform.position = RandomSpawnPoint;
            enemy.gameObject.SetActive(true);
            _currentEnemyCount++;
            _lastSpawnTime = Time.timeSinceLevelLoad;
        }

        [System.Serializable]
        public class Settings
		{
            public float SpawnInterval = 2f;
            public int MaxEnemies = 15;
            public Transform MinSpawnPoint;
            public Transform MaxSpawnPoint;
        }
    }
}