using Assets.Scripts.Player;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ExtraAmmoZone : MonoBehaviour
{
    [SerializeField] private GameObject _extraAmmoDragMenu;

    private bool _inZone;
    private PlayerController _playerController;

    private void Awake()
    {
        _extraAmmoDragMenu.SetActive(false);
    }

    private void Update()
    {
        if (_inZone && Input.GetKeyDown(KeyCode.E))
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
        _inZone = false;
        _playerController.SetMenuMode(false);
        _playerController = null;
        _extraAmmoDragMenu.SetActive(false);
    }
}
