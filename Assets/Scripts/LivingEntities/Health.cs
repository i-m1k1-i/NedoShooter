using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int _maxHealth = 100;

    [SerializeField] protected int _currentHealth;
    public int CurrentHealth => _currentHealth;


    protected virtual void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        print(_currentHealth);
        if (_currentHealth <= 0 )
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
