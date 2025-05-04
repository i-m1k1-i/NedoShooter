namespace Assets.Scripts.Weapons
{
    public class Vandal : Automatic
    {
        private void Awake()
        {
            _damage = 25;
            _shotsPerSecond = 9.25f;
            MagazineCapacity = 25;
            Type = WeaponType.MainWeapon;
            Init();
        }
    }
}
