using UnityEngine.Events;

namespace Assets.Scripts.Economy
{
    public interface IBalance
    {
        int Balance { get; }

        public event UnityAction BalanceChanged;
    }
}
