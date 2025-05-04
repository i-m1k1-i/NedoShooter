using UnityEngine;
using TMPro;
using Nedoshooter.Players;
using Nedoshooter.WeaponUser;
using Zenject;

namespace Nedoshooter.Weapons
{
    public class AmmoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _ammoUIText;
        [SerializeField] private IFirearm _weapon;

        private IFirearmed _firearmed;
        private string _text;

        [Inject]
        private void Initialize(IFirearmed firearmed)
        {
            _firearmed = firearmed;
        }

        private void Start()
        {
            _ammoUIText.enabled = false;
        }

        private void UpdateCurrentAmmo()
        {
            if (_weapon == null)
                _text = $"{_firearmed.ExtraAmmoAmount}";
            else
                _text = $"{_firearmed.ExtraAmmoAmount}/{_weapon.CurrentAmmo}";
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
            _firearmed.WeaponChanged += ChangeWeapon;
            _firearmed.ExtraAmmoAmountChanged += UpdateCurrentAmmo;
        }

        private void OnDisable()
        {
            if (_weapon != null)
                _weapon.AmmoAmountChanged -= UpdateCurrentAmmo;

            _firearmed.WeaponChanged -= ChangeWeapon;
            _firearmed.ExtraAmmoAmountChanged -= UpdateCurrentAmmo;
        }
    }
}
