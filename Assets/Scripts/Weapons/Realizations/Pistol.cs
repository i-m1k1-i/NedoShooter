namespace Nedoshooter.Weapons
{
    public  class Pistol : SemiAutomatic
    {
        private void Awake()
        {
            _damage = 15;
            MagazineCapacity = 20;
            Type = WeaponType.SecondaryWeapon;
            Init();
        }
    }
}
