using UnityEngine;

namespace Nedoshooter.DragAndDrop
{
    public abstract class InventorySlotUI : MonoBehaviour, IDragContainer
    {
        [SerializeField] protected InventoryItemIcon _icon;

        public Sprite GetItem()
        {
            return _icon.GetItem();
        }

        public int GetAmmoAmount()
        {
            return _icon.AmmoAmount;
        }

        public void RemoveItem()
        {
            if (_icon.GetItem() != null)
            {
                _icon.SetItem(null);
                RemoveItemHandler();
            }
            _icon.SetAmmoAmount(0);
        }

        public void AddItem(Sprite item, int ammoAmount)
        {
            _icon.SetItem(item);
            _icon.SetAmmoAmount(ammoAmount);
            AddItemHandler();
        }

        protected abstract void RemoveItemHandler();
        protected abstract void AddItemHandler();
    }
}
