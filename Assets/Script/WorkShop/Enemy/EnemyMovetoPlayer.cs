using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovetoPlayer : Enemy
{
    private void Update()
    {
        if (player == null)
        {
            animator.SetBool("Attack", false);
            return;
        }
        Turn(player.transform.position - transform.position);
        timer -= Time.deltaTime;

        if (GetDistanPlayer() < 1.5)
        {
            Attack(player);
        }
        else
        {
            animator.SetBool("Attack", false);
            Vector3 direction = (player.transform.position - transform.position).normalized;
            Move(direction);
        }
    }

    public override void TakeDamage(int amount)
    {
        amount = Mathf.Clamp(amount - Deffent, 1, amount);
        health -= amount;
        if (health <= 0)
        {
            if (QuestManager.Instance.selectedQuest.questID == 1)
            {
                GameManager.Instance.AddKill(1); 
            }

            Destroy(gameObject);
        }
    }
}
