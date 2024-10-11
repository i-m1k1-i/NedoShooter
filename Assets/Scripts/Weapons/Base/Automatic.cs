using Assets.Scripts.LivingEntities.Player;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Weapons
{
    public abstract class Automatic : MonoBehaviour, IFirearm
    {
        public event UnityAction AmmoAmountChanged;
        public event UnityAction Attacked;
        public event UnityAction BoltCocked;

        [SerializeField] protected Transform _camera;  
        [SerializeField] protected int _damage;
        [SerializeField] protected float _sprayDelay;

        [Header("Recoil")]
        [SerializeField] protected float _defaultRecoilPower = 0.2f;
        [SerializeField] protected float _recoilPowerIncreasingRate = 0.05f;
        [SerializeField] protected float _recoilDecreasingRate = 0.1f;

        private RotationController _rotationController;
        private float _sprayTimer;
        private bool _safetyEnabled = false;
        private float _recoilPower;
        private float _addedRecoil;

        public Vector3 InHandPosition { get; protected set; }
        public int CurrentAmmo { get; protected set; }
        public int MagazineCapacity { get; protected set; }

        protected void Init()
        {
            _camera = transform.parent;
            Transform player = _camera.parent;
            _rotationController = player.GetComponent<RotationController>();

            CurrentAmmo = MagazineCapacity;
            _recoilPower = _defaultRecoilPower;
            InHandPosition = new Vector3(0f, 0f, 0f);
        }

        private void Update()
        {
            if (_sprayTimer > 0)
            {
                _sprayTimer -= Time.deltaTime;
            }

            if (_addedRecoil > 0)
            {
                _rotationController.AddRotateX(-_recoilDecreasingRate);
                _addedRecoil -= _recoilDecreasingRate;
            }
            else 
            {
                _recoilPower = _defaultRecoilPower;
            }
        }

        public virtual void Attack()
        {
            if (CurrentAmmo == 0 ||
                _sprayTimer > 0 ||
                _safetyEnabled)
            {
                return;
            }

            _sprayTimer = _sprayDelay;

            RaycastHit[] hits = Physics.RaycastAll(_camera.position, _camera.forward, 500);
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.TryGetComponent<Health>(out Health health))
                {
                    health.TakeDamage(_damage);
                    print(hit.transform.name);
                }
            }
            ApplyRecoil();
            CurrentAmmo--;

            Attacked?.Invoke();
            AmmoAmountChanged?.Invoke();
        }

        public virtual int Reload(int ammoAmount)
        {
            int ammoUsed;
            if (ammoAmount == 0)
            {
                ammoUsed = 0;
                return ammoUsed;
            }
            
            int reloadableAmount = MagazineCapacity - CurrentAmmo;

            if (ammoAmount >= reloadableAmount)
            {
                ammoUsed = reloadableAmount;
                CurrentAmmo += reloadableAmount;
            }
            else
            {
                CurrentAmmo += ammoAmount;
                ammoUsed = ammoAmount;
            }

            AmmoAmountChanged?.Invoke();
            return ammoUsed;
        }

        private void ApplyRecoil()
        {
            _rotationController.AddRotateX(_recoilPower);
            _addedRecoil += _recoilPower;
            _recoilPower += _recoilPowerIncreasingRate;
        }

        public void SafetyOff()
        {
            _safetyEnabled = false;
            BoltCocked?.Invoke();
        }

        public void SafetyOn()
        {
            _safetyEnabled = true;
        }

        private void OnDisable()
        {
            SafetyOn();
        }
    }
}