using UnityEngine.Events;

namespace Nedoshooter.Weapons
{
    internal interface IFirearm : IWeapon
    {
        public event UnityAction AmmoAmountChanged;
        public event UnityAction BoltCocked;

        public int MagazineCapacity { get; }
        public int CurrentAmmo { get; }

        public void SafetyOff();
        public void SafetyOn();
        public int Reload(int ammoAmount);
    }
}
