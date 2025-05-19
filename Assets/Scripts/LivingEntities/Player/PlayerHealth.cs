using UnityEngine.SceneManagement;

public class PlayerHealth : Health, IHealable
{
    public void Heal(int healAmount)
    {
        _currentHealth += healAmount;
    }

    protected override void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
