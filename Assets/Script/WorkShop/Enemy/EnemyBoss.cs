using UnityEngine;

public class EnemyBoss : EnemyMovetoPlayer
{
    public override void TakeDamage(int amount)
    {
        amount = Mathf.Clamp(amount - Deffent, 1, amount);
        health -= amount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
