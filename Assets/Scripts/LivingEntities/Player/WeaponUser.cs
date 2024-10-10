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
        [SerializeField] private int _extraAmmoLimit = 350;

        private IWeapon[] _weapons = new IWeapon[3];
        private IWeapon _currentWeapon;
        private int[] ammos = new int[3];
        

        public int AmmoAmount => _extraAmmo;

        private void Awake()
        {
            SetWeapons();
            ChangeWeapon(WeaponTypes.Melee);
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
            print("Attack");
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
                print("reload");
            }
            print("cant reload");
        }

        private void ChangeWeapon(WeaponTypes weaponType)
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.gameObject.SetActive(false);
            }
            _currentWeapon = _weapons[(int)weaponType];
            _currentWeapon.gameObject.SetActive(true);
            _currentWeapon.transform.localPosition = _currentWeapon.InHandPosition;
            WeaponChanged?.Invoke(_currentWeapon);
            print("weapon changed");
        }

        private void TryPickUpAmmo()
        {
            if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit hit, 2f) == false)
            {
                Debug.Log("Object not found");
                return;
            }

            if (hit.transform.TryGetComponent<ExtraAmmo>(out ExtraAmmo ammo) == false)
            {
                Debug.Log("Extra ammo not found");
                return;
            }

            if (TryAddAmmo(ammo.Amount))
            {
                Destroy(ammo.gameObject);
                Debug.Log("Extra ammo picked up");
                ExtraAmmoAmountChanged?.Invoke();
            }
        }

        private bool TryAddAmmo(int amount)
        {
            if (_extraAmmo + amount <= _extraAmmoLimit)
            { 
                _extraAmmo += amount;
                return true;
            }
            else
            {
                return false;
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