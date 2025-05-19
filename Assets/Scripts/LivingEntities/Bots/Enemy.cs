using UnityEngine;
using UnityEngine.AI;
using Nedoshooter.Enemies.StateManager;
using System;
using Zenject;
using Nedoshooter.Installers;

namespace Nedoshooter.Enemies
{
    public abstract class Enemy : MonoBehaviour, IFollowerEnemy, IReinjectable
	{
        [SerializeField] protected Settings _settings;

        [SerializeField] private Transform[] _patrolPoints;

        private EnemyStateManager _stateManager;

        public ITarget Target { get; set; }
        public NavMeshAgent Agent { get; private set; }
        public EnemyStateManager StateManager => _stateManager;
        public Transform[] PatrolPoints => _patrolPoints;

        public abstract void Attack();
        public abstract void Follow(Vector3 targetPosition);

        [Inject]
        private void Initialize(ITarget target)
        {
            SampleInstaller.Instance.RegisterReinjectable(this);
            Target = target;
            Debug.Log($"Target injected: {target}");
        }

        public void Reinject(DiContainer container)
        {
            container.Inject(this);
        }

        public bool CanSeePlayer()
        {
            return Vector3.Distance(transform.position, Target.Position) <= _settings.FollowRange;
        }

        public bool IsPlayerInAttackRange()
        {
            return Vector3.Distance(transform.position, Target.Position) <= _settings.AttackDistance;
        }

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();

            _stateManager = new EnemyStateManager(
                new EnemyPatrolState(this),
                new EnemyFollowState(this), 
                new EnemyAttackState(this)
                );
        }

        private void Start()
        {
            _stateManager.ChangeState(EnemyStates.Patrol);
        }

        private void Update()
        {
            _stateManager.Tick();
        }

        [Serializable]
        public class Settings
        {
            public float FollowRange;
            public float AttackDistance;
            public float FollowSpeed;
        }
    }
}