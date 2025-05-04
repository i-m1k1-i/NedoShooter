using UnityEngine;
using TMPro;
using Assets.Scripts.Player;

namespace Assets.Scripts.Weapons
{
    public class AmmoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _ammoUIText;
        [SerializeField] private IFirearm _weapon;
        [SerializeField] private WeaponUser _weaponUser;

        private string _text;

        private void Start()
        {
            _ammoUIText.enabled = false;
        }

        private void UpdateCurrentAmmo()
        {
            if (_weapon == null)
                _text = $"{_weaponUser.ExtraAmmoAmount}";
            else
                _text = $"{_weaponUser.ExtraAmmoAmount}/{_weapon.CurrentAmmo}";
            _ammoUIText.text = _text;
        }

        private void ChangeWeapon(IWeapon weapon)
        {
            if (weapon is IFirearm firearm)
            {
                _ammoUIText.enabled = true;
                _weapon = firearm;

                UpdateCurrentAmmo();

                _weapon.AmmoAmountChanged += UpdateCurrentAmmo;
            }
            else
            {
                _ammoUIText.enabled = false;
            }
        }

        private void OnEnable()
        {
            _weaponUser.WeaponChanged += ChangeWeapon;
            _weaponUser.ExtraAmmoAmountChanged += UpdateCurrentAmmo;
        }

        private void OnDisable()
        {
            if (_weapon != null)
                _weapon.AmmoAmountChanged -= UpdateCurrentAmmo;

            _weaponUser.WeaponChanged -= ChangeWeapon;
            _weaponUser.ExtraAmmoAmountChanged -= UpdateCurrentAmmo;
        }
    }
}
