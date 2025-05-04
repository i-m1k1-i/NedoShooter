using UnityEngine;

namespace Nedoshooter.DragAndDrop
{
    public interface IDragDestination
    {
        void AddItem(Sprite item, int AmmoAmount);
    }
}