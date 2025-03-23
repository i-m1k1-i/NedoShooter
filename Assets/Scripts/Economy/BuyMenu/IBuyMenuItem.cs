using UnityEngine;

namespace Assets.Scripts.Economy.BuyMenu
{
	public interface IBuyMenuItem
	{
        string Name { get; }
        int Price { get; }
        Sprite Sprite { get; }
        void OnBuy();
	}
}