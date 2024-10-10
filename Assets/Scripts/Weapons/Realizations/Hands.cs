using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Weapons
{
    internal class Hands : MonoBehaviour, IWeapon
    {
        public event UnityAction Attacked;

        public Vector3 InHandPosition {  get; private set; }

        private void Awake()
        {
            SetPosition();
        }

        protected void SetPosition()
        {
            InHandPosition = new Vector3(0f, -0.3f, 0f);
            print("setted");
        }

        public void Attack()
        {
            print("Hooking");
            Attacked?.Invoke();
            return;
        }

    }
}
