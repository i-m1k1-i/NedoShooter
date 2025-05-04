using Nedoshooter.Weapons;
using UnityEngine;
using UnityEngine.Events;

namespace Nedoshooter.WeaponUser
{
    public interface IHasWeapon
    {
        event UnityAction<IWeapon> WeaponChanged;

        void SetActiveWeapon(WeaponType weaponType);
        void SetWeaponInSlot(GameObject weaponPrefab);
    }
}
