using Assets.Scripts.Weapons;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class WeaponUser : MonoBehaviour
    {
        public event UnityAction<IWeapon> WeaponChanged;
        public event UnityAction ExtraAmmoAmountChanged;

        [SerializeField] private GameObject[] _weaponPrefabs = new GameObject[3];
        [SerializeField] private Transform _camera;
        [SerializeField] private WeaponUserInput _userInput;
        [SerializeField] private int _extraAmmo = 30;
        [SerializeField] private int _maxExtraAmmo = 350;

        private IWeapon[] _weapons = new IWeapon[3];
        private IWeapon _currentWeapon;
        private int _stuffLayerMask;

        public int ExtraAmmoAmount => _extraAmmo;


        public bool TryAddjustAmmo(int amount)
        {
            int newAmount = _extraAmmo + amount;

            if (newAmount <= 0)
            {
                _extraAmmo = 0;
            }
            else if (newAmount <= _maxExtraAmmo)
            {
                _extraAmmo += amount;
            }
            else 
            {
                return false;
            }

            ExtraAmmoAmountChanged?.Invoke();
            return true;

        }

        private void Awake()
        {
            SetWeapons();
            ChangeWeapon(WeaponTypes.Melee);
            _stuffLayerMask = LayerMask.NameToLayer("Stuff");
            // Debug.Log(_stuffLayerMask);
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

        private void Spray()
        {
            if (_currentWeapon is Automatic)
            {
                _currentWeapon.Attack();
            }
        }

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

        private void ChangeWeapon(WeaponTypes weaponType)
        {
            _currentWeapon?.gameObject.SetActive(false);
            _currentWeapon = _weapons[(int)weaponType];
            _currentWeapon.gameObject.SetActive(true);
            _currentWeapon.transform.localPosition = _currentWeapon.InHandPosition;
            WeaponChanged?.Invoke(_currentWeapon);
            Debug.Log("Weapon changed");
        }

        private void TryPickUpAmmo()
        {
            if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit hit, 2f, _stuffLayerMask) == false)
            {
                Debug.Log("Object not found");
                return;
            }

            if (hit.transform.TryGetComponent<ExtraAmmo>(out ExtraAmmo ammo) == false)
            {
                Debug.Log($"Extra ammo not found: {hit.transform.gameObject.name}");
                return;
            }

            if (TryAddjustAmmo(ammo.Amount))
            {
                Destroy(ammo.gameObject);
                Debug.Log("Extra ammo picked up");
            }
        }

        private void OnEnable()
        {
            _userInput.Attacking += Attack;
            _userInput.Spraing += Spray;
            _userInput.Reloading += Reload;
            _userInput.ChangingWeapon += ChangeWeapon;
            _userInput.TryingPickUpAmmo += TryPickUpAmmo;
        }

        private void OnDisable()
        {
            _userInput.Attacking -= Attack;
            _userInput.Spraing += Spray;
            _userInput.Reloading -= Reload;
            _userInput.ChangingWeapon -= ChangeWeapon;
            _userInput.TryingPickUpAmmo -= TryPickUpAmmo;
        }
    }

    
}