using UnityEngine;
using Nedoshooter.Economy.BuyMenu;
using Nedoshooter.Players;
using Zenject;

public class UIInputHandler : MonoBehaviour
{
    [SerializeField] private BuyMenu _buyMenu;

    private InputReader _input;
    private PlayerController _playerController;

    [Inject]
    private void Initialize(InputReader inputReader, PlayerController playerController)
    {
        _input = inputReader;
        _playerController = playerController;
    }

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
