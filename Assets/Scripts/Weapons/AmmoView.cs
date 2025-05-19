using UnityEngine;
using TMPro;
using Nedoshooter.WeaponUser;
using Zenject;
using System.Collections;
using Nedoshooter.Installers;

namespace Nedoshooter.Weapons
{
    public class AmmoView : MonoBehaviour, IReinjectable
    {
        [SerializeField] private TextMeshProUGUI _ammoUIText;
        [SerializeField] private IFirearm _weapon;

        private IFirearmed _firearmed;
        private string _text;

        [Inject]
        private void Initialize(IFirearmed firearmed)
        {
            SampleInstaller.Instance.RegisterReinjectable(this);
            _firearmed = firearmed;
        }

        public void Reinject(DiContainer container)
        {
            container.Inject(this);
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
            StartCoroutine(DelayedSubscribe());
        }

        private void OnDisable()
        {
            if (_weapon != null)
                _weapon.AmmoAmountChanged -= UpdateCurrentAmmo;

            if (_firearmed != null)
            {
                _firearmed.WeaponChanged -= ChangeWeapon;
                _firearmed.ExtraAmmoAmountChanged -= UpdateCurrentAmmo;
            }
        }

        private IEnumerator DelayedSubscribe()
        {
            yield return new WaitWhile(() => _firearmed == null);

            _firearmed.WeaponChanged += ChangeWeapon;
            _firearmed.ExtraAmmoAmountChanged += UpdateCurrentAmmo;
        }
    }
}
