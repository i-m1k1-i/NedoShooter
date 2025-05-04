using Nedoshooter.Players;
using Nedoshooter.WeaponUser;
using UnityEngine;
using Zenject;

namespace Nedoshooter.DragAndDrop
{
    public class PlayerInvetnorySlotUI : InventorySlotUI
    {
        private IHasExtraAmmo _hasExtraAmmo;

        [Inject]
        private void Initialize(IHasExtraAmmo hasExtraAmmo)
        {
            _hasExtraAmmo = hasExtraAmmo;
        }

        public void ResetSlot()
        {
            _icon.SetAmmoAmount(0);
            _icon.SetItem(null);
        }

        protected override void RemoveItemHandler()
        {
            _hasExtraAmmo.TryAddjustAmmo(-GetAmmoAmount());
            Debug.Log("Ammo taked out");
        }

        protected override void AddItemHandler()
        {
            _hasExtraAmmo.TryAddjustAmmo(GetAmmoAmount());
            Debug.Log("Ammo added");
        }
    }
}