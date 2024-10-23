
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.DragAndDrop
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] WeaponUser _weaponUser;
        [SerializeField] Transform _inventorySlots;
        [SerializeField] Sprite _bulletSprite;

        private PlayerInvetnorySlotUI[] _slots = new PlayerInvetnorySlotUI[8];

        private void Awake()
        {   
            for (int i= 0;  i < _inventorySlots.childCount; i++)
            {
                Transform slot = _inventorySlots.GetChild(i);
                _slots[i] = slot.GetComponent<PlayerInvetnorySlotUI>();
            }
        }

        private void OnEnable()
        {
            CreateUIExtraAmmos(_weaponUser.ExtraAmmoAmount);
        }

        private void CreateUIExtraAmmos(int extraAmmoAmount)
        {

            foreach (var slot in _slots)
            {
                slot.ResetSlot();
                if (extraAmmoAmount >= 30)
                {
                    _weaponUser.TryAddjustAmmo(-30);
                    slot.AddItem(_bulletSprite, 30);
                    extraAmmoAmount -= 30;
                }
                else if (extraAmmoAmount > 0)
                {
                    _weaponUser.TryAddjustAmmo(-extraAmmoAmount);
                    slot.AddItem(_bulletSprite, extraAmmoAmount);
                    extraAmmoAmount = 0;
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
