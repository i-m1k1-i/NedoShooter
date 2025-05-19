using UnityEngine.Events;

namespace Nedoshooter.Economy
{
    public interface IHasBalance
    {
        int Balance { get; }

        public event UnityAction BalanceChanged;

        public void SetBalance(int amount);
    }
}
