using UnityEngine;

public class TableExtraAmmo : MonoBehaviour
{
    [SerializeField] private int _extraAmmoAmount;

    public void RemoveAmmo(int amount)
    {
        _extraAmmoAmount -= amount;
    }

}
