namespace Assets.Scripts.Weapons
{
    public class Vandal : Automatic
    {
        private void Awake()
        {
            _damage = 25;
            _sprayDelay = 0.1f;
            MagazineCapacity = 30;
            Init();
        }
    }
}
