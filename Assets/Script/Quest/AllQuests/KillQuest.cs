using UnityEngine;

public class KillQuest : Quest
{
    public int killGoal = Random.Range(10, 25);
    public KillQuest()
    {
        this.questName = $"Kill {killGoal} slimes";
        this.questDesc = "Go to the battleground and kill some slimes";
        this.questType = QuestType.SideQuest;
        this.questStatus = QuestStatus.Pending;
        this.questID = 1;
    }

    public override void CheckCompletion()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (GameManager.Instance.killCount >= killGoal)
        {
            this.questStatus = QuestStatus.Completed;
            QuestCompleted();
        }

        if (player == null)
        {
            this.questStatus = QuestStatus.Failed;
            QuestFailed();
        }

        Debug.Log($"{questName} CheckCompletion is running");
    }

    public override void OnAccept()
    {
        QuestProgressUIManager.Instance.KillQuestProgressUI.SetActive(true);

        Debug.Log($"{questName} OnAccept is running");
    }

    public override void QuestCompleted()
    {
        QuestProgressUIManager.Instance.KillQuestProgressUI.SetActive(false);
        GameManager.Instance.killCount = 0;
        //GameManager.Instance.UpdateKillUI();

        Debug.Log($"{questName} completed!");
    }

    public override void QuestFailed()
    {
        QuestProgressUIManager.Instance.KillQuestProgressUI.SetActive(false);
        GameManager.Instance.killCount = 0;
        //GameManager.Instance.UpdateKillUI();

        Debug.Log($"{questName} failed!");
    }
}
