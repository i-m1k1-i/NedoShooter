using Nedoshooter.Players;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ExtraAmmoZone : MonoBehaviour
{
    [SerializeField] private GameObject _extraAmmoDragMenu;

    private IMouseLocker _mouseLocker;
    private bool _inZone;

    private void Awake()
    {
        _extraAmmoDragMenu.SetActive(false);
    }

    private void Update()
    {
        if (_inZone && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(_extraAmmoDragMenu.name);
            bool menuIsActive = _extraAmmoDragMenu.activeSelf;
            _extraAmmoDragMenu.SetActive(!menuIsActive);
            _mouseLocker.LockMouse(menuIsActive);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement player) == false)
        {
            return;
        }

        _mouseLocker = player.GetComponent<IMouseLocker>();
        _inZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out _) == false)
        {
            return;
        }
        if (_mouseLocker == null)
        {
            return;
        }

        _inZone = false;
        _mouseLocker.LockMouse(true);
        _mouseLocker = null;
        _extraAmmoDragMenu.SetActive(false);
    }
}
