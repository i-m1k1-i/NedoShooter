using Nedoshooter.Players;
using UnityEngine;
using UnityEngine.Events;

namespace Nedoshooter.Weapons
{
    public abstract class Automatic : MonoBehaviour, IFirearm
    {
        public event UnityAction AmmoAmountChanged;
        public event UnityAction Attacked;
        public event UnityAction BoltCocked;

        [SerializeField] private GameObject tracerPrefab;
        [SerializeField] private Transform _barrelEndPoint;

        [Header("Recoil")]
        [SerializeField] protected float _defaultRecoilPower = 1f;
        [SerializeField] protected float _recoilPowerIncreasingRate = 0.1f;

        protected float RecoilDecreasingRate => _recoilPower * 0.025f;
        protected Transform _camera;
        protected int _damage;
        protected float _shotsPerSecond;

        private float _shotsDelay;
        private RotationController _rotationController;
        private Animator _animator;
        private float _sprayTimer;
        private bool _safetyEnabled = false;
        private float _recoilPower;
        private float _addedRecoil;

        private float _tracerDuration = 0.05f;
        private float _tracerMaxDistance = 100f;

        public WeaponType Type { get; protected set; }
        public Vector3 InHandPosition { get; protected set; }
        public int CurrentAmmo { get; protected set; }
        public int MagazineCapacity { get; protected set; }

        protected void Init()
        {
            _camera = transform.parent;
            Transform player = _camera.parent;

            _rotationController = player.GetComponent<RotationController>();
            _animator = GetComponent<Animator>();

            _shotsDelay = 1 / _shotsPerSecond;
            CurrentAmmo = MagazineCapacity;
            _recoilPower = _defaultRecoilPower;
            InHandPosition = new Vector3(0f, 0f, 0f);
        }

        private void OnEnable()
        {
            ResetRecoil();
            _animator.SetTrigger("Take");
            Debug.Log("In Hand Pos: " + InHandPosition);
        }

        private void OnDisable()
        {
            SafetyOn();
        }

        private void Update()
        {
            if (_sprayTimer > 0)
            {
                _sprayTimer -= Time.deltaTime;
            }

            if (_addedRecoil > 0)
            {
                _rotationController.AddRotateX(-RecoilDecreasingRate);
                _addedRecoil = Mathf.Clamp(_addedRecoil - RecoilDecreasingRate, 0, 1000);
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

            _sprayTimer = _shotsDelay;

            RaycastHit[] hits = new RaycastHit[10];
            int hitCount = Physics.RaycastNonAlloc(_camera.position,  _camera.forward, hits, 500);

            for (int i = 0; i < hitCount; i++)
            {
                RaycastHit hit = hits[i];
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

            Vector3 direction = _camera.forward;
            Vector3 tracerEndPoint = _camera.position + direction * _tracerMaxDistance;
            if (hitCount > 0)
            {
                tracerEndPoint = hits[0].point;
            }
            CreateTracer(tracerEndPoint);
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

        public void SafetyOff()
        {
            _safetyEnabled = false;
            BoltCocked?.Invoke();
            Debug.Log("idle");
        }

        public void SafetyOn()
        {
            _safetyEnabled = true;
        }

        private void ApplyRecoil()
        {
            _rotationController.AddRotateX(_recoilPower);
            _addedRecoil += _recoilPower;
            _recoilPower += _recoilPowerIncreasingRate;
        }

        private void ResetRecoil()
        {
            _addedRecoil = 0;
            _recoilPower = _defaultRecoilPower;
        }

        private void CreateTracer(Vector3 endPoint)
        {
            GameObject tracer = Instantiate(tracerPrefab);
            LineRenderer lr = tracer.GetComponent<LineRenderer>();
            lr.SetPosition(0, _barrelEndPoint.position);
            lr.SetPosition(1, endPoint);

            Destroy(tracer, _tracerDuration);
        }
    }
}