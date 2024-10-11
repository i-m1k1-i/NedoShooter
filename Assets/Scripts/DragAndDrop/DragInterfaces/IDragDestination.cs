using UnityEngine;

namespace Assets.Scripts.DragAndDrop
{
    public interface IDragDestination
    {
        void AddItem(Sprite item);
    }
}