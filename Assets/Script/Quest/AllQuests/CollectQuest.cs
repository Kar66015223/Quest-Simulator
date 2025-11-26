using Unity.VisualScripting;
using UnityEngine;

public class CollectQuest : Quest
{
    public int random = 1/*Random.Range(20, 30)*/;
    public CollectQuest()
    {
        this.questName = $"Collect {random} coins";
        this.questDesc = "I lost some coin on the battleground, find and return them to me";
        this.questType = QuestType.SideQuest;
        this.questStatus = QuestStatus.Pending;
    }

    public override void CheckCompletion()
    {
        int coins = GameManager.Instance.currentCoin;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        string currentScene = LoadSceneManager.Instance.GetCurrentSceneName();

        if (coins >= random && currentScene == "MainGame")
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
        QuestProgressUIManager.Instance.CollectQuestProgressUI.SetActive(true);

        Debug.Log($"{questName} OnAccept is running");
    }

    public override void QuestCompleted()
    {
        QuestManager.Instance.alreadyRun = false;

        Debug.Log($"{questName} completed!");
    }

    public override void QuestFailed()
    {
        QuestManager.Instance.alreadyRun = false;

        Debug.Log($"{questName} failed!");
    }
}
