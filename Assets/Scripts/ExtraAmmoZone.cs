using Assets.Scripts.Player;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ExtraAmmoZone : MonoBehaviour
{
    [SerializeField] private GameObject _extraAmmoDragMenu;
    [SerializeField] private PlayerController _playerController;

    private bool _inZone;

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
            _playerController.SetMenuMode(!menuIsActive);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<WeaponUser>(out WeaponUser _) == false)
        {
            return;
        }
        _inZone = true;
        other.TryGetComponent<PlayerController>(out _playerController);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<WeaponUser>(out WeaponUser _))
        {
            return;
        }
        if (_playerController == null)
        {
            return;
        }

        _inZone = false;
        _playerController.SetMenuMode(false);
        _playerController = null;
        _extraAmmoDragMenu.SetActive(false);
    }
}
