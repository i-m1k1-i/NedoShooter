using Assets.Scripts.LivingEntities.Player;
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

        [Header("Recoil")]
        [SerializeField] protected float _defaultRecoilPower = 1f;
        [SerializeField] protected float _recoilPowerIncreasingRate = 0.1f;

        protected float _recoilDecreasingRate => _recoilPower * 0.025f;
        private RotationController _rotationController;
        private Animator _animator;
        private bool _safetyEnabled = false;
        private float _recoilPower;
        private float _addedRecoil;

        public Vector3 InHandPosition { get; protected set; }
        public int CurrentAmmo { get; protected set; }
        public int MagazineCapacity { get; protected set; }

        // Should be used in the last string of a child class's Awake
        protected void Init()
        {
            _camera = transform.parent;
            Transform player = _camera.parent;

            _rotationController = player.GetComponent<RotationController>();
            _animator = GetComponent<Animator>();

            CurrentAmmo = MagazineCapacity;
            InHandPosition = new Vector3(0f, 0f, 0f);
        }

        private void Update()
        {
            if (_addedRecoil > 0)
            {
                _rotationController.AddRotateX(-_recoilDecreasingRate);
                _addedRecoil = Mathf.Clamp(_addedRecoil - _recoilDecreasingRate, 0, 1000);
            }
            else
            {
                _recoilPower = _defaultRecoilPower;
            }
        }

        public virtual void Attack()
        {
            Debug.Log("Semi attack");
            if (CurrentAmmo == 0 ||
                _safetyEnabled)
            {
                Debug.Log("Semi attack cancel");
                return;
            }

            RaycastHit[] hits = Physics.RaycastAll(_camera.position, _camera.forward, 500);
            Debug.Log("Semi attack raycast");
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.TryGetComponent<Health>(out Health health))
                {
                    Debug.Log("Semi attack damage");
                    health.TakeDamage(_damage);
                    print(hit.transform.name);
                }
            }
            ApplyRecoil();
            CurrentAmmo--;
            Attacked?.Invoke();
            AmmoAmountChanged?.Invoke();
        }

        public virtual int Reload(int extraAmmo)
        {
            Debug.Log("Semi Reload");
            if (extraAmmo == 0)
            {
                Debug.Log("Semi Reload Extra 0");
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

        private void OnEnable()
        {
            _animator.SetTrigger("Take");
            Debug.Log("In Hand Pos: " + InHandPosition);
        }

        private void OnDisable()
        {
            SafetyOn();
        }
    }
}