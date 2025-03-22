public class PlayerHealth : Health
{
    public void Heal(int healAmount)
    {
        _currentHealth += healAmount;
    }
}
