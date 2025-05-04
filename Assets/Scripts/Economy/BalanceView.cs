using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Economy.UI
{
    public class BalanceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _balanceText;
        [SerializeField] private IBalance _balance;

        [Inject]
        public void Initialize(IBalance balance)
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