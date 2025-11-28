using UnityEngine;

public class FindQuest : Quest
{
    public FindQuest()
    {
        this.questName = "ตามหาเพชร";
        this.questDesc = "ฉันทำเพชรอันล้ำค่าหายระหว่างทางมาที่นี่ ช่วยออกไปตามหามันที";
        this.questType = QuestType.SideQuest;
        this.questStatus = QuestStatus.Pending;
        this.questID = 5;
    }
    public override void CheckCompletion()
    {
        int diamond = GameManager.Instance.currentDiamond;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        string currentScene = LoadSceneManager.Instance.GetCurrentSceneName();

        if (diamond >= 1 && currentScene == "MainGame")
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
        QuestProgressUIManager.Instance.FindQuestProgressUI.SetActive(true);
        Debug.Log($"{questName} OnAccept is running");
    }

    public override void QuestCompleted()
    {
        QuestProgressUIManager.Instance.FindQuestProgressUI.SetActive(false);

        GameManager.Instance.currentDiamond = 0;
        GameManager.Instance.UpdateDiamondUI();

        Debug.Log($"{questName} completed!");
    }

    public override void QuestFailed()
    {
        QuestProgressUIManager.Instance.FindQuestProgressUI.SetActive(false);

        GameManager.Instance.currentDiamond = 0;
        GameManager.Instance.UpdateDiamondUI();

        Debug.Log($"{questName} failed!");
    }
}
