using UnityEngine;
using Zenject;

namespace Nedoshooter.Economy.BuyMenu
{
	public class BuyMenu : MonoBehaviour
	{
        private PlayerMoney _playerMoney;

        public bool IsActive => gameObject.activeSelf;

        [Inject]
        private void Initialize(PlayerMoney playerMoney)
        {
            _playerMoney = playerMoney;
        }

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