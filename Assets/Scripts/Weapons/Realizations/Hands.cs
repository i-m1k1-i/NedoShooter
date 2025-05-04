using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace Nedoshooter.Weapons
{
    internal class Hands : MonoBehaviour, IWeapon
    {
        public event UnityAction Attacked;
        public WeaponType Type => WeaponType.Melee;
        public Vector3 InHandPosition {  get; private set; }

        [SerializeField] private int _damage = 25;
        [SerializeField] private float _damageDistance = 2f;

        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _rightHand;

        private Transform _camera;
        private Animator _animator;
        private AudioSource _audioSource;
        private Vector3 _leftDefaultPosition;
        private Vector3 _rightDefaultPosition;
        private bool _firstPunch = true;

        private void Awake()
        {
            SetPosition();
            _camera = transform.parent;
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _leftDefaultPosition = _leftHand.localPosition;
            _rightDefaultPosition = _rightHand.localPosition;
        }

        public void SetHandsDefaultPosition()
        {
            _leftHand.localPosition = _leftDefaultPosition;
            _rightHand.localPosition = _rightDefaultPosition;
        }

        protected void SetPosition()
        {
            InHandPosition = new Vector3(0f, -0.3f, 0f);
            print("setted");
        }

        public void Attack()
        {
            if (_firstPunch) 
            {
                _animator.SetTrigger("Left");
            }
            else
            {
                _animator.SetTrigger("Right");
            }
            _audioSource.Play();
            _firstPunch = !_firstPunch;

            int layerMask = ~LayerMask.GetMask("Player");
            Vector3 raycastPosition = _camera.position + _camera.forward * 0.37f;
            if (Physics.Raycast(raycastPosition, _camera.forward, out RaycastHit hit, _damageDistance, layerMask))
            {
                if (hit.transform.TryGetComponent<Health>(out Health health))
                {
                    Debug.Log("Hit name: " + hit.transform.name);
                    health.TakeDamage(_damage);
                }
                Attacked?.Invoke();
            }
        }

        private void OnEnable()
        {
            Debug.Log("default position set: "  +  _leftDefaultPosition + ", " + _rightDefaultPosition + "\n"  + _leftHand.localPosition + ", " + _rightHand.localPosition);
        }

        private void OnDrawGizmos()
        {
            Vector3 raycastPosition = _camera.position + _camera.forward * 0.37f;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(raycastPosition, raycastPosition + (_camera.forward * _damageDistance));
        }
    }
}
