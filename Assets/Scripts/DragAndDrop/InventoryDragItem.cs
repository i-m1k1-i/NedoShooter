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
            _parentCanvas = GetComponent<Canvas>();
            _source = GetComponentInParent<IDragSource>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = eventData.position;
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
            transform.position = _startPosition;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            transform.SetParent(_originalParent, true);

            IDragDestination container = GetContainer(eventData);
            if(EventSystem.current.IsPointerOverGameObject() == false)
            {
                container = _parentCanvas.GetComponent<IDragDestination>();
            }

            if (container != null)
            {
                DropItemIntoContainer(container);
            }
        }

        private IDragDestination GetContainer(PointerEventData eventData)
        {
            IDragDestination container;
            GameObject raycastedObject = eventData.pointerCurrentRaycast.gameObject;
            if (raycastedObject.TryGetComponent<IDragDestination>(out container))
            {
                return container;
            }
            else
            {
                return null;
            }
        }

        private void DropItemIntoContainer(IDragDestination destination)
        {
            destination.AddItem(_source.GetItem());
            _source.RemoveItem();
        }
    }
}
