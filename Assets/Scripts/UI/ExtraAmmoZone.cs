using Assets.Scripts.Player;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ExtraAmmoZone : MonoBehaviour
{
    [SerializeField] private GameObject _extraAmmoDragMenu;

    private bool _inZone;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _extraAmmoDragMenu.SetActive(true);
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
