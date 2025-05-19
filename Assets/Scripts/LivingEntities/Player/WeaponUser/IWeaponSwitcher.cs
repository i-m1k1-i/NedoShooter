using Nedoshooter.Weapons;
using UnityEngine.Events;

namespace Nedoshooter.WeaponUser
{
    public interface IWeaponSwitcher
    {
        event UnityAction<IWeapon> WeaponChanged;

        void SetActiveWeapon(WeaponType weaponType);
    }
}
