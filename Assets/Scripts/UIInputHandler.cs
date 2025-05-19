using UnityEngine;
using Nedoshooter.Economy.BuyMenu;
using Nedoshooter.Players;
using Zenject;
using Nedoshooter.Installers;

public class UIInputHandler : MonoBehaviour, IReinjectable
{
    [SerializeField] private BuyMenu _buyMenu;

    private InputReader _input;
    private IMouseLocker _mouseLockHandler;

    [Inject]
    private void Initialize(InputReader inputReader, IMouseLocker mouseLocker)
    {
        SampleInstaller.Instance.RegisterReinjectable(this);
        _input = inputReader;
        _mouseLockHandler = mouseLocker;
    }

    public void Reinject(DiContainer container)
    {
        container.Inject(this);
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
            _mouseLockHandler.LockMouse(true);
        }
        else
        {
            _buyMenu.Enable();
            _mouseLockHandler.LockMouse(false);
        }
    }
}
