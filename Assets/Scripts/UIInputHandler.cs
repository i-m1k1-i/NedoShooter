using UnityEngine;
using Assets.Scripts.Economy.BuyMenu;

public class UIInputHandler : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private BuyMenu _buyMenu;

    private void OnEnable()
    {
        _input.BuyMenuEvent += OnBuyMenu;
    }

    private void OnDisable()
    {
        _input.BuyMenuEvent -= OnBuyMenu;
    }

    private void OnBuyMenu()
    {
        if (_buyMenu.IsActive)
        {
            _buyMenu.Disable();
            _playerController.SetMenuMode(false);
        }
        else
        {
            _buyMenu.Enable();
            _playerController.SetMenuMode(true);
        }
    }
}
