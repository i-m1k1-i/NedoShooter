using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DragAndDrop
{
    public class InventoryItemIcon : MonoBehaviour 
    {
        private Image _image;

        private void Awake ()
        {
            _image = GetComponent<Image>();
        }

        public void SetItem(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public Sprite GetItem()
        {
            return _image.sprite;
        }
    }
}
