using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Economy
{
    public class BankAccount : MonoBehaviour
    {
        [SerializeField] private int _balance;

        public event UnityAction BalanceChanged;

        public int Balance => _balance;

        public void Deposit(int amount)
        {
            _balance += amount;
            BalanceChanged?.Invoke();
        }

        public bool Withdraw(int amount)
        {
            if (_balance >= amount)
            {
                _balance -= amount;
                BalanceChanged?.Invoke();  
                return true;
            }  
            return false;
        }
    }
}