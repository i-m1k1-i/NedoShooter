using UnityEngine;

namespace Nedoshooter.DragAndDrop
{
    public interface IDragSource
    {
        Sprite GetItem();
        int GetAmmoAmount();
        void RemoveItem();
    }
}