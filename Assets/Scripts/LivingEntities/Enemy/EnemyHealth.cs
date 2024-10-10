using UnityEngine;

public class EnemyHealth : Health
{
    private static EnemyController enemyController;

    private void Start()
    {
        if (enemyController == null)
        {
            enemyController = GameObject.Find("EnemyController").GetComponent<EnemyController>();
        }
    }

    protected override void Die()
    {
        enemyController.ReturnToPool(gameObject);
    }
}
