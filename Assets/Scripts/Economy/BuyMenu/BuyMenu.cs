using TMPro.EditorUtilities;
using UnityEngine;

namespace Assets.Scripts.Economy.BuyMenu
{
	public class BuyMenu : MonoBehaviour
	{
        [SerializeField] private PlayerMoney _playerMoney;

        public bool IsActive => gameObject.activeSelf;

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            Disable();
        }

        private void OnEnable()
        {
            UIBuyMenuSlot.SlotClicked += OnSlotClick;
        }

        private void OnDisable()
        {
            UIBuyMenuSlot.SlotClicked -= OnSlotClick;
        }
        
        private void OnSlotClick(IBuyMenuItem item)
        {
            if (_playerMoney.Balance >= item.Price)
            {
                _playerMoney.TryRemoveMoney(item.Price);
                item.OnBuy();
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
    }
}