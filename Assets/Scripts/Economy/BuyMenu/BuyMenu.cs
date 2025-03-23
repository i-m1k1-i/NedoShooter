using TMPro.EditorUtilities;
using UnityEngine;

namespace Assets.Scripts.Economy.BuyMenu
{
	public class BuyMenu : MonoBehaviour
	{
        [SerializeField] private BankAccount _bankAccount;

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
            if (_bankAccount.Balance >= item.Price)
            {
                _bankAccount.Withdraw(item.Price);
                item.OnBuy();
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
    }
}