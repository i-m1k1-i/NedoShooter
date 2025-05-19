using UnityEngine;
using UnityEngine.Events;

namespace Nedoshooter.Economy
{
    public class PlayerMoney : IHasBalance
    {
        public int Balance { get; private set; }

        public event UnityAction BalanceChanged;

        public void SetBalance(int amount)
        {
            Balance = amount;
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