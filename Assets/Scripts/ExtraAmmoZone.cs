using Assets.Scripts.Player;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ExtraAmmoZone : MonoBehaviour
{
    [SerializeField] private GameObject _extraAmmoDragMenu;

    private bool _inZone;

    private void Awake()
    {
        _extraAmmoDragMenu.SetActive(false);
    }

    private void Update()
    {
        if (_inZone && Input.GetKeyDown(KeyCode.E))
        {
            if (_extraAmmoDragMenu.activeSelf == false)
            {
                _extraAmmoDragMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                _extraAmmoDragMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<WeaponUser>(out WeaponUser _) == false)
        {
            return;
        }

        _inZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<WeaponUser>(out WeaponUser weaponUser))
        {
            return;
        }

        _inZone = false;
        _extraAmmoDragMenu.SetActive(false);
    }
}
