using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.DragAndDrop
{
    public class PlayerInvetnorySlotUI : InventorySlotUI
    {
        [SerializeField] private WeaponUser _weaponUser;

        public void ResetSlot()
        {
            _icon.SetAmmoAmount(0);
            _icon.SetItem(null);
        }

        protected override void RemoveItemHandler()
        {
            _weaponUser.TryAddjustAmmo(-GetAmmoAmount());
            Debug.Log("Ammo taked out");
        }

        protected override void AddItemHandler()
        {
            _weaponUser.TryAddjustAmmo(GetAmmoAmount());
            Debug.Log("Ammo added");
        }
    }
}