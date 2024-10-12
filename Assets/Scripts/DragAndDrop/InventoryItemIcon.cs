using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace Assets.Scripts.DragAndDrop
{
    public class InventoryItemIcon : MonoBehaviour 
    {
        private Image _image;
        private CanvasGroup _canvasGroup; 

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
            _image.sprite = sprite;
            _canvasGroup.blocksRaycasts = sprite != null;
        }

        public Sprite GetItem()
        {
            return _image.sprite;
        }
    }
}
