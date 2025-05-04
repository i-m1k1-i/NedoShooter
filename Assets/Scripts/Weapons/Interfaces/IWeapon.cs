using UnityEngine;
using UnityEngine.Events;

namespace Nedoshooter.Weapons
{
    public interface IWeapon
    {
        public WeaponType Type { get; }
        public event UnityAction Attacked;

        public Vector3 InHandPosition {get;}
        public Transform transform { get; }
        public GameObject gameObject { get; }

        public void Attack();
    }
}
