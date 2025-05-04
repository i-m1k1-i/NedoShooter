using Nedoshooter.Players;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(BoxCollider))]
public class ExtraAmmoZone : MonoBehaviour
{
    [SerializeField] private GameObject _extraAmmoDragMenu;

    private PlayerController _playerController;
    private bool _inZone;

    [Inject]
    private void Initialize(PlayerController playerController)
    {
        _playerController = playerController;
    }

    private void Awake()
    {
        _extraAmmoDragMenu.SetActive(false);
    }

    private void Update()
    {
        if (_inZone && Input.GetKeyDown(KeyCode.F))
        {
            bool menuIsActive = _extraAmmoDragMenu.activeSelf;
            _extraAmmoDragMenu.SetActive(!menuIsActive);
            _playerController.LockMouse(menuIsActive);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out _playerController) == false)
        {
            return;
        }
        _inZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out _playerController) == false)
        {
            return;
        }
        if (_playerController == null)
        {
            return;
        }

        _inZone = false;
        _playerController.LockMouse(true);
        _playerController = null;
        _extraAmmoDragMenu.SetActive(false);
    }
}
