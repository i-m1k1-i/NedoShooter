using Assets.Scripts.Weapons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    public class WeaponUser : MonoBehaviour
    {
        public event UnityAction<IWeapon> WeaponChanged;
        public event UnityAction ExtraAmmoAmountChanged;

        [SerializeField] private InputReader _input;
        [SerializeField] private GameObject[] _weaponPrefabs = new GameObject[3];
        [SerializeField] private Transform _camera;
        [SerializeField] private int _extraAmmo = 30;
        [SerializeField] private int _maxExtraAmmo = 350;

        private IWeapon[] _weapons = new IWeapon[3];
        private IWeapon _currentWeapon;
        private bool _isAttacking;

        public int ExtraAmmoAmount => _extraAmmo;


        public bool TryAddjustAmmo(int amount)
        {
            int newAmount = _extraAmmo + amount;
            _extraAmmo = Mathf.Clamp(newAmount, 0, _maxExtraAmmo);
            ExtraAmmoAmountChanged?.Invoke();
            return true;

        }

        private void Awake()
        {
            SetWeapons();
            ChangeWeapon(WeaponType.Melee);
        }

        private void Update()
        {
            if (_isAttacking)
            {
                Attack();
            }
        }

        private void SetWeapons()
        {
            for (int i = 0; i < _weaponPrefabs.Length; i++)
            {
                GameObject weaponGameObject = Instantiate(_weaponPrefabs[i], _camera);
                _weapons[i] = weaponGameObject.GetComponent<IWeapon>();
                weaponGameObject.SetActive(false);
            }

            _weaponPrefabs = null;
        }

        private void Attack()
        {
            _currentWeapon.Attack();
            Debug.Log("Attack");
        }

        //private void Spray()
        //{
        //    if (_currentWeapon is Automatic)
        //    {
        //        _currentWeapon.Attack();
        //    }
        //}

        private void Reload()
        {
            if (_currentWeapon is IFirearm firearm)
            {
                _extraAmmo -= firearm.Reload(_extraAmmo);
                ExtraAmmoAmountChanged?.Invoke();
                Debug.Log("Reloaded");
                return;
            }
            Debug.Log("Can't reload");
        }

        public void ChangeWeapon(WeaponType weaponType)
        {
            IWeapon targetWeapon = _weapons[(int)weaponType];
            if (targetWeapon != _currentWeapon)
            {
                _currentWeapon?.gameObject.SetActive(false);
                _currentWeapon = targetWeapon;
                if (targetWeapon is Hands hands)
                {
                    hands.SetHandsDefaultPosition();
                }
                _currentWeapon.transform.localPosition = _currentWeapon.InHandPosition;
                _currentWeapon.transform.localRotation = Quaternion.identity;
                _currentWeapon.gameObject.SetActive(true);
                WeaponChanged?.Invoke(_currentWeapon);
                Debug.Log("Weapon changed");
            }
        }

        private void HandleFire()
        {
            if (_currentWeapon is Automatic)
            { 
                _isAttacking = true; 
            }
            else
            {
                Attack();
            }
        }

        private void HandleFireCanceled()
        {
            _isAttacking = false;
        }

        private void OnEnable()
        {
            _input.FireEvent += HandleFire;
            _input.FireCanceledEvent += HandleFireCanceled;
            _input.ChangeWeaponEvent += ChangeWeapon;
            _input.ReloadEvent += Reload;

            //_userInput.Attacking += Attack;
            //_userInput.Spraing += Spray;
            //_userInput.Reloading += Reload;
            //_userInput.ChangingWeapon += ChangeWeapon;
        }

        private void OnDisable()
        {
            _input.FireEvent -= HandleFire;
            _input.FireCanceledEvent -= HandleFireCanceled;
            _input.ChangeWeaponEvent -= ChangeWeapon;
            _input.ReloadEvent -= Reload;

            //_userInput.Attacking -= Attack;
            //_userInput.Spraing -= Spray;
            //_userInput.Reloading -= Reload;
            //_userInput.ChangingWeapon -= ChangeWeapon;
        }
    }
}