using Assets.Scripts.Economy;
using TMPro;
using UnityEngine;

public class BalanceView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceText;
    [SerializeField] private BankAccount _bankAccount;

    private void Start()
    {
        _balanceText.text = _bankAccount.Balance.ToString();
    }
    private void OnEnable()
    {
        _bankAccount.BalanceChanged += OnBalanceChanged;
    }

    private void OnDisable()
    {
        _bankAccount.BalanceChanged -= OnBalanceChanged;
    }   

    private void OnBalanceChanged()
    {
        _balanceText.text = _bankAccount.Balance.ToString();
    }
}
