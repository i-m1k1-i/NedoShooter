namespace Assets.Scripts.Weapons
{
    public class Phantom : Automatic
    {
        private void Awake()
        {
            _damage = 20;
            _sprayDelay = 0.07f;
            MagazineCapacity = 30;
            Type = WeaponType.MainWeapon;
            Init();
        }
    }
}