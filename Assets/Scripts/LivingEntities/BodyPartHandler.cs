using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LivingEntities
{
    public enum BodyPart
    {
        Head = 1,
        Body = 2,
        Foot = 3
    }

    public class BodyPartHandler : MonoBehaviour, IDamagable
    {
        private static readonly Dictionary<BodyPart, float> Multipliers = new()
        {
            { BodyPart.Head, 3.75f },
            { BodyPart.Body, 1 },
            { BodyPart.Foot, 0.5f}
        };

        [SerializeField] private BodyPart _bodyPart;
        [SerializeField] private EnemyHealth _health;

        private float _multiplier => Multipliers[_bodyPart];

        public void TakeDamage(int amount)
        {
            _health.TakeDamage((int)(amount * _multiplier));
        }
    }
}
