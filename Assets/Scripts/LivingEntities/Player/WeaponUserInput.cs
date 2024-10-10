using UnityEngine;
using UnityEngine.Events;
using Assets.Scripts.Weapons;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(WeaponUser))]
    public class WeaponUserInput : MonoBehaviour
    {
        public event UnityAction Attacking;
        public event UnityAction Spraing;
        public event UnityAction Reloading;
        public event UnityAction<WeaponTypes> ChangingWeapon;
        public event UnityAction TryingPickUpAmmo;


        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attacking?.Invoke();
                print("Attacking input");
            }
            else if (Input.GetButton("Fire1"))
            {
                Spraing?.Invoke();
                print("Spraing input");
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                Reloading?.Invoke();
                print("Reloading input");
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                TryingPickUpAmmo?.Invoke();
            }

            HandleWeaponChanging();
        }

        private void HandleWeaponChanging()
        {
            for (int i = 1; i <= 3; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                {
                    ChangingWeapon?.Invoke((WeaponTypes)i - 1);
                    print("ChangingWeapon inputs");
                }
            }
        }
    }
}
