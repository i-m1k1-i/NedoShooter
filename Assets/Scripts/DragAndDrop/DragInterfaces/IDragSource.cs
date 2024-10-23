using UnityEngine;

namespace Assets.Scripts.DragAndDrop
{
    public interface IDragSource
    {
        Sprite GetItem();
        int GetAmmoAmount();
        void RemoveItem();
    }
}