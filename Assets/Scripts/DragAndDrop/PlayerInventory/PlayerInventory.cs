using Nedoshooter.WeaponUser;
using UnityEngine;
using Zenject;

namespace Nedoshooter.DragAndDrop
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private IHasExtraAmmo _hasExtraAmmo;
        [SerializeField] private Transform _inventorySlots;
        [SerializeField] private Sprite _bulletSprite;

        private PlayerInvetnorySlotUI[] _slots = new PlayerInvetnorySlotUI[8];

        [Inject]
        private void Initialize(IHasExtraAmmo hasExtraAmmo)
        {
            _hasExtraAmmo = hasExtraAmmo;
        }

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
            Debug.Log($"Create UI Extra Ammos: {_hasExtraAmmo}");
            CreateUIExtraAmmos(_hasExtraAmmo.ExtraAmmoAmount);
        }

        private void CreateUIExtraAmmos(int extraAmmoAmount)
        {

            foreach (var slot in _slots)
            {
                slot.ResetSlot();
                if (extraAmmoAmount >= 30)
                {
                    _hasExtraAmmo.TryAddjustAmmo(-30);
                    slot.AddItem(_bulletSprite, 30);
                    extraAmmoAmount -= 30;
                }
                else if (extraAmmoAmount > 0)
                {
                    _hasExtraAmmo.TryAddjustAmmo(-extraAmmoAmount);
                    slot.AddItem(_bulletSprite, extraAmmoAmount);
                    extraAmmoAmount = 0;
                }
                else if (extraAmmoAmount == 0)
                {
                    return;
                }
            }
        }
    }
}
