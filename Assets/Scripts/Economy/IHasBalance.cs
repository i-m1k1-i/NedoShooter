using UnityEngine.Events;

namespace Nedoshooter.Economy
{
    public interface IHasBalance
    {
        int Balance { get; }

        public event UnityAction BalanceChanged;
    }
}
