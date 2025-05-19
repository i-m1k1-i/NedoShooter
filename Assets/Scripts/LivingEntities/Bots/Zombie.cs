using System;
using UnityEngine;

namespace Nedoshooter.Enemies
{
    public class Zombie : Enemy
    {
        [SerializeField] private AttackSettings _attackSettings;

        private float _lastAttackTime;

        public override void Attack()
        {
            if (Time.timeSinceLevelLoad - _lastAttackTime < _attackSettings.AttackCooldown)
                return;

            _lastAttackTime = Time.timeSinceLevelLoad;

            Vector3 targetDir = Target.Position - transform.position;
            targetDir.y = transform.position.y;
            transform.rotation.SetLookRotation(targetDir);

            Vector3 center = transform.position + transform.forward * _settings.AttackDistance / 2;
            Vector3 size = new Vector3(_attackSettings.AttackWidth / 2, _attackSettings.AttackHeight / 2, _settings.AttackDistance / 2);
            LayerMask attackMask = LayerMask.GetMask("Player");

            Collider[] hits = Physics.OverlapBox(center, size, transform.rotation, attackMask);

            foreach (Collider hit in hits)
            {
                if (hit.TryGetComponent(out IDamagable damageable))
                {
                    damageable.TakeDamage(_attackSettings.Damage);
                }
            }
        }

        public override void Follow(Vector3 targetPosition)
        {
            Agent.SetDestination(targetPosition);
        }

        [Serializable]
        public class AttackSettings
        {
            public int AttackCooldown;
            public int Damage;
            public float AttackWidth;
            public float AttackHeight;
        }
    }
}