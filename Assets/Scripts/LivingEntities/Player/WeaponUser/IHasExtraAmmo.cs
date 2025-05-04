using UnityEngine.Events;

namespace Nedoshooter.WeaponUser
{
    public interface IHasExtraAmmo
    {
        event UnityAction ExtraAmmoAmountChanged;

        int ExtraAmmoAmount { get; }

        bool TryAddjustAmmo(int amount);
    }
}