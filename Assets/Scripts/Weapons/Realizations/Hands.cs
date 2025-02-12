using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Weapons
{
    internal class Hands : MonoBehaviour, IWeapon
    {
        public event UnityAction Attacked;

        public Vector3 InHandPosition {  get; private set; }

        [SerializeField] private int _damage = 25;
        [SerializeField] private float _damageDistance = 2f;

        private Transform _camera;
        private Animator _animator;

        private void Awake()
        {
            SetPosition();
            _camera = transform.parent;
            _animator = GetComponent<Animator>();
        }

        protected void SetPosition()
        {
            InHandPosition = new Vector3(0f, -0.3f, 0f);
            print("setted");
        }

        public void Attack()
        {
            _animator.SetTrigger("Punch");
            if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit hit, _damageDistance))
            {
                if (hit.transform.TryGetComponent<Health>(out Health health))
                {
                    health.TakeDamage(_damage);
                }
                Attacked?.Invoke();
            }
        }

    }
}
