using UnityEngine;

public class SurviveQuestSpawner : MonoBehaviour
{
    EnemySpawner[] enemySpawners;
    private Player player;

    private bool hasSpawned = false;

    private void Start()
    {
        enemySpawners = GetComponentsInChildren<EnemySpawner>();
    }

    private void Update()
    {
        if (QuestManager.Instance.selectedQuest == null ||
            QuestManager.Instance.selectedQuest.questID != 4)
        {
            return; //stop Update()
        }

        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }

        if (player != null && !hasSpawned)
        {
            foreach (EnemySpawner e in enemySpawners)
            {
                e.enabled = true;
            }

            hasSpawned = true;
        }

        if (QuestManager.Instance.selectedQuest.questStatus == Quest.QuestStatus.Completed ||
            QuestManager.Instance.selectedQuest.questStatus == Quest.QuestStatus.Failed)
        {
            ClearScene();

            foreach (EnemySpawner e in enemySpawners)
            {
                e.enabled = false;
            }
        }
    }

    private void ClearScene()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            Destroy(enemy.gameObject);
        }

        Debug.Log("ClearScene is running");
    }
}
