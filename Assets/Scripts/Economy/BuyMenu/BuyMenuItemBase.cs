using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Economy.BuyMenu
{
    public abstract class BuyMenuItemBase : ScriptableObject, IBuyMenuItem
	{
        [SerializeField] protected Sprite _itemSprite;
        [SerializeField] protected string _itemName;
        [SerializeField] protected int _itemPrice;

        public string Name => _itemName;
        public int Price => _itemPrice;
        public Sprite Sprite => _itemSprite;

        public abstract void OnBuy();
	}
}