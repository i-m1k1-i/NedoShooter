using UnityEngine;
using Assets.Scripts.Economy.BuyMenu;
using Assets.Scripts.Player;

public class UIInputHandler : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private BuyMenu _buyMenu;

    private void OnEnable()
    {
        _input.ToggleBuyMenuEvent += ToggleBuyMenu;
    }

    private void OnDisable()
    {
        _input.ToggleBuyMenuEvent -= ToggleBuyMenu;
    }

    private void ToggleBuyMenu()
    {
        if (_buyMenu.IsActive)
        {
            _buyMenu.Disable();
            _playerController.LockMouse(true);
        }
        else
        {
            _buyMenu.Enable();
            _playerController.LockMouse(false);
        }
    }
}
