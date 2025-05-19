using Nedoshooter.Economy;
using UnityEngine;
using Zenject;

namespace Nedoshooter.Players
{
    public class Player : MonoBehaviour, ITarget
    {
        [SerializeField] private int _startBalance;

        private IHasBalance _balance;

        public Vector3 Position
        {
            get { return transform.position; }
        }

        [Inject]
        private void Initialize(PlayerMoney playerMoney)
        {
            _balance = playerMoney;
        }

        private void Start()
        {
            _balance.SetBalance(_startBalance);
        }

        public class Factory : PlaceholderFactory<Player>
        {
        }
    }
}
