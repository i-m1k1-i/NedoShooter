using TMPro;
using UnityEngine;
using Zenject;

namespace Nedoshooter.Economy.UI
{
    public class BalanceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _balanceText;
        [SerializeField] private IHasBalance _balance;

        [Inject]
        public void Initialize(IHasBalance balance)
        {
            _balance = balance;
        }

        private void Start()
        {
            _balanceText.text = _balance.Balance.ToString();
        }

        private void OnEnable()
        {
            _balance.BalanceChanged += OnBalanceChanged;
        }

        private void OnDisable()
        {
            _balance.BalanceChanged -= OnBalanceChanged;
        }

        private void OnBalanceChanged()
        {
            _balanceText.text = _balance.Balance.ToString();
        }
    }
}