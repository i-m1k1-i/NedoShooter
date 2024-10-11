using UnityEngine;

namespace Assets.Scripts.DragAndDrop
{
    public class InventorySlotUI : MonoBehaviour, IDragContainer
    {
        [SerializeField] private InventoryItemIcon _icon;

        public Sprite GetItem()
        {
            return _icon.GetItem();
        }

        public void RemoveItem()
        {
            _icon.SetItem(null);

        }

        public void AddItem(Sprite item)
        {
            _icon.SetItem(item);
        }
    }
}
