using Assets.Scripts.Weapons;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public interface IWeaponUserActions
    {
        event UnityAction Attacking;
        event UnityAction Spraing;
        event UnityAction Reloading;
        event UnityAction<WeaponType> ChangingWeapon;
    }
}
