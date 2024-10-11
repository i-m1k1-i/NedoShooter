using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Weapons
{
    public abstract class SemiAutomatic : MonoBehaviour, IFirearm
    {
        public event UnityAction AmmoAmountChanged;
        public event UnityAction Attacked;
        public event UnityAction BoltCocked;
        
        [SerializeField] protected Transform _camera;
        [SerializeField] protected int _damage;

        public Vector3 InHandPosition { get; protected set; }
        public int CurrentAmmo { get; protected set; }
        public int MagazineCapacity { get; protected set; }

        private bool _safetyEnabled = false;


        // Should be used in the last string of a child class's Awake
        protected void Init()
        {
            _camera = transform.parent;
            CurrentAmmo = MagazineCapacity;
            InHandPosition = new Vector3(0f, 0f, 0f);
        }

        public virtual void Attack()
        {
            if (CurrentAmmo == 0 ||
                _safetyEnabled)
            {
                return;
            }

            RaycastHit[] hits = Physics.RaycastAll(_camera.position, _camera.forward, 500);
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.TryGetComponent<Health>(out Health health))
                {
                    health.TakeDamage(_damage);
                    print(hit.transform.name);
                }
            }
            CurrentAmmo--;
            Attacked?.Invoke();
            AmmoAmountChanged?.Invoke();
        }

        public virtual int Reload(int extraAmmo)
        {
            if (extraAmmo == 0)
            {
                return 0;
            }

            int usedAmmo;
            int reloadableAmount = MagazineCapacity - CurrentAmmo;

            if (extraAmmo >= reloadableAmount)
            {
                usedAmmo = reloadableAmount;
                CurrentAmmo += reloadableAmount;
            }
            else
            {
                CurrentAmmo += extraAmmo;
                usedAmmo = extraAmmo;
            }

            AmmoAmountChanged?.Invoke();
            return usedAmmo;
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