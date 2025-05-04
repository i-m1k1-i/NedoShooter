namespace Nedoshooter.Weapons
{
    public class Phantom : Automatic
    {
        private void Awake()
        {
            _damage = 20;
            _shotsPerSecond = 11f;
            MagazineCapacity = 30;
            Type = WeaponType.MainWeapon;
            Init();
        }
    }
}