namespace Assets.Scripts.Weapons
{
    public  class Pistol : SemiAutomatic
    {
        private void Awake()
        {
            _damage = 15;
            MagazineCapacity = 20;
            Init();
        }
    }
}
