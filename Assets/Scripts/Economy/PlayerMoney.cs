using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Economy
{
    public class PlayerMoney : MonoBehaviour, IBalance
    {
        [SerializeField] private int _startBalance;

        public int Balance { get; private set; }

        public event UnityAction BalanceChanged;

        private void Start()
        {
            Balance = _startBalance;
            BalanceChanged?.Invoke();
        }

        public void AddMoney(int amount)
        {
            Balance += amount;
            BalanceChanged?.Invoke();
        }

        public bool TryRemoveMoney(int amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                BalanceChanged?.Invoke();  
                return true;
            }  
            return false;
        }
    }
}