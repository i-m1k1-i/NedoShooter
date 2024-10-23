using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.DragAndDrop
{
    public class InventoryDragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector3 _startPosition;
        private Transform _originalParent;
        private IDragSource _source;
        
        private Canvas _parentCanvas;

        private void Awake()
        {
            _parentCanvas = GetComponentInParent<Canvas>();
            _source = GetComponentInParent<IDragSource>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = transform.position;
            _originalParent = transform.parent;

            GetComponent<CanvasGroup>().blocksRaycasts = false;
            transform.SetParent(_parentCanvas.transform, true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ResetDragItemState();

            IDragDestination container = GetContainer(eventData);
            if (container != null)
            {
                DropItemIntoContainer(container);
            }
        }

        private IDragDestination GetContainer(PointerEventData eventData)
        {
            GameObject raycastedObject = eventData.pointerCurrentRaycast.gameObject;

            if (raycastedObject == null) 
            {
                return null;
            }

            if (raycastedObject.TryGetComponent<IDragDestination>(out IDragDestination container))
            {
                return container;
            }

            return null;
        }

        private void DropItemIntoContainer(IDragDestination destination)
        {
            Sprite itemIcon = _source.GetItem();
            int ammoAmount = _source.GetAmmoAmount();
            _source.RemoveItem();
            destination.AddItem(itemIcon, ammoAmount);
        }

        private void ResetDragItemState()
        {
            transform.position = _startPosition;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            transform.SetParent(_originalParent, true);
        }
    }
}
