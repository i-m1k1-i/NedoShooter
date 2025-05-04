using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Economy.BuyMenu
{
    [CreateAssetMenu(fileName = "New BuyMenuGun", menuName = "BuyMenu/BuyMenuGun")]
    public class BuyMenuGun : BuyMenuItemBase
    {
        [SerializeField] protected GameObject _weaponPrefab;

        public static event UnityAction<GameObject> WeaponBouhgt;

        public override void OnBuy()
		{
            WeaponBouhgt?.Invoke(_weaponPrefab);
            Debug.Log($"{_itemName} bought");
		}
    }
}