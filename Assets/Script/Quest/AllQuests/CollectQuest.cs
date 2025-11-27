using Unity.VisualScripting;
using UnityEngine;

public class CollectQuest : Quest
{
    public int random = 1/*Random.Range(20, 30)*/;
    public CollectQuest()
    {
        this.questName = $"เก็บเหรียญทอง {random} เหรียญ";
        this.questDesc = "ฉันทำเหรียณทองหล่นหายระหว่างทางมาที่นี่ ช่วยออกไปตามหาพวกมันกลับมาให้ฉันที";
        this.questType = QuestType.SideQuest;
        this.questStatus = QuestStatus.Pending;
        this.questID = 2;
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
        QuestProgressUIManager.Instance.CollectQuestProgressUI.SetActive(false);

        GameManager.Instance.currentCoin = 0;
        GameManager.Instance.UpdateCoinUI();

        Debug.Log($"{questName} completed!");
    }

    public override void QuestFailed()
    {
        QuestProgressUIManager.Instance.CollectQuestProgressUI.SetActive(false);

        GameManager.Instance.currentCoin = 0;
        GameManager.Instance.UpdateCoinUI();

        Debug.Log($"{questName} failed!");
    }
}
