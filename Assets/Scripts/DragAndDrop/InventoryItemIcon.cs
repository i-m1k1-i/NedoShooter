using UnityEngine;
using UnityEngine.UI;

namespace Nedoshooter.DragAndDrop
{
    public class InventoryItemIcon : MonoBehaviour 
    {
        [SerializeField] private int _ammoAmount = 0;

        private Image _image;
        private CanvasGroup _canvasGroup;

        public int AmmoAmount => _ammoAmount;

        public void SetAmmoAmount(int amount)
        {
            _ammoAmount = amount;
        }

        private void Awake ()
        {
            _image = GetComponent<Image>();
            _canvasGroup = GetComponent<CanvasGroup>();
            if (_image.sprite == null)
            {
                _canvasGroup.blocksRaycasts = false;
            }
        }

        public void SetItem(Sprite sprite)
        {
            if (_image == null)
            {
                Debug.Log("Image null");
                return;
            }
            if(_image.sprite == null)
            {
                Debug.Log("Image sprite null");
                return;
            }
            if (sprite == null)
            {
                Debug.Log("Sprite null");
                return;
            }

            _image.sprite = sprite;
            _canvasGroup.blocksRaycasts = sprite != null;
        }

        public Sprite GetItem()
        {
            print(_image.sprite + "_m1k1");
            return _image.sprite;
        }
    }
}
